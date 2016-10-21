using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectable : MonoBehaviour {

	public int score;
	public Text scoreUI;

	void OnTriggerEnter(Collider other){
		
		if (other.tag == "Collectable") {
			Debug.Log ("Collision");
			Destroy (other.gameObject);
			score++;
		}

		scoreUI.text = "Score: " + score.ToString ();

	}


}
