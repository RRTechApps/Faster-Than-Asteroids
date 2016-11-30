using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	//[SyncVar(hook=onHealthChange)]
	private int health;
	//[SyncVar(hook=onEnergyChange)]
	private int energy;
	private int maxHealth;
	private int maxEnergy;
	private float rotation;
	private bool localPause;
	private string playerName;
	private Rigidbody rb;
	private Vector3 camOffset;
	private Vector3 lightOffset;
	private UIManager ui;
	private Color team;
	private Dictionary<string, object> controls;
	private Dictionary<string, object> lastControls;
	private ShieldManager shieldManager;

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
		//Front of the player model
		rotation = 0.0f;
		//If the player is in the "pause" menu
		localPause = false;
		//Player's rigidbody
		rb = GetComponent<Rigidbody>();
		//Original offset of the camera
		camOffset = playerCamera.transform.position;
		//Original offset of the light
		lightOffset = playerLight.transform.position;
		ui = transform.parent.Find("UIObjects").GetComponent<UIManager>();
		shieldManager = transform.Find("Shield").gameObject.GetComponent<ShieldManager>();
		controls = new Dictionary<string, object>();
		lastControls = new Dictionary<string, object>();
		controls.Add("x", 0.0f);
		controls.Add("y", 0.0f);
		controls.Add("shoot", false);
		controls.Add("debug", false);
		controls.Add("shield", false);
		controls.Add("menu", false);
		controls.Add("scoreboard", false);

		lastControls.Add("x", 0.0f);
		lastControls.Add("y", 0.0f);
		lastControls.Add("shoot", false);
		lastControls.Add("debug", false);
		lastControls.Add("shield", false);
		lastControls.Add("menu", false);
		lastControls.Add("scoreboard", false);

	}

	//Movement is done here
	void FixedUpdate () {
		rotation = this.transform.eulerAngles.y * Mathf.Deg2Rad;
		float horizontalControl = (float)controls["x"];
		float verticalControl = (float)controls["y"];

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
		//Get control keys
		controls["x"] = Input.GetAxis("Horizontal");
		controls["y"] = Input.GetAxis("Vertical");
		controls["shoot"] = Input.GetMouseButton(0);
		controls["debug"] = Input.GetKey(KeyCode.O);
		controls["shield"] = Input.GetKey(KeyCode.Space);
		controls["menu"] = Input.GetKey(KeyCode.Escape);
		controls["scoreboard"] = Input.GetKey(KeyCode.Tab);
		//Debug key
		if ((bool)controls["debug"]) {
			Debug.Log("Velocity: " + rb.velocity);
			Debug.Log("Rel Velocity: " + new Vector3(rb.velocity.x * Mathf.Sin(rotation), 0.0f, rb.velocity.z * Mathf.Cos(rotation)));
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			updateHealth(-10);
			updateEnergy(-15);
		}
		//Turn on the shield
		if((bool)controls["shield"] && !(bool)lastControls["shield"]) {
			shieldManager.toggleShield();
			Debug.Log("toggle shield");
		}
		//Scoreboard
		ui.setScoreboardVisible((bool)controls["scoreboard"]);
		//Pause Menu (Doesn't actually pause the game)
		if((bool)controls["menu"]) {
			ui.togglePauseMenuVisible();
		}
		lastControls["x"] = controls["x"];
		lastControls["y"] = controls["y"];
		lastControls["shoot"] = controls["shoot"];
		lastControls["debug"] = controls["debug"];
		lastControls["shield"] = controls["shield"];
		lastControls["menu"] = controls["menu"];
		lastControls["scoreboard"] = controls["scoreboard"];
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
		ui.updateText("EnergyText", "Energy: " + energy + "/" + maxEnergy);	
	}

	public void asteroidCollision(int magnitude){
		//Make the player blow up if asteroid big enough?
	}

	public void bulletCollision(int magnitude){
		//Make the player lose energy and health and/or degrade ship quality?
	}
}
