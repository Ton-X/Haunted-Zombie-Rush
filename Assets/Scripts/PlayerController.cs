using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float jumpForce = 1;
	[SerializeField]
	private AudioClip sfxJump;
	[SerializeField]
	private AudioClip sfxDeath;

	private AudioSource audioSource;
	private Animator anim;
	private Rigidbody rb;
	private bool jumpFlag = false;

	void Awake () {
		Assert.IsNotNull(sfxJump);
		Assert.IsNotNull(sfxDeath);

		audioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.GameOver || !GameManager.instance.GameStarted)
			return;

		if (Input.GetMouseButtonDown(0)) {
			GameManager.instance.StartGame();
			anim.Play("Jump");
			audioSource.PlayOneShot(sfxJump);
			rb.useGravity = true;
			jumpFlag = true;
		}
	}

	void FixedUpdate() {
		if (jumpFlag) {
			jumpFlag = false;
			rb.velocity = Vector2.zero;
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Obstacle") {
			audioSource.PlayOneShot(sfxDeath);
			rb.AddForce(new Vector2(-15, 10), ForceMode.Impulse);
			rb.detectCollisions = false;

			GameManager.instance.HitObstacle();
		}
	}
}
