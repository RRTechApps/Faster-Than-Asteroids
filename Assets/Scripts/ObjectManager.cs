using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {
	
	private string objectType;
	private int magnitudeOfAction;
	private Rigidbody rb;

	//Initialization
	void Start () {
		objectType = this.tag;
		rb = this.GetComponent<Rigidbody>();
	}

	//Set the magnitude of what the object attached is supposed to do
	public void setMagnitudeOfAction(int magnitude){
		magnitudeOfAction = magnitude;
	}

	//Do what the object attached is supposed to do
	public void performObjectTask(GameObject target){
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
	}
}
