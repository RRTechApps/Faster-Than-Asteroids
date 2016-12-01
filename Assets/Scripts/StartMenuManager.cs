using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class StartMenuManager : MonoBehaviour {
	//Controls behavior of the start menu.
	//Will link with the maingame scene once connected.

	private GameObject joinServerButton;
	private GameObject hostServerButton;
	private GameObject helpButton;
	private GameObject joinHostButton;
	private GameObject backButton;
	private GameObject ipInput;
	private GameObject nameInput;
	private InputField ipInputField;
	private Text joinHostButtonText;
	private string lastIPText;
	private string lastChar;

	//Initialization
	void Start () {
		joinServerButton = transform.Find("JoinServerButton").gameObject;
		hostServerButton = transform.Find("HostServerButton").gameObject;
		helpButton = transform.Find("HelpButton").gameObject;
		joinHostButton = transform.Find("JoinHostButton").gameObject;
		backButton = transform.Find("BackButton").gameObject;
		ipInput = transform.Find("IPInput").gameObject;
		nameInput = transform.Find("NameInput").gameObject;
		ipInputField = ipInput.GetComponent<InputField>();
		joinHostButtonText = joinHostButton.GetComponentInChildren<Text>();
		lastIPText = "";
		lastChar = "";
		ipInput.SetActive(false);
		nameInput.SetActive(false);
		joinHostButton.SetActive(false);
		backButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Disables all UI elements other than the ip input, name input, join button, and the back button
	public void onJoinServerButton(){
		nameInput.GetComponent<InputField>().text = "";
		ipInput.GetComponent<InputField>().text = "";
		joinHostButtonText.text = "Join";
		joinServerButton.SetActive(false);
		hostServerButton.SetActive(false);
		helpButton.SetActive(false);
		ipInput.SetActive(true);
		nameInput.SetActive(true);
		joinHostButton.SetActive(true);
		backButton.SetActive(true);
	}

	//Disables all UI elements other than the name input, host button, and the back button
	public void onHostServerButton(){
		nameInput.GetComponent<InputField>().text = "";
		ipInput.GetComponent<InputField>().text = "";
		joinHostButtonText.text = "Host";
		joinServerButton.SetActive(false);
		hostServerButton.SetActive(false);
		helpButton.SetActive(false);
		ipInput.SetActive(false);
		nameInput.SetActive(true);
		joinHostButton.SetActive(true);
		backButton.SetActive(true);
	}

	public void onHelpButton(){

	}

	public void onJoinHostButton(){
		//Don't even ask, it works (regex for valid ip)
		if(joinHostButtonText.text.Equals("Join")) {
			
			if(Regex.IsMatch(lastIPText, "(([1][0-9][0-9]|2[0-4][0-9]|25[0-5]|[0-9][0-9]?)\\.){3}([1][0-9][0-9]|2[0-4][0-9]|25[0-5]|[0-9][0-9]?)")) {
				//TODO: Connect here and change scene
			} else {
				//TODO: Display invalid IP text/image
			}
		}
	}

	//Resets UI elements to what they are by default
	public void onBackButton(){
		joinServerButton.SetActive(true);
		hostServerButton.SetActive(true);
		helpButton.SetActive(true);
		ipInput.SetActive(false);
		nameInput.SetActive(false);
		joinHostButton.SetActive(false);
		backButton.SetActive(false);
	}
	public void onIPTextChange(string text){
		string addedChar = lastIPText.Length - text.Length > 0 ? "BS" : text.Length < 1 ? "" : text.Substring(text.Length - 1);
		if(addedChar.Equals("BS")) {
			lastChar = "";
			lastIPText = text;
			return;
		}
		//Truncates any 0s and prevents invalid characters (anything other than digits and periods)
		if((lastChar.Equals("0") && addedChar.Equals("0")) || Regex.IsMatch(addedChar, "[^\\.\\d]")) {
			ipInputField.text = lastIPText;
			return;
		}
		lastChar = addedChar;
		lastIPText = text;
	}
}
