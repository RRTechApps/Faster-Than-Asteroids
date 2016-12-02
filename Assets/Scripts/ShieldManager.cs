using UnityEngine;
using System.Collections;

public class ShieldManager : MonoBehaviour {
	private PlayerManager playerManager;
	private bool active;
	private bool touchingAsteroids;

	public GameObject player;

	void Start()
	{
		playerManager = player.GetComponent<PlayerManager>();
		active = false;
		this.gameObject.SetActive(active);
	}

	void Update(){
		if(active) {
			//Debug.Log("lost energy amt: " + Time.deltaTime);
			playerManager.queueEnergy(Time.deltaTime);
		}
	}

	public void asteroidCollision(GameObject asteroid, float size){
		//TODO: Generate an explosion
		Destroy(asteroid);
		playerManager.updateEnergy(-Mathf.RoundToInt(size));
	}

	public void toggleShield(){
		this.gameObject.SetActive(active = !active);
	}
		
//	void onTriggerEnter(Collider other)
//	{
//		if (other.gameObject.CompareTag("Asteroid"))
//		{
//			Debug.Log ("COLLISION");
//			Rigidbody Arb = other.GetComponent<Rigidbody> ();
//			Arb.AddRelativeForce (-Arb.velocity);
//			playerManager.updateEnergy(-15);
//		}
//	}
}
