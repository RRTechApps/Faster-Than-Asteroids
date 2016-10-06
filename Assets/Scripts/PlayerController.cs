using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public Camera cam;
	private Rigidbody rb;
	private Camera kms;
	public Vector3 offset = new Vector3(0.0f,0.0f,0.0f);
	void Start ()
	{
		Debug.Log("Heyo: " + cam.enabled);
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
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 ((moveHorizontal), 0.0f, (moveVertical));
		this.transform.rotation = new Quaternion(0.0f,90.0f,90.0f,0.0f);
		rb.AddForce (movement * speed);
	}
	void CameraContoller(Camera cam){
		cam.transform.position = this.transform.position + offset;
		this.transform.rotation = new Quaternion(90.0f,90.0f,90.0f,0.0f);
	}
}

