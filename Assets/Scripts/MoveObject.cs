using UnityEngine;

public class MoveObject : MonoBehaviour {

	[SerializeField]
	private float objSpeed = 1;
	[SerializeField]
	protected float resetPosition = -25;
	[SerializeField]
	protected float startPosition = 81.25f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (GameManager.instance.GameOver)
			return;

		transform.Translate(Vector3.left * objSpeed * Time.deltaTime, Space.World);

		if (transform.localPosition.x <= resetPosition) {
			Vector3 newPos = new Vector3(startPosition, transform.position.y, transform.position.z);
			transform.position = newPos;
		}
	}
}
