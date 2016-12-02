using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	//This script belongs on the PlayerModel gameobject and controls the position of the player.
	
	public Camera playerCamera;
	public GameObject playerLight;
	public float speed;
	public float angSpeed;

	//[SyncVar(hook=onHealthChange)]
	private int health;
	//[SyncVar(hook=onEnergyChange)]
	private int energy;
	private int maxHealth;
	private int maxEnergy;
	//[SyncVar(hook=onScoreChange)]
	private int score;
	private float queuedEnergy;
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

	//Initialization
	void Start () {
		health = 100;
		energy = 100;
		maxHealth = 100;
		maxEnergy = 100;
		score = 0;
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
		lastControls.Add("x", 0.0f);
		lastControls.Add("y", 0.0f);
		lastControls.Add("shoot", false);
		lastControls.Add("debug", false);
		lastControls.Add("shield", false);
		lastControls.Add("menu", false);
		lastControls.Add("scoreboard", false);
		updateControls(controls);
		playerName = "Debgger";
		ui.addScoreboardEntry(playerName, Color.red);
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
	}

	void Update(){
		playerCamera.transform.position = this.transform.position + camOffset;
		playerLight.transform.position = this.transform.position + lightOffset;
		//Get control keys
		updateControls(controls);
		//Turn on the shield
		if((bool)controls["shield"] && !(bool)lastControls["shield"]) {
			shieldManager.toggleShield();
		}
			
		//Show scoreboard while key is pressed
		ui.setScoreboardVisible((bool)controls["scoreboard"]);

		//Pause Menu (Doesn't actually pause the game)
		if((bool)controls["menu"] && !(bool)lastControls["menu"]) {
			ui.togglePauseMenuVisible();
			localPause = !localPause;
		}

		//Debug key
		if ((bool)controls["debug"] && !(bool)lastControls["debug"]) {
			Debug.Log("Velocity: " + rb.velocity);
			Debug.Log("Rel Velocity: " + new Vector3(rb.velocity.x * Mathf.Sin(rotation), 0.0f, rb.velocity.z * Mathf.Cos(rotation)));
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			updateHealth(-10);
			updateEnergy(-15);
		}
		//Update lastControls
		lastControls["x"] = controls["x"];
		lastControls["y"] = controls["y"];
		lastControls["shoot"] = controls["shoot"];
		lastControls["debug"] = controls["debug"];
		lastControls["shield"] = controls["shield"];
		lastControls["menu"] = controls["menu"];
		lastControls["scoreboard"] = controls["scoreboard"];
	}

	void onTriggerEnter(Collider target){
		target.GetComponent<ObjectManager>().performObjectTaskEnter(this.gameObject);
	}

	/*
	 * These are for multiplayer support
	void onHealthChange(){

	}

	void onEnergyChange(){

	}
	
	void onScoreChange(){

	}*/

	//Defined Functions

	//TODO: allow different keys to be set
	//Uodates controls to the current input
	private void updateControls(Dictionary<string, object> controlDict){
		controlDict["x"] = Input.GetAxis("Horizontal");
		controlDict["y"] = Input.GetAxis("Vertical");
		controlDict["shoot"] = Input.GetMouseButton(0);
		controlDict["debug"] = Input.GetKey(KeyCode.O);
		controlDict["shield"] = Input.GetKey(KeyCode.Space);
		controlDict["menu"] = Input.GetKey(KeyCode.Escape);
		controlDict["scoreboard"] = Input.GetKey(KeyCode.Tab);
	}

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

	public void updateScore(int magnitude){
		score += magnitude;
		ui.updateScoreboardEntry(this.playerName, score);
	}

	//Used for shield to queue up energy loss
	public void queueEnergy(float magnitude){
		queuedEnergy += magnitude;
		if(queuedEnergy > 1.0f) {
			updateEnergy(-Mathf.FloorToInt(queuedEnergy));
			queuedEnergy -= Mathf.Floor(queuedEnergy);
		}
	}

	public void asteroidCollision(int magnitude){
		//Make the player blow up if asteroid big enough?
	}

	public void bulletCollision(int magnitude){
		//Make the player lose energy and health and/or degrade ship quality?
	}
}
