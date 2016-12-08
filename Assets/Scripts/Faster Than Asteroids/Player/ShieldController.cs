using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {
	
	private bool active;
	private bool touchingAsteroids;
	private Player player;

	void Start()
	{
		//Scripts

		this.player = this.transform.parent.GetComponent<Player>();

		//Instance Variables

		this.active = false;

		//Initialization

		this.gameObject.SetActive(this.active);
	}

	void Update(){
		if(active) {
			this.player.queueEnergy(Time.deltaTime);
		}
	}

	//Defined Methods

	public void asteroidCollision(GameObject asteroid, float size){
		//TODO: Generate an explosion
		Destroy(asteroid);
		this.player.updateEnergy(-Mathf.RoundToInt(size));
	}

	public void toggleShield(){
		this.gameObject.SetActive(this.active = !this.active);
	}
}