using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	//scoreTextBox is the actual instance of the textbox itself
	Text scoreTextBox;
	//score is the actual score of the player (in integer form)
	int score;
	//what will be placed inside of scoreTextBox
	string text;

	// Use this for initialization
	void Start () {
		score = 0;
		text = "Score: " + score;
		scoreTextBox = this.GetComponent<Text> ();
	}

	//call whenever an asteroid is destroyed (from object manager)
	public void asteroidDestroyed(){
		//score increases, text is reassigned and then scoreTextBox is updated
		score++;
		text = "Score: " + score;
		scoreTextBox.text = text;
	}
}
