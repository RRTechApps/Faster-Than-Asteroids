using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public Camera cam;
	private Rigidbody rb;
	private Camera kms;
	public Vector3 offset = new Vector3(0.0f,0.0f,0.0f);
	float ispressed;
	void Start ()
	{
		//Debug.Log("Heyo: " + cam.enabled);
		//kms = (Camera)Instantiate(cam, this.transform.position, new Quaternion(0.0f,0.0f,0.0f,0.0f));
		//kms.transform.parent = this.transform;
		rb = GetComponent<Rigidbody>();
	}
	void LateUpdate()
	{
		//CameraContoller(kms);
	}
	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.UpArrow)) {
			rb.AddForce (transform.forward * speed);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			rb.AddForce (transform.forward*-1 * speed);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			//ispressedLeft = 0;
			rb.rotation = Quaternion.Euler (0.0f,ispressed, 0.0f);
			ispressed++;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//ispressedRight = 0;
			rb.rotation = Quaternion.Euler (0.0f,ispressed, 0.0f);
			ispressed--;
		}
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");
		//Vector3 vertiMove = new Vector3 (0.0f, 0.0f, (moveVertical));
		//this.transform.rotation = Quaternion(0.0f,90.0f,90.0f,0.0f);
		//rb.AddForce (vertiMove * speed);
		//rb.rotation = Quaternion.Euler (0.0f,ispressed, 0.0f);
		//if (moveHorizontal > 0) {
		//	ispressed++;
		//}

		//rb.transform.Translate (movement * speed);
	}
	/*void CameraContoller(Camera cam){
		cam.transform.position = this.transform.position + offset;
		this.transform.rotation = new Quaternion(90.0f,90.0f,90.0f,0.0f);
	}*/
}
