using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallOnParent : MonoBehaviour
{

	public void callPoop()
	{

		FindObjectOfType<Controller>().Poop();
	}
}
