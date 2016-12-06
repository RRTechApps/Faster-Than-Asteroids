using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private CurrentViews currentView; 
	private Camera gameCamera;
	private Transform player;

	public enum CurrentViews{
		TOPDOWN, FIRSTPERSON, THIRDPERSON
	}

	void Start () {
		//Instance Variables
		this.player = this.transform.parent.Find("PlayerModel");
		this.gameCamera = this.GetComponent<Camera>();
		this.currentView = CurrentViews.TOPDOWN;
	}
	
	void Update () {
		switch(this.currentView) {
			case CurrentViews.TOPDOWN:
				this.transform.position = player.position + new Vector3(0.0f, 10.0f, 0.0f);
				this.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
				break;
			case CurrentViews.FIRSTPERSON:
				this.transform.position = player.position + new Vector3(0.0f, 0.0f, 1.0f);
				this.transform.eulerAngles = Vector3.zero;
				break;
			case CurrentViews.THIRDPERSON:

				break;
		}
	}

	public void setView(CurrentViews view){
		this.currentView = view;
	}
}
