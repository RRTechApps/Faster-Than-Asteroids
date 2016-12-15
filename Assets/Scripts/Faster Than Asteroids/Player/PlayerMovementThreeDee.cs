using UnityEngine;
using System.Collections;

public class PlayerMovementThreeDee : MonoBehaviour {
	//This script belongs on the PlayerModel gameobject and controls player movement


	public float speed;
	public float angSpeed;

	private Transform playerLight;
	private Rigidbody rb;
	private Vector3 camOffset;
	private Vector3 lightOffset;
	private PlayerControls controls;
	private Player player;

	void Start () {
		//Script Definitions

		controls = GetComponent<PlayerControls>();
		player = GetComponent<Player>();

		//Instance Variables

		//Original offset of the light
		playerLight = transform.parent.Find("Headlight");
		lightOffset = playerLight.transform.position;
		//Player's rigidbody
		rb = GetComponent<Rigidbody>();
	}

	//Movement is done here
	void FixedUpdate () {
		float roll = -controls.getAxis("roll");
		float pitch = controls.getAxis("pitch");
		float yaw = -controls.getAxis("yaw");
		float thrust = controls.getAxis("thrust");

		if(thrust != 0.0f) {
//			rb.AddForce(new Vector3(thrust * speed * Mathf.Sin(rotation), 0.0f, thrust * speed * Mathf.Cos(rotation)), ForceMode.Force);
			//rb.AddForce(player.transform.InverseTransformVector(new Vector3(thrust * speed, 0.0f, thrust * speed)), ForceMode.Force);
			//rb.AddRelativeForce(new Vector3(thrust * speed, 0.0f, thrust * speed), ForceMode.Force);
		}
		if(pitch != 0.0f) {
			this.transform.Rotate(pitch * Time.deltaTime * angSpeed, 0.0f, 0.0f, Space.Self);
			//rotation = this.transform.eulerAngles.y * (Mathf.PI / 180.0f);
			rb.velocity = Vector3.Lerp(rb.velocity, transform.TransformVector(rb.velocity), Time.deltaTime);
		}
		if(roll != 0.0f) {
			this.transform.Rotate(0.0f, 0.0f, roll * Time.deltaTime * angSpeed, Space.Self);
			//rotation = this.transform.eulerAngles.y * (Mathf.PI / 180.0f);
			//Vector3 newVelocity = new Vector3(rb.velocity.magnitude * Mathf.Sin(rotation), 0.0f, rb.velocity.magnitude * Mathf.Cos(rotation));
			//rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.deltaTime);
			rb.velocity = Vector3.Lerp(rb.velocity, transform.TransformVector(rb.velocity), Time.deltaTime);
		}
		if(yaw != 0.0f) {
			this.transform.Rotate(0.0f, yaw * Time.deltaTime * angSpeed, 0.0f, Space.Self);
			//rotation = this.transform.eulerAngles.y * (Mathf.PI / 180.0f);
			//Vector3 newVelocity = new Vector3(rb.velocity.magnitude * Mathf.Sin(rotation), 0.0f, rb.velocity.magnitude * Mathf.Cos(rotation));
			//rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.deltaTime);
			rb.velocity = Vector3.Lerp(rb.velocity, transform.TransformVector(rb.velocity), Time.deltaTime);
		}
	}

	void Update () {
		//Translate the player's headlight
		playerLight.position = this.transform.position + this.lightOffset;
		playerLight.eulerAngles = new Vector3(20.0f, 0.0f, 0.0f);
		playerLight.RotateAround(player.transform.position, Vector3.up, player.transform.eulerAngles.y);
	}

	void onTriggerEnter(Collider target){
		target.GetComponent<EntityHandler>().performObjectTaskEnter(this.gameObject);
	}

	//Defined Methods

	public void debug(){
		Debug.Log("Velocity: " + rb.velocity);
		//Debug.Log("Rel Velocity: " + new Vector3(rb.velocity.x * Mathf.Sin(rotation), 0.0f, rb.velocity.z * Mathf.Cos(rotation)));
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;	
	}
}
