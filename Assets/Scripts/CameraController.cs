using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject Player;
	public Camera cam;
	private Rigidbody rbP;
	private Transform offset;

	void Start()
	{
		rbP = Player.GetComponent<Rigidbody>();
		offset = cam.transform;
	}
	void Update()
	{
		cam.transform.position = rbP.transform.position + new Vector3(0.0f,10.0f,0.0f);
	}



}