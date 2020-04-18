
using UnityEngine;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{

	public currentOverlap overlap;


	public Rigidbody2D baby;
	public float moveSpeed = 1;
	public Transform PissPlace;
	public Transform KnifePlace;
	public Transform FallPlace;
	public bool carrying;
	public bool playing;

	private Camera mainCam;
	private float cooldownCounter;
	public bool invalidBabyPos;
	public Vector3 LastValidPos;
	public bool canMove;
	public Transform[] waypoints;
	public Transform target;
	public Animator anim;
	public bool crying;
	public float NoiseAmount;
	public Image noiseBar;
	public float NoiseIncreaseRate;

	private float TimeoutCounter;
	private float RandomFallTimeCounter;
	private float playTimeCounter;

	public void buttonClick()
	{

		Debug.Log("Button Click");
		if (!carrying)
		{
			if (cooldownCounter < Time.time)
			{
				carrying = true;
				cooldownCounter = Time.time + 0.2f;
			}
		}
	}


	private void Start()
	{
		mainCam = Camera.main;
	}


	private void Update()
	{
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Cry"))
		{
			NoiseAmount += NoiseIncreaseRate * Time.deltaTime;
			noiseBar.fillAmount = NoiseAmount;
		}

		if (NoiseAmount > 1)
		{
			Debug.Log("GameOver");
		}
		

		if (carrying)
		{
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
						baby.transform.position = LastValidPos;
						cooldownCounter = Time.time + 0.2f;
						Debug.Log("invalidPos");
					}
					else
					{
						if(overlap == currentOverlap.playPlace)
						{
							playing = true;
						}


						carrying = false;
						cooldownCounter = Time.time + 0.2f;
						Debug.Log("validPos");
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

						anim.Play("FallOver");

						target = null;

						canMove = false;
					}

				}
				else
				{
					target = waypoints[Random.Range(0, waypoints.Length)];
					Debug.Log("ReLocating Target");
					TimeoutCounter = Time.time + 2;
					RandomFallTimeCounter = Time.time + Random.Range(0f, 5f);
				}
			}
		}
	}
}