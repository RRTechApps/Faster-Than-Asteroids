using UnityEngine;
using System.Collections;

public class CollectableManager : MonoBehaviour {
	//This script should go on an empty gameObject located in the middle of the game field, and is used to manage collectables ([re]spawning them)
	//There should only be one instance of this script per client/server

	public GameObject hpBoxPrefab;
	public GameObject energyBoxPrefab;

	private int hpBoxesOnField = 20;
	private int energyBoxesOnField = 20;
	private int[] boxAmounts = { 5, 10, 25, 50 };
	private Vector3 fieldOffset;
	private Vector3 fieldMargins;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
