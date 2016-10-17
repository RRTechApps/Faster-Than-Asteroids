using UnityEngine;
using System.Collections;

public class BestPlayerMovement : MonoBehaviour {

	// Initialization of variables :)
	private Rigidbody rb;
	private Transform cam;
	public float speed;
	public int angSpeed = 40;

	void Start () {
		//Gets the rigidbody of the player the script is attached to
		rb = GetComponent<Rigidbody>();
		
		//Gets the camera of the player the script is attached to
		//cam has no use as of yet in the 2D prototype
		cam = transform.Find("PlayerCamera");
	}

	// Use FixedUpdate for physics
	void FixedUpdate () {
		rb.transform.Rotate(0.0f, Input.GetAxis("Horizontal") * Time.deltaTime * angSpeed, 0.0f);
		rb.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * speed);
		if(Input.GetKeyDown(KeyCode.Space))
			rb.velocity = Vector3.zero;
	}
	//Stabilization Func! WHY DOES THIS EXIST???
	/*public IEnumerator waitTime(float waits){
		transform.rotation = Quaternion.identity;
		yield return new WaitForSeconds(waits);
	}*/
}

