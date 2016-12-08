using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	//This script belongs on the PlayerModel gameobject and controls the player


	//[SyncVar(hook=onHealthChange)]
	private int health;
	//[SyncVar(hook=onEnergyChange)]
	private int energy;
	private int maxHealth;
	private int maxEnergy;
	//[SyncVar(hook=onScoreChange)]
	private int score;
	private float queuedEnergy;
	private string playerName;
	private Color team;
	private UIManager ui;
	private PlayerControls controls;
	private PlayerMovement playerMovement;


	void Start () {
		//Script Definitions

		controls = GetComponent<PlayerControls>();
		playerMovement = GetComponent<PlayerMovement>();
		ui = transform.Find("UIObjects").GetComponent<UIManager>();

		//Instance Variables

		health = 100;
		energy = 100;
		maxHealth = 100;
		maxEnergy = 100;
		score = 0;
		playerName = "Debgger";

		//Initialization

		ui.addScoreboardEntry(playerName, Color.red);
	}

	//Defined Methods

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

	/*
	 * These are for multiplayer support
	void onHealthChange(){

	}

	void onEnergyChange(){

	}
	
	void onScoreChange(){

	}*/

	public void asteroidCollision(int magnitude){
		//Make the player blow up if asteroid big enough?
	}

	public void bulletCollision(int magnitude){
		//Make the player lose energy and health and/or degrade ship quality?
	}
}
