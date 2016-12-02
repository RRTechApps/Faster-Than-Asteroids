using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {
	//This script belongs on every spawned prefab and controls its collider
	
	private string objectType;
	private int magnitudeOfAction;
	private float magnitudeOfActionF;
	private bool destroyOnExit;
	private Vector3 gameFieldRadius;
	private AsteroidManager asteroidManager;
	private CollectableManager collectableManager;

	//Initialization
	void Start(){
		objectType = this.tag;
		destroyOnExit = true;
		if(objectType.Equals("Asteroid")) {
			Vector3 scaleBy = Vector3.one * (this.magnitudeOfActionF / 5.0f);
			this.transform.localScale.Scale(scaleBy);
		}
		gameFieldRadius = GameObject.Find("GameField").GetComponent<GameFieldHelper>().getGameFieldRadius();
		asteroidManager = GameObject.Find("Asteroids").GetComponent<AsteroidManager>();
		collectableManager = GameObject.Find("Collectables").GetComponent<CollectableManager>();
	}

	//For the destroyOnExit flag
	void Update(){
		if(destroyOnExit) {
			float x, z;
			x = this.transform.position.x;
			z = this.transform.position.z;
			//If the position of this object is outside the radius of the game field
			if(gameFieldRadius.x - Mathf.Abs(x) < 0 || gameFieldRadius.z - Mathf.Abs(z) < 0) {
				Destroy(this.gameObject);
				if(this.objectType.Equals("Asteroid")) {
					asteroidManager.AddAsteroid();
				}
			}
		}
	}

	void OnTriggerEnter(Collider other){
		performObjectTaskEnter(other.gameObject);
	}

	void OnTriggerExit(Collider other){
		performObjectTaskExit(other.gameObject);
	}

	//Set the magnitude of what the object attached is supposed to do
	public void setMagnitudeOfAction(int magnitude){
		magnitudeOfAction = magnitude;
	}

	//Set the magnitude of what the object attached is supposed to do
	public void setMagnitudeOfActionF(float magnitude){
		magnitudeOfActionF = magnitude;
	}

	public void setDestroyOnExit(bool destroy){
		destroyOnExit = destroy;
	}

	//Do what the object attached is supposed to do
	public void performObjectTaskEnter(GameObject target){
		if(target.tag.Equals("Player")){
			PlayerManager pm = target.GetComponent<PlayerManager>();
			switch(objectType) {
				case "HPCol":
					pm.updateHealth(magnitudeOfAction);
					Destroy(this.gameObject);
					break;
				case "EnergyCol":
					pm.updateEnergy(magnitudeOfAction);
					Destroy(this.gameObject);
					break;
				case "Asteroid":
					pm.asteroidCollision(magnitudeOfAction);
					break;
				case "Bullet":
					pm.bulletCollision(magnitudeOfAction);
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
					collectableManager.spawnBoxes((int)magnitudeOfActionF / 4, transform.position);
					asteroidManager.AddAsteroid();
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
			ShieldManager shield = target.gameObject.GetComponent<ShieldManager>();
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
