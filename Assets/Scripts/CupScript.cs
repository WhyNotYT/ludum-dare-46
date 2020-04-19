using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScript : MonoBehaviour
{
	public bool Cup;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.name == "Babe")
		{
			if(FindObjectOfType<Controller>().target == this.transform)
			{
				if (Cup)
				{
					FindObjectOfType<Controller>().PickedUpCup = true;
					FindObjectOfType<Controller>().target = null;
					FindObjectOfType<Controller>().UwU.Play();
				}
				else
				{
					FindObjectOfType<Controller>().PickedUpKnife = true;
					FindObjectOfType<Controller>().target = null;
					FindObjectOfType<Controller>().UwU.Play();
				}
			}
		}
	}
}
