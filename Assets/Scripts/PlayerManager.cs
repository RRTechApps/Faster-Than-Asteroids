using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	//[SyncVar(hook=onHealthChange)]
	private int health;
	//[SyncVar(hook=onEnergyChange)]
	private int energy;
	private Rigidbody rb;
	public Camera cam;
	public float speed;
	public int angSpeed;

	//Initialization
	void Start () {
		health = 100;
		energy = 100;
		angSpeed = 40;
		rb = GetComponent<Rigidbody>();

		//Gets the camera of the player the script is attached to
		//cam has no use as of yet in the 2D prototype

	}

	//Movement is done here
	void FixedUpdate () {

		transform.Rotate(0.0f, Input.GetAxis("Horizontal") * Time.deltaTime * angSpeed, 0.0f);
		rb.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * speed);
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
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
