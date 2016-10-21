using UnityEngine;
using System.Collections;

public class BulletCollisionScript : MonoBehaviour
{
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Bullet")
		{
			Destroy(gameObject);
		}
	}
}