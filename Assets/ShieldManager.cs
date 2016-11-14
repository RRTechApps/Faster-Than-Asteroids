using UnityEngine;
using System.Collections;

public class ShieldManager : MonoBehaviour {

	public float energy;
	void start()
	{
		energy = 100;
	}
	void onTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Asteroid"))
		{
			Debug.Log ("COLLISION");
			Rigidbody Arb = other.GetComponent<Rigidbody> ();
			Arb.AddRelativeForce (-Arb.velocity);
			energy -= 15;
		}
	}
}
