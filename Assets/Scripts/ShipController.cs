using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
	public int linSpeed = 15;
	public int angSpeed = 40;

	//private Vector3 cameraOffset;
	private Rigidbody playerBody;

	//Initialization
	void Start () {
		
		//Don't need to worry about camera offset for 2d games :)
		//cameraOffset = transform.FindChild("PlayerCamera").position;


		playerBody = GetComponent<Rigidbody>();
	}
	

	void FixedUpdate () {
		//Add rotation (LEFT/A & RIGHT/D) movement to the ship
		transform.Rotate(0.0f, Input.GetAxis("Horizontal") * Time.deltaTime * angSpeed, 0.0f);

		//Add the UP/W & DOWN/S movement to the ship
		playerBody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * linSpeed);

	}
}
