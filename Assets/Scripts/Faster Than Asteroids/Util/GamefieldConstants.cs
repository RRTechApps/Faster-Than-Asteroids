using UnityEngine;
using System.Collections;

public class GamefieldConstants : MonoBehaviour {
	private Vector3 gameFieldRadius;
	private Vector3 gameFieldMargins;
	private Vector3 gameFieldOffset;

	// Use this for initialization
	void Start () {
		//Since it's a plane, the plane scale * 10 is the normal scale, radius is half
		this.gameFieldRadius = this.transform.lossyScale * 5;
		this.gameFieldMargins = new Vector3(1.0f, 0.0f, 1.0f) * (this.gameFieldRadius.magnitude / 20.0f);
		this.gameFieldOffset = this.transform.position;
		this.gameFieldOffset.Scale(new Vector3(1.0f, 0.0f, 1.0f));
	}

	//Defined Methods

	public Vector3 getGameFieldRadius(){
		return this.gameFieldRadius;
	}

	public Vector3 getGameFieldMargins(){
		return this.gameFieldMargins;
	}

	public Vector3 getGameFieldOffset(){
		return this.gameFieldOffset;
	}
}
