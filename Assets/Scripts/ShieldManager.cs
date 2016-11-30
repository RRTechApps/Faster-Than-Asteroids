using UnityEngine;
using System.Collections;

public class ShieldManager : MonoBehaviour {
	private PlayerManager playerManager;

	public GameObject player;

	void Start()
	{
		//playerManager = player.GetComponent<PlayerManager>();
	}

	public void asteroidCollision(GameObject asteroid){
		asteroid.GetComponent<Rigidbody>().velocity *= -0.8f;
	}

	public void toggleShield(){

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
