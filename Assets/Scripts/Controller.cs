
using UnityEngine;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{

	public currentOverlap overlap;


	public Rigidbody2D baby;
	public float moveSpeed = 1;
	public Transform CupPlace;
	public Transform KnifePlace;
	public bool carrying;
	public bool playing;
	public GameObject wannaPoop;
	public float MessAmount;

	private Camera mainCam;
	public bool invalidBabyPos;
	public Vector3 LastValidPos;
	public bool canMove;
	public Transform[] waypoints;
	public Transform target;
	public Animator anim;
	public bool crying;
	public float NoiseAmount;
	public Image noiseBar;
	public Image messBar;
	public float NoiseIncreaseRate;
	public GameObject PoopPrefab;
	public GameObject BloodPrefab;
	public GameObject BrokenCupPrefab;

	public bool PoopComming;

	public float PoopCounter;
	public float TimeoutCounter;
	public float RandomFallTimeCounter;
	public float playTimeCounter;
	public float cooldownCounter;
	public bool PickedUpKnife;
	public bool PickedUpCup;
	public GameObject KnifeInHand;
	public GameObject CupInHand;


	public void buttonClick()
	{

		Debug.Log("Button Click");
		if (!carrying)
		{
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("FallOver"))
			{
				if (cooldownCounter < Time.time)
				{

					if(PickedUpKnife)
					{
						Debug.Log("Knife");
						KnifePlace.transform.position = baby.transform.position + new Vector3(0, -0.5f, 1);
						PickedUpKnife = false;
						KnifeInHand.SetActive(false);
					}
					else if(PickedUpCup)
					{
						Debug.Log("Cup");
						CupPlace.transform.position = baby.transform.position + new Vector3(0, -0.5f, 1);
						PickedUpCup = false;
						CupInHand.SetActive(false);
					}
					carrying = true;
					cooldownCounter = Time.time + 0.2f;
				}
			}
		}
	}


	private void Start()
	{
		mainCam = Camera.main;
		PoopCounter = Time.time + Random.Range(10, 30);
	}

	public void Poop()
	{
		Debug.Log("Poop Called");
		wannaPoop.SetActive(false);
		PoopComming = false;
		canMove = true;
		anim.SetBool("Walking", true);

		Instantiate(PoopPrefab, baby.transform.position + new Vector3(0, -0.5f, 6), Quaternion.identity);
		PoopCounter = Time.time + Random.Range(10, 30);

	}



	private void Update()
	{
		messBar.fillAmount = Mathf.Clamp01(MessAmount);

		if(PickedUpCup)
		{
			CupInHand.SetActive(true);
			CupPlace.position = new Vector3(-100, 100);
		}
		else if(PickedUpKnife)
		{
			KnifeInHand.SetActive(true);
			KnifePlace.position = new Vector3(-100, 100);
		}


		anim.SetBool("Walking", canMove);


		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Cry"))
		{
			NoiseAmount += NoiseIncreaseRate * Time.deltaTime;
			noiseBar.fillAmount = Mathf.Clamp01(NoiseAmount);
		}
		else
		{
			NoiseAmount = Mathf.Clamp01(NoiseAmount - NoiseIncreaseRate * Time.deltaTime * 0.1f);
			noiseBar.fillAmount = Mathf.Clamp01(NoiseAmount);

		}
		if (NoiseAmount > 1 || MessAmount > 1)
		{
			Debug.Log("GameOver");
		}
		//Debug.Log(Time.time);
		if(PoopCounter < Time.time + 3)
		{
			wannaPoop.SetActive(true);
			PoopComming = true;

			if (PoopComming)
			{
				if (PoopCounter < Time.time)
				{
					if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
					{
						canMove = false;
						anim.Play("Poop");
						PoopComming = false;
					}
					else
					{
						PoopCounter += 0.5f;
					}
				}
			}
		}
		else
		{
			wannaPoop.SetActive(false);
		}

		if(playing)
		{
			if(playTimeCounter + 0.05f < Time.time)
			{
				canMove = true;
				playing = false;

				if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
				{
					anim.Play("Walk");
				}

			}

			crying = false;
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Play"))
			{
				anim.Play("Play");
			}
		}

		if (carrying)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
			{
				anim.Play("BabySit");
			}

			canMove = false;
			Vector3 targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
			targetPos.z = 0;
			baby.transform.position = targetPos;

			if (Input.GetMouseButtonDown(0))
			{
				if (cooldownCounter < Time.time)
				{

					if (invalidBabyPos)
					{
						carrying = false;
						if (anim.GetCurrentAnimatorStateInfo(0).IsName("BabySit"))
						{
							canMove = true;
						}
						baby.transform.position = LastValidPos;
						cooldownCounter = Time.time + 0.2f;
						Debug.Log("invalidPos");
						if ((baby.transform.position - LastValidPos).sqrMagnitude > 1f)
						{
							RandomFallTimeCounter = 0;
						}
						else
						{
							RandomFallTimeCounter = Time.time + Random.Range(0f, 5f);
						}
					}
					else
					{
						if (anim.GetCurrentAnimatorStateInfo(0).IsName("BabySit"))
						{
							canMove = true;
						}
						carrying = false;
						cooldownCounter = Time.time + 0.2f;
						Debug.Log("validPos");
						RandomFallTimeCounter = Time.time + Random.Range(0f, 5f);
					}

					if (overlap == currentOverlap.playPlace)
					{
						if (anim.GetCurrentAnimatorStateInfo(0).IsName("Cry"))
						{
							playing = true;
							playTimeCounter = Time.time + 5;
							canMove = false;
						}
					}
					else if(overlap == currentOverlap.ToiletDoor)
					{
						if(PoopComming)
						{
							PoopComming = false;
							PoopCounter = Time.time + Random.Range(10, 30);
							canMove = true;
							anim.Play("Walk");
						}
					}
				}
			}
		}
		else
		{
			if (canMove)
			{
				if (target != null)
				{
					Vector3 targetDelta = (target.position - baby.transform.position);
					targetDelta.z = 0;
					baby.velocity = targetDelta.normalized * moveSpeed;
					anim.SetBool("Walking", true);

					

					if (targetDelta.sqrMagnitude < 1)
					{
						target = null;
					}

					if (TimeoutCounter < Time.time)
					{
						target = null;
					}

					if(RandomFallTimeCounter < Time.time)
					{
						if(PickedUpKnife)
						{
							Instantiate(BloodPrefab , baby.transform.position + new Vector3(0, -0.5f, 1), Quaternion.identity);
							KnifeInHand.SetActive(false);
							PickedUpKnife = false;
							KnifePlace.position = baby.transform.position + new Vector3(0, -0.5f, 0);
						}
						else if(PickedUpCup)
						{
							Instantiate(BrokenCupPrefab, baby.transform.position + new Vector3(0, -0.5f, 1), Quaternion.identity);
							CupInHand.SetActive(false);
							PickedUpCup = false;
							CupPlace = null;
							CupPlace.position = new Vector3(-100, 100);
						}
						anim.Play("FallOver");

						target = null;

						canMove = false;
						
					}

				}
				else
				{

					int a = Random.Range(-1, 5);

					if (a == 0)
					{
						if (!PickedUpCup && !PickedUpKnife)
						{
							target = KnifePlace;
						}
					}
					else if (a == 1)
					{
						if (!PickedUpCup && !PickedUpKnife)
						{
							if (CupPlace != null)
							{
								target = CupPlace;
							}	
						}
					}
					else
					{
						target = waypoints[Random.Range(0, waypoints.Length)];
					}

					Debug.Log("ReLocating Target");
					TimeoutCounter = Time.time + 2;
					RandomFallTimeCounter = Time.time + Random.Range(1f, 5f);
				}
			}
		}
	}
}