using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	//[SyncVar(hook=onHealthChange)]
	private int health;
	//[SyncVar(hook=onEnergyChange)]
	private int energy;
	private Rigidbody rb;
	private Transform hostTransform;
	private Vector3 camOffset;
	private Vector3 lightOffset;

	public float rotation;
	public Camera playerCamera;
	public GameObject playerLight;
	public float speed;
	public float angSpeed;
	public float maxSpeed;
	public float turningSpringRate;


	//Initialization
	void Start () {
		health = 100;
		energy = 100;
		//Player's rigidbody
		rb = GetComponent<Rigidbody>();
		//Original offset of the camera
		camOffset = playerCamera.transform.position;
		//Original offset of the light
		lightOffset = playerLight.transform.position;
		//Front of the player model
		rotation = 0.0f;
		hostTransform = this.transform.parent;
	}

	//Movement is done here
	void FixedUpdate () {
		rotation = this.transform.eulerAngles.y * (Mathf.PI / 180.0f);
		float verticalControl = Input.GetAxis("Vertical");
		float horizontalControl = Input.GetAxis("Horizontal");

		if(verticalControl != 0.0f) {
			rb.AddForce(new Vector3(verticalControl * speed * Mathf.Sin(rotation), 0.0f, verticalControl * speed * Mathf.Cos(rotation)));
			if(Mathf.Sign(verticalControl) == -1.0f) {
				horizontalControl *= -1;
			}
		}
		if(horizontalControl != 0.0f) {
			this.transform.Rotate(0.0f, horizontalControl * Time.deltaTime * angSpeed, 0.0f);
			rb.maxAngularVelocity = angSpeed / 20.0f;
			rotation = this.transform.eulerAngles.y * (Mathf.PI / 180.0f);
			Vector3 newVelocity = new Vector3(rb.velocity.magnitude * Mathf.Sin(rotation), 0.0f, rb.velocity.magnitude * Mathf.Cos(rotation));
			rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.deltaTime);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
		playerCamera.transform.position = this.transform.position + camOffset;
		playerLight.transform.position = this.transform.position + lightOffset;
	}

	void onTriggerEnter(Collider target){
		target.GetComponent<ObjectManager>().performObjectTask(this.gameObject);
	}

	/*
	 * These are for multiplayer support
	void onHealthChange(){

	}

	void onEnergyChange(){

	}*/

	//Defined Functions

	public void updateHealth(int magnitude){
		health += magnitude;
	}

	public void updateEnergy(int magnitude){
		energy += magnitude;
	}

	public void asteroidCollision(int magnitude){
		//Make the player blow up if asteroid big enough?
	}

	public void bulletCollision(int magnitude){
		//Make the player lose energy and health and/or degrade ship quality?
	}
}
