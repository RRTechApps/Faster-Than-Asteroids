using UnityEngine;
using System.Collections;


public class AsteroidManager : MonoBehaviour {
	//This script should go on an empty gameObject located at the middle of the game field, and is used to manage asteroids (spawning them)
	//There should only be one instance of this script per client/server

	public int asteroidsOnField; 			//# of asteroids on the game field at all times
	public GameObject[] asteroidPrefabs;
	public Vector3 fieldRadius; 				//Used to define where the asteroids will spawn from
	public float asteroidLaunchForce; 	//Magnitude of the launch velocity (will be vectorized in code)

	private Vector3 fieldOffset;
	private Vector3 fieldMargins;

	//Initialization
	void Start () {
		asteroidsOnField = 20;
		asteroidLaunchForce = 20;
		fieldOffset = transform.position;
		fieldMargins = new Vector3(1.5f, 0.0f, 1.5f);
		AddAsteroid();
	}

	//[Command]
	//void CmdAddAsteroid(){
	//Once we need to add MP ^^
	void AddAsteroid(){
		float x, z; 			//x and/or z will be set to a edge coordinate, the one not set to the edge coordinate will have any coordinate along the edge
		if(RandBool()) {		//Used to assign x and z to noted constraints 
			x = Random.Range(-fieldRadius.x, fieldRadius.x);
			z = RandBool() ? fieldRadius.z : -fieldRadius.z;
		} else {
			x = RandBool() ? fieldRadius.x : -fieldRadius.x;
			z = Random.Range(-fieldRadius.z, fieldRadius.z);
		}
		// Random point in the gamefield that we will point the asteroid at
		Vector3 pointInField = new Vector3(Random.Range(-fieldRadius.x, fieldRadius.x), 0.0f, Random.Range(-fieldRadius.z, fieldRadius.z));
		Vector3 asteroidPosition = new Vector3(x, 0.0f, z);
		asteroidPosition.Scale(fieldMargins);
		asteroidPosition += fieldOffset;
		//Direction to point the asteroid
		Vector3 direction = pointInField - asteroidPosition;
		//Create the asteroid gameobject
		GameObject genAsteroid = (GameObject)Instantiate(asteroidPrefabs[Random.Range(0,3)], asteroidPosition, Quaternion.FromToRotation(Vector3.forward, direction), this.transform);
		//Set the size of the asteroid
		genAsteroid.GetComponent<ObjectManager>().setMagnitudeOfActionF(Random.Range(1.0f, 20.0f));
		//Add a force to the asteroid to make it go through the field
		genAsteroid.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * asteroidLaunchForce);
	}
	bool RandBool(){ 	//Returns true or false
		return Random.Range(0, 2) == 0 ? true : false;
	}
}
