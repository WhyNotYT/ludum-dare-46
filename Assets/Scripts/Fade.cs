using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
	public float fadeSpeed;
	public bool fadeIn;
	public Image spriteRenderer;

	private void Update()
	{
		if(fadeIn)
		{
			spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.clear, Time.deltaTime * fadeSpeed);
		}
		else
		{
			spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, Time.deltaTime * fadeSpeed);
		}
	}
}
