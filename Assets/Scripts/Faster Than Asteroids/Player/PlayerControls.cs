using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls: MonoBehaviour {
	//This script belongs on the PlayerModel gameobject and controls the controls


	private bool localPause;
	private UIManager ui;
	private Dictionary<string, object> controls;
	private Dictionary<string, object> lastControls;
	private ShieldController shieldManager;
	private PlayerMovement playerMovement;
	private Player player;

	void Start () {
		//Script definitions

		ui = transform.parent.Find("UIObjects").GetComponent<UIManager>();
		shieldManager = transform.Find("Shield").gameObject.GetComponent<ShieldController>();
		playerMovement = GetComponent<PlayerMovement>();
		player = GetComponent<Player>();

		//Instance Variables

		controls = new Dictionary<string, object>();
		lastControls = new Dictionary<string, object>();
		//If the player is in the "pause" menu
		localPause = false;

		//Initialization

		//Initialize the dictionaries
		updateControls();
		updateLastControls();
	}

	void Update(){
		//Get control keys
		updateControls();
		//Turn on the shield
		if(getInputDown("shield")) {
			shieldManager.toggleShield();
		}
			
		//Show scoreboard while key is pressed
		ui.setScoreboardVisible((bool)getInput("scoreboard"));

		//Pause Menu (Doesn't actually pause the game)
		if(getInputDown("menu")) {
			ui.togglePauseMenuVisible();
			localPause = !localPause;
		}

		//Debug key
		if (getInputDown("debug")) {
			playerMovement.debug();
			player.updateHealth(-10);
			player.updateEnergy(-15);
		}

		if((bool)getInput("changeview")) {

		}
		updateLastControls();
	}


	//Defined Methods

	//Returns the current state of the control input
	public object getInput(string input){
		return controls[input];
	}

	//Returns the last state of the control input
	public object getLastInput(string input){
		return lastControls[input];
	}

	private bool getInputDown(string input){
		return (bool)getInput(input) && !(bool)getLastInput(input);
	}

	private bool getInputUp(string input){
		return !(bool)getInput(input) && (bool)getLastInput(input);
	}

	//TODO: allow different keys to be set
	//Updates controls to the current input
	private void updateControls(){
		controls["roll"] = Input.GetAxis("Horizontal");
		controls["pitch"] = Input.GetAxis("Vertical");
		controls["yawleft"] = Input.GetKey(KeyCode.Q);
		controls["yawright"] = Input.GetKey(KeyCode.E);
		controls["incthrust"] = Input.GetKey(KeyCode.C);
		controls["decthrust"] = Input.GetKey(KeyCode.V);
		controls["shoot"] = Input.GetMouseButton(0);
		controls["debug"] = Input.GetKey(KeyCode.O);
		controls["shield"] = Input.GetKey(KeyCode.Space);
		controls["menu"] = Input.GetKey(KeyCode.Escape);
		controls["scoreboard"] = Input.GetKey(KeyCode.Tab);
		controls["changeview"] = Input.GetKey(KeyCode.R);
	}
	//Sets lastControls to controls
	private void updateLastControls(){
		foreach(var key in controls.Keys) {
			lastControls[key] = controls[key];
		}
	}
}
