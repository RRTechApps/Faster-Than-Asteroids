using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {
	
	private string objectType;
	private int magnitudeOfAction;
	private float magnitudeOfActionF;
	private Rigidbody rb;

	//Initialization
	void Start () {
		objectType = this.tag;
		rb = this.GetComponent<Rigidbody>();
		if(objectType == "Asteroid") {
			Vector3 scaleBy = Vector3.one * (this.magnitudeOfActionF / 5.0f);
			this.transform.localScale.Scale(scaleBy);
		}
	}

	void OnTriggerEnter(Collider other){
		performObjectTask(other.gameObject);
	}

	//Set the magnitude of what the object attached is supposed to do
	public void setMagnitudeOfAction(int magnitude){
		magnitudeOfAction = magnitude;
	}

	//Set the magnitude of what the object attached is supposed to do
	public void setMagnitudeOfActionF(float magnitude){
		magnitudeOfActionF = magnitude;
	}

	//Do what the object attached is supposed to do
	public void performObjectTask(GameObject target){
		if(target.tag == "Player") {
			switch(objectType) {
				case "HPCol":
					target.GetComponent<PlayerManager>().updateHealth(magnitudeOfAction);
					Destroy(this.gameObject);
					break;
				case "EnergyCol":
					target.GetComponent<PlayerManager>().updateEnergy(magnitudeOfAction);
					Destroy(this.gameObject);
					break;
				case "Asteroid":
					target.GetComponent<PlayerManager>().asteroidCollision(magnitudeOfAction);
					break;
				case "Bullet":
					target.GetComponent<PlayerManager>().bulletCollision(magnitudeOfAction);
					break;
			}
		} else if(target.tag == "Bullet") {
			switch(objectType) {
				case "HPCol":
					//Make an explosion and destroy target and self
					break;
				case "EnergyCol":
					//Make an explosion and destroy target and self
					break;
				case "Asteroid":
					//Make an explosion and spawn a few boxes depending on size of asteroid
					GameObject.Find("Collectables").GetComponent<CollectableManager>().spawnBoxes((int)magnitudeOfActionF / 3, transform.position);
					break;
				case "Bullet":
					//Make an explosion and destroy target and self
					break;
			}
		}
	}
}
