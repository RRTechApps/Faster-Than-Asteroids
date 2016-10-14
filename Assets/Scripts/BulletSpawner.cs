using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {
	public GameObject Bullet;
	public GameObject Spawner;
	private Rigidbody lmayyyyyyyyyo;
	public float sped;
	// Use this for initialization
	void Start () {
		lmayyyyyyyyyo = Bullet.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			Instantiate(Bullet, Spawner.transform.position, Quaternion.identity);
			lmayyyyyyyyyo.velocity = transform.TransformDirection(new Vector3(0.0f,0.0f,sped));
		}
	}
}
