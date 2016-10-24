using UnityEngine;
using System.Collections;


public class AsteroidManager : MonoBehaviour {
	//This script should go on an empty gameObject located at the middle of the game field, and is used to manage asteroids
	//There should only be one instance of this script per client/server
	//Each asteroid will have its own collider and destroyer script different to this script

	public int asteroidsOnField = 20; 			//# of asteroids on the game field at all times
	public GameObject[] asteroidPrefabs;
	public Vector3 fieldRadius; 				//Used to define where the asteroids will spawn from
	public float asteroidLaunchVelocity = 20; 	//Magnitude of the launch velocity (will be vectorized in code)

	private Vector3 fieldOffset;
	private Vector3 fieldMargins;
	private Vector3[] fieldCorners;

	// Use this for initialization
	void Start () {
		fieldOffset = transform.position;
		fieldMargins = new Vector3(1.5f, 0.0f, 1.5f);
		fieldCorners = new Vector3[] {new Vector3(-fieldRadius.x, 0.0f, fieldRadius.z), new Vector3(fieldRadius.x, 0.0f, fieldRadius.z), new Vector3(-fieldRadius.x, 0.0f, -fieldRadius.z), new Vector3(fieldRadius.x, 0.0f, -fieldRadius.z)};
		AddAsteroid();
	}

	void Loop(){
	}

	//[Command]
	//void CmdAddAsteroid(){
	void AddAsteroid(){
		float x, z; 			//x and/or z will be set to a edge coordinate, the one not set to the edge coordinate will have any coordinate along the edge
		Vector3 asteroidAngle;	//asteroidAngle will be any angle allowing the asteroid to travel through the field (bounds are 2 edges)
		if(RandBool()) {		//Used to assign x and z to noted constraints 
			x = Random.Range(-fieldRadius.x, fieldRadius.x);
			z = RandBool() ? fieldRadius.z : -fieldRadius.z;
		} else {
			x = RandBool() ? fieldRadius.x : -fieldRadius.x;
			z = Random.Range(-fieldRadius.z, fieldRadius.z);
		}
		//Point in the gamefield that we will transform.LookAt
		Vector3 pointInField = new Vector3(Random.Range(-fieldRadius.x, fieldRadius.x), 0.0f, Random.Range(-fieldRadius.z, fieldRadius.z));
		//Where the asteroid is
		Vector3 asteroidPosition = new Vector3(x, 0.0f, z);
		//Scale the asteroid to the margins
		asteroidPosition.Scale(fieldMargins);
		//Add on any offset from the origin
		asteroidPosition += fieldOffset;
		//Create the asteroid gameobject
		GameObject genAsteroid = (GameObject)Instantiate(asteroidPrefabs[Random.Range(0,3)], asteroidPosition, Quaternion.Euler(Vector3.zero), this.transform);
		//Point to the gamefield
		genAsteroid.transform.LookAt(pointInField);
		//Add a force to the asteroid to make it go through the field
		genAsteroid.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * asteroidLaunchVelocity);
	}
	bool RandBool(){ 	//Returns true or false
		return Random.Range(0, 2) == 0 ? true : false;
	}
}
