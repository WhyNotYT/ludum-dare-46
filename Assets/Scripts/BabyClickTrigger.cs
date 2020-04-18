using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyClickTrigger : MonoBehaviour
{
	private Controller controller;


	private void Start()
	{
		controller = FindObjectOfType<Controller>();
	}


	private void OnMouseDown()
	{
		controller.buttonClick();
	}
}
