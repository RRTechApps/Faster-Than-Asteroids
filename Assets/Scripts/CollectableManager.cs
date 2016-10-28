using UnityEngine;
using System.Collections;

public class CollectableManager : MonoBehaviour {
	//This script should go on an empty gameObject located in the middle of the game field, and is used to manage collectables ([re]spawning them)
	//There should only be one instance of this script per client/server

	//Type of collectable
	public enum Types {HP, Energy};	

	public GameObject hpBoxPrefab;
	public GameObject energyBoxPrefab;

	//Amt of health/energy to give, picked randomly with bias
	private int[] boxAmounts = {5, 10, 25, 50};
	//Chance per each box amount to be spawned
	private int[] boxAmountsChance = {30, 40, 20, 10};
	//# boxes per each type to be on field
	private int[] boxesOnField = {20, 20}; 
	private float respawnTime;
	private Vector3 fieldOffset;
	private Vector3 fieldMargins;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Defined functions

	void addBox(Types type, int amt){

	}
}