using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.collider.name);
		if (collision.collider.name == "Player")
		{
			FindObjectOfType<GameOverManager>().GameOver(this.transform.position);


		}
	}
}
