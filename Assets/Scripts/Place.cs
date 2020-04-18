using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum currentOverlap
{
    None, playPlace, ToiletDoor
}
public class Place : MonoBehaviour
{
	public currentOverlap overlap;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "Babe")
		{
			FindObjectOfType<Controller>().overlap = overlap;
		}
	}


	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.name == "Babe")
		{
			FindObjectOfType<Controller>().overlap = currentOverlap.None;
		}
	}
}
