using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {
	
	private string objectType;
	private int magnitudeOfAction;

	//Initialization
	void Start () {
		objectType = this.tag;
	}

	//Set the magnitude of what the object attached is supposed to do
	public void setMagnitudeOfAction(int magnitude){
		magnitudeOfAction = magnitude;
	}

	//Do what the object attached is supposed to do
	public void performObjectTask(GameObject target, bool destroy){
		switch(objectType) {
			case "HPCol":
				target.GetComponent<PlayerManager>().updateHealth(magnitudeOfAction);
				break;
			case "EnergyCol":
				target.GetComponent<PlayerManager>().updateEnergy(magnitudeOfAction);
				break;
			case "Asteroid":
				target.GetComponent<PlayerManager>().asteroidCollision(magnitudeOfAction);
				break;
			case "Bullet":
				target.GetComponent<PlayerManager>().bulletCollision(magnitudeOfAction);
				break;
		}
		if(destroy) {
			Destroy(this);
		}
	}
}
