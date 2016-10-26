using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	private Text[] textObjects;
	private Image[] imageObjects;

	//Initialization
	void Start () {
		textObjects = GameObject.FindObjectsOfType<Text>();
		imageObjects = GameObject.FindObjectsOfType<Image>();
	}

	//[ClientRpc]
	//public void RpcUpdateText(string textObjName, string newText){
	public void updateText(string textObjName, string newText){
		foreach(Text textObj in textObjects) {
			if(textObj.name == textObjName) {
				textObj.text = newText;
			}
		}
	}

	//[ClientRpc]
	//public void RpcUpdateImage(string imgObjName, Material newMaterial){
	public void updateImage(string imgObjName, Material newMaterial){
		foreach(Image imgObj in imageObjects) {
			if(imgObj.name == imgObjName) {
				imgObj.material = newMaterial;
			}
		}
	}

	//[ClientRpc]
	//public void RpcUpdateImage(string imgObjName, Vector2 newSize){
	public void updateImage(string imgObjName, Vector2 newSize){
		foreach(Image imgObj in imageObjects) {
			if(imgObj.name == imgObjName) {
				imgObj.rectTransform.sizeDelta = newSize;
			}
		}
	}

	//[ClientRpc]
	//public void RpcUpdateImage(string imgObjName, Material newMaterial, Vector2 newSize){
	public void updateImage(string imgObjName, Material newMaterial, Vector2 newSize){
		foreach(Image imgObj in imageObjects) {
			if(imgObj.name == imgObjName) {
				imgObj.material = newMaterial;
				imgObj.rectTransform.sizeDelta = newSize;
			}
		}
	}
}
