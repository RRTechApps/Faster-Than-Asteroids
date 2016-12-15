using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Text.RegularExpressions;

public class GameMenu : MonoBehaviour {
	//Controls behavior of the start menu.
	//Will link with the maingame scene once connected.
	//TODO: this is spaghetti code...

	private GameObject joinServerButton;
	private GameObject hostServerButton;
	private GameObject helpButton;
	private GameObject joinHostButton;
	private GameObject backButton;
	private GameObject ipInput;
	private GameObject nameInput;
	private NetworkManager networkManager;
	private InputField ipInputField;
	private InputField nameInputField;
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
		networkManager = GameObject.Find("Networking").GetComponent<NetworkManager>();
		ipInputField = ipInput.GetComponent<InputField>();
		nameInputField = nameInput.GetComponent<InputField>();
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
		nameInputField.text = "";
		ipInputField.text = "";
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
		nameInputField.text = "";
		ipInputField.text = "";
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
		if(nameInputField.text.Equals("")) {
			//TODO: Invalid name handling
			return;
		}
		if(joinHostButtonText.text.Equals("Join")) {
			
			if(Regex.IsMatch(lastIPText, "(([1][0-9][0-9]|2[0-4][0-9]|25[0-5]|[0-9][0-9]?)\\.){3}([1][0-9][0-9]|2[0-4][0-9]|25[0-5]|[0-9][0-9]?)")) {
				networkManager.networkAddress = lastIPText;
				networkManager.networkPort = 7777;
				networkManager.StartClient();
			} else {
				//TODO: Display invalid IP text/image
			}
		} else {
			//Start the server
			networkManager.StartHost();
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
	public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		var player = (GameObject)GameObject.Instantiate(networkManager.playerPrefab, Vector3.zero, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		player.GetComponent<Player>().updateName(nameInputField.text);
	}
}
