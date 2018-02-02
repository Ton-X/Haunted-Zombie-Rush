using System.Collections;
using UnityEngine;

public class Skull : MoveObject {

	[SerializeField]
	private float angularSpeed;
	[SerializeField]
	private Vector2 xRange;
	[SerializeField]
	private Vector2 yRange;
	[SerializeField]
	private AudioClip sfxCoinPickup;

	private AudioSource source;

	private void Awake() {
		source = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(Rotate());
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (!GameManager.instance.PlayerActive)
			return;

		base.Update();

		if (transform.localPosition.x == startPosition) {
			if (!gameObject.GetComponent<Renderer>().enabled) {
				gameObject.GetComponent<Renderer>().enabled = true;
				gameObject.GetComponent<BoxCollider>().enabled = true;
			}

			float randXPos = Random.Range(xRange.x, xRange.y);
			float randYPos = Random.Range(yRange.x, yRange.y);
			transform.position = new Vector3(randXPos, randYPos, transform.position.z);
		}
	}

	IEnumerator Rotate() {
		while (true) {
			transform.Rotate(Vector3.up, angularSpeed * Time.deltaTime, Space.World);
			yield return null;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			source.PlayOneShot(sfxCoinPickup);
			GameManager.instance.AddPoint();
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
}
