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


	// Use this for initialization
	void Start () {
		fieldOffset = transform.position;

	}
	
	void AddAsteroid(){
		//
		float x, z; //x and/or z will be set to a corner value, the one not set to the corner value will have any value on the edge
		Vector3 asteroidPosition = new Vector3(x, 0.0f, z) * fieldRadius + fieldOffset;
		Vector3 asteroidAngle;	//asteroidAngle will be any angle allowing the asteroid to travel through the field (bounds are 2 edges)
		GameObject genAsteroid = Instantiate(asteroidPrefabs[Random.Range(0,3)], asteroidPosition, Quaternion.Euler(), this.transform);
		//Vector3.Angle(genAsteroid.transform.position, 
		//genAsteroid.GetComponent<Rigidbody>()
	}
}
