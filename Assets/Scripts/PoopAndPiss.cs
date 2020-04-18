using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopAndPiss : MonoBehaviour
{

	private Color startColor;
	private float MessAtStart;
	private void OnMouseDown()
	{
		this.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, Color.clear, (FindObjectOfType<Controller>().MessAmount - MessAtStart) / MessToAdd);
		FindObjectOfType<Controller>().MessAmount -= MessToAdd * 0.2f;
	}



	public float MessToAdd = 0.2f;
	private void Start()
	{
		startColor = this.GetComponent<SpriteRenderer>().color;
		MessAtStart = FindObjectOfType<Controller>().MessAmount;

		FindObjectOfType<Controller>().MessAmount += MessToAdd;
	}
}
