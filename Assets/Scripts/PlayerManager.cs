using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	//[SyncVar(hook=onHealthChange)]
	private int health;
	//[SyncVar(hook=onEnergyChange)]
	private int energy;
	private int maxHealth;
	private int maxEnergy;
	private string playerName;
	private Rigidbody rb;
	private Transform hostTransform;
	private Vector3 camOffset;
	private Vector3 lightOffset;
	private UIManager ui;
	private float rotation;

	public Camera playerCamera;
	public GameObject playerLight;
	public float speed;
	public float angSpeed;


	//Initialization
	void Start () {
		health = 100;
		energy = 100;
		maxHealth = 100;
		maxEnergy = 100;
		//Player's rigidbody
		rb = GetComponent<Rigidbody>();
		//Original offset of the camera
		camOffset = playerCamera.transform.position;
		//Original offset of the light
		lightOffset = playerLight.transform.position;
		//Front of the player model
		rotation = 0.0f;
		hostTransform = this.transform.parent;
		ui = GameObject.Find("UIObjects").GetComponent<UIManager>();
	}

	//Movement is done here
	void FixedUpdate () {
		rotation = this.transform.eulerAngles.y * Mathf.Deg2Rad;
		float verticalControl = Input.GetAxis("Vertical");
		float horizontalControl = Input.GetAxis("Horizontal");

		if(verticalControl != 0.0f) {
			rb.AddForce(new Vector3(verticalControl * speed * Mathf.Sin(rotation), 0.0f, verticalControl * speed * Mathf.Cos(rotation)));
		}
		if(horizontalControl != 0.0f) {
			this.transform.Rotate(0.0f, horizontalControl * Time.deltaTime * angSpeed, 0.0f);
			rb.maxAngularVelocity = angSpeed / 20.0f;
			rotation = this.transform.eulerAngles.y * (Mathf.PI / 180.0f);
			Vector3 newVelocity = new Vector3(rb.velocity.magnitude * Mathf.Sin(rotation), 0.0f, rb.velocity.magnitude * Mathf.Cos(rotation));
			rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.deltaTime);
		}
		playerCamera.transform.position = this.transform.position + camOffset;
		playerLight.transform.position = this.transform.position + lightOffset;
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("Velocity: " + rb.velocity);
			Debug.Log("Rel Velocity: " + new Vector3(rb.velocity.x * Mathf.Sin(rotation), 0.0f, rb.velocity.z * Mathf.Cos(rotation)));
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			updateHealth(-10);
			updateEnergy(-15);
		}
		ui.setScoreboardVisible(Input.GetKey(KeyCode.Tab));
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
		health = Mathf.Min(health, maxHealth);
		ui.updateImage("HPBarPositive", new Vector2(health, 10.0f));
		ui.updateText("HPText", "HP: " + health + "/" + maxHealth);
	}

	public void updateEnergy(int magnitude){
		energy += magnitude;
		energy = Mathf.Min(energy, maxEnergy);
		ui.updateImage("EnergyBarPositive", new Vector2(energy, 10.0f));
		ui.updateText("EnergyText", "Energy: " + energy + "/" + maxEnergy);	}

	public void asteroidCollision(int magnitude){
		//Make the player blow up if asteroid big enough?
	}

	public void bulletCollision(int magnitude){
		//Make the player lose energy and health and/or degrade ship quality?
	}
}
