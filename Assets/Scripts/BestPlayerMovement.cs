using UnityEngine;
using System.Collections;

public class BestPlayerMovement : MonoBehaviour {

	// Use this for initialization
	public Rigidbody rb;
	private Camera cam;
	public float speed;

	void Start () {
		rb = this.GetComponent<Rigidbody>();
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		float vSped = Input.GetAxis("Vertical");
		rb.transform.Rotate(new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f));
		rb.AddRelativeForce(new Vector3(0.0f, 0.0f, vSped * speed));
	}

	public IEnumerator waitTime(float waits){
		rb.transform.Rotate(new Quaternion (Quaternion.identity));
		yield return new WaitForSeconds(waits);
	}
}
