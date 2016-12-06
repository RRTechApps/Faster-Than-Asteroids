using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {
	//This script belongs on every spawned prefab and controls its collider
	
	private string objectType;
	private int magnitudeOfAction;
	private float magnitudeOfActionF;
	private bool destroyOnExit;
	private Vector3 gamefieldRadius;
	private AsteroidManager asteroidManager;
	private CollectableManager collectableManager;

	//Initialization
	void Start(){
		this.objectType = this.tag;
		this.destroyOnExit = true;
		if(this.objectType.Equals("Asteroid")) {
			Vector3 scaleBy = Vector3.one * (this.magnitudeOfActionF / 5.0f);
			this.transform.localScale.Scale(scaleBy);
		}
		this.gamefieldRadius = GameObject.Find("GameField").GetComponent<GamefieldConstants>().getGameFieldRadius();
		this.asteroidManager = GameObject.Find("Asteroids").GetComponent<AsteroidManager>();
		this.collectableManager = GameObject.Find("Collectables").GetComponent<CollectableManager>();
	}

	//For the destroyOnExit flag
	void Update(){
		if(this.destroyOnExit) {
			float x = this.transform.position.x;
			float z = this.transform.position.z;
			//If the position of this object is outside the radius of the game field
			if(this.gamefieldRadius.x - Mathf.Abs(x) < 0 || this.gamefieldRadius.z - Mathf.Abs(z) < 0) {
				Destroy(this.gameObject);
				if(this.objectType.Equals("Asteroid")) {
					this.asteroidManager.AddAsteroid();
				}
			}
		}
	}

	//Defined Methods

	void OnTriggerEnter(Collider other){
		this.performObjectTaskEnter(other.gameObject);
	}

	void OnTriggerExit(Collider other){
		this.performObjectTaskExit(other.gameObject);
	}

	//Set the magnitude of what the object attached is supposed to do
	public void setMagnitudeOfAction(int magnitude){
		this.magnitudeOfAction = magnitude;
	}

	//Set the magnitude of what the object attached is supposed to do
	public void setMagnitudeOfActionF(float magnitude){
		this.magnitudeOfActionF = magnitude;
	}

	public void setDestroyOnExit(bool destroy){
		this.destroyOnExit = destroy;
	}

	//Do what the object attached is supposed to do
	public void performObjectTaskEnter(GameObject target){
		if(target.tag.Equals("Player")){
			Player player = target.GetComponent<Player>();
			switch(objectType) {
				case "HPCol":
					player.updateHealth(this.magnitudeOfAction);
					Destroy(this.gameObject);
					break;
				case "EnergyCol":
					player.updateEnergy(this.magnitudeOfAction);
					Destroy(this.gameObject);
					break;
				case "Asteroid":
					player.asteroidCollision(this.magnitudeOfAction);
					break;
				case "Bullet":
					player.bulletCollision(this.magnitudeOfAction);
					break;
			}
		} else if(target.tag.Equals("Bullet")){
			switch(objectType) {
				case "HPCol":
					//TODO: Make an explosion and destroy target and self
					break;
				case "EnergyCol":
					//TODO: Make an explosion and destroy target and self
					break;
				case "Asteroid":
					//TODO: Make an explosion and spawn a few boxes depending on size of asteroid
					this.collectableManager.spawnBoxes((int)this.magnitudeOfActionF / 4, this.transform.position);
					this.asteroidManager.AddAsteroid();
					Destroy(this.gameObject);

					break;
				case "Bullet":
					//TODO: Make an explosion and destroy target and self
					break;
				case "Unassigned":
					break;
			}
		} else if(target.tag.Equals("Shield")) {
			Debug.Log("Shield Collide");
			ShieldController shield = target.gameObject.GetComponent<ShieldController>();
			switch(objectType) {
				case "Asteroid":
					shield.asteroidCollision(this.gameObject, this.magnitudeOfActionF);
					break;
			}
		}
	}
	public void performObjectTaskExit(GameObject target){
		
	}
}
