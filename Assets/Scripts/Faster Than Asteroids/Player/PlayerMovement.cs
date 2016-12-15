using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	//This script belongs on the PlayerModel gameobject and controls player movement


	public float speed;
	public float angSpeed;

	private float rotation;
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
		rotation = 0.0f;
		rb.maxAngularVelocity = 2.0f;
	}

	//Movement is done here
	void FixedUpdate () {
		rotation = this.transform.eulerAngles.y * Mathf.Deg2Rad;
		float horizontalControl = controls.getAxis("roll");
		float verticalControl = controls.getAxis("pitch");
		if(verticalControl != 0.0f) {
			rb.AddForce(new Vector3(verticalControl * speed * Mathf.Sin(rotation), 0.0f, verticalControl * speed * Mathf.Cos(rotation)), ForceMode.Force);
			//rb.AddForce(player.transform.TransformPoint(new Vector3(verticalControl * speed, 0.0f, verticalControl * speed)), ForceMode.Force);
			//rb.AddRelativeForce(new Vector3(verticalControl * speed, 0.0f, verticalControl * speed), ForceMode.Force);
		}
		if(horizontalControl != 0.0f) {
			//this.transform.Rotate(0.0f, horizontalControl * Time.deltaTime * angSpeed, 0.0f, Space.Self);
			rb.AddRelativeTorque(0.0f, horizontalControl * Time.deltaTime * angSpeed, 0.0f);
			rotation = this.transform.eulerAngles.y * (Mathf.PI / 180.0f);
			Vector3 newVelocity = new Vector3(rb.velocity.magnitude * Mathf.Sin(rotation), 0.0f, rb.velocity.magnitude * Mathf.Cos(rotation));
			//Vector3 newVelocity = player.transform.TransformDirection(rb.velocity);
			rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.deltaTime);
		}
		this.transform.rotation = Quaternion.Euler(Vector3.up * this.transform.eulerAngles.y);
		this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
	}

	void Update () {
		//Translate the player's headlight
		playerLight.position = this.transform.position + lightOffset;
		playerLight.eulerAngles = new Vector3(20.0f, 0.0f, 0.0f);
		playerLight.RotateAround(player.transform.position, Vector3.up, player.transform.eulerAngles.y);

		//this.transform.eulerAngles.Set(0.0f, this.transform.eulerAngles.y, 0.0f);
	}

	void onTriggerEnter(Collider target){
		target.GetComponent<EntityHandler>().performObjectTaskEnter(this.gameObject);
	}

	//Defined Methods

	public void debug(){
		Debug.Log("Velocity: " + rb.velocity);
		Debug.Log("Rel Velocity: " + new Vector3(rb.velocity.x * Mathf.Sin(rotation), 0.0f, rb.velocity.z * Mathf.Cos(rotation)));
		Debug.Log("Ang Velocity: " + rb.angularVelocity);
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;	
	}
}
