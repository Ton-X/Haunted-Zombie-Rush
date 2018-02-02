using System.Collections;
using UnityEngine;

public class Rock : MoveObject {

	[SerializeField]
	private Vector3 topPosition;
	[SerializeField]
	private Vector3 bottomPosition;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float angularSpeed;

	// Use this for initialization
	void Start () {
		StartCoroutine(Move(topPosition));
		StartCoroutine(Rotate());
	}

	protected override void Update() {
		if (!GameManager.instance.PlayerActive)
			return;

		base.Update();
	}

	IEnumerator Move(Vector3 target) {
		while (Mathf.Abs((target - transform.localPosition).y) > 0.2f) {
			Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;
			transform.localPosition += direction * Time.deltaTime * speed;

			yield return null;
		}

		yield return new WaitForSeconds(0.5f);

		Vector3 newTarget = target.y == topPosition.y ? bottomPosition : topPosition;
		StartCoroutine(Move(newTarget));
	}

	IEnumerator Rotate() {
		while(true) {
			transform.Rotate(Vector3.up, angularSpeed * Time.deltaTime);
			yield return null;
		}
	}
}
