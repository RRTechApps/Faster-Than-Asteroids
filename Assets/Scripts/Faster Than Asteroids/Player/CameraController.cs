using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private CurrentViews currentView; 
	private Camera gameCamera;
	private Transform player;
	private Space playerSpace;

	public enum CurrentViews{
		TOPDOWN, FIRSTPERSON, THIRDPERSON
	}

	void Start () {
		//Instance Variables
		this.player = this.transform.parent.Find("PlayerModel");
		this.gameCamera = this.GetComponent<Camera>();
		this.currentView = CurrentViews.TOPDOWN;
		this.playerSpace = player.GetComponent<Player>().getSpace();
	}

	//TODO: Make this rotate with the player...
	void Update () {
		switch(this.currentView) {
			case CurrentViews.TOPDOWN:
				this.transform.position = player.position + new Vector3(0.0f, 10.0f, 0.0f);
				this.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
				this.transform.RotateAround(player.position, Vector3.up, player.eulerAngles.y);
				break;
			case CurrentViews.FIRSTPERSON:
				this.transform.position = player.position + new Vector3(0.0f, 0.0f, 1.0f);
				this.transform.RotateAround(player.position, Vector3.up, player.eulerAngles.y);
				this.transform.LookAt(player.Find("Shield"));
				break;
			case CurrentViews.THIRDPERSON:
				this.transform.position = player.position + new Vector3(0.0f, 2.0f, -5.0f);
				this.transform.RotateAround(player.position, Vector3.up, player.eulerAngles.y);
				this.transform.LookAt(player);
				break;
		}
	}

	public void setView(CurrentViews view){
		this.currentView = view;
	}

	public void toggleView(){
		switch(this.currentView) {
			case CurrentViews.TOPDOWN:
				currentView = CurrentViews.FIRSTPERSON;
				break;
			case CurrentViews.FIRSTPERSON:
				currentView = CurrentViews.THIRDPERSON;
				break;
			case CurrentViews.THIRDPERSON:
				currentView = CurrentViews.TOPDOWN;
				break;
		}
	}
}
