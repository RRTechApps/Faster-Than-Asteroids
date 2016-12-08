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
		playerLight = transform.parent.Find("Spotlight");
		lightOffset = playerLight.transform.position;
		//Player's rigidbody
		rb = GetComponent<Rigidbody>();
		rotation = 0.0f;
	}

	//Movement is done here
	void FixedUpdate () {
		rotation = this.transform.eulerAngles.y * Mathf.Deg2Rad;
		float horizontalControl = (float)controls.getInput("roll");
		float verticalControl = (float)controls.getInput("pitch");

		if(verticalControl != 0.0f) {
			rb.AddForce(new Vector3(verticalControl * speed * Mathf.Sin(rotation), 0.0f, verticalControl * speed * Mathf.Cos(rotation)), ForceMode.Force);
		}
		if(horizontalControl != 0.0f) {
			this.transform.Rotate(0.0f, horizontalControl * Time.deltaTime * angSpeed, 0.0f);
			rb.maxAngularVelocity = angSpeed / 20.0f;
			rotation = this.transform.eulerAngles.y * (Mathf.PI / 180.0f);
			Vector3 newVelocity = new Vector3(rb.velocity.magnitude * Mathf.Sin(rotation), 0.0f, rb.velocity.magnitude * Mathf.Cos(rotation));
			rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.deltaTime);
		}
	}

	void Update () {
		//Translate the player's components
		playerLight.position = this.transform.position + this.lightOffset;
	}

	void onTriggerEnter(Collider target){
		target.GetComponent<ObjectManager>().performObjectTaskEnter(this.gameObject);
	}

	//Defined Methods

	public void debug(){
		Debug.Log("Velocity: " + rb.velocity);
		Debug.Log("Rel Velocity: " + new Vector3(rb.velocity.x * Mathf.Sin(rotation), 0.0f, rb.velocity.z * Mathf.Cos(rotation)));
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;	
	}
}
