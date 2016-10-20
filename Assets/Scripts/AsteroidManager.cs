using UnityEngine;
using System.Collections;

public class AsteroidManager : MonoBehaviour {
	//This script should go on an empty gameObject located at the middle of the game field, and is used to manage asteroids
	//There should only be one instance of this script per client/server
	//Each asteroid will have its own collider script different to this script

	public int asteroidsOnField = 20; 		//# of asteroids on the game field at all times
	public GameObject asteroidPrefab1;
	public GameObject asteroidPrefab2;
	public GameObject asteroidPrefab3;
	public Vector3 fieldRadius; 			//Used to define where the asteroids will spawn from
	public float asteroidLaunchVelocity = 20; 	//Magnitude of the launch velocity (will be vectorized in code)

	private Vector3 fieldOffset;


	// Use this for initialization
	void Start () {
		fieldOffset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
