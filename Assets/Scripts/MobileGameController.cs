using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MobileGameController : MonoBehaviour
{
	public float playerSpeed;
	public float MinX, MaxX;
	public Transform player;
	public SpriteRenderer CivilianCar1;
	public SpriteRenderer CivilianCar2;
	public Transform MiddleLaneStart;
	public Transform LeftLaneStart;
	public Transform RightLaneStart;
	public float CarSpeedMin, CarSpeedMax;
	public float carSpeed1, carSpeed2;
	public int scoreUpdatePerCar;
	public int Score;
	public TMP_Text scoreText;
	public Sprite[] carVariants;
	public Image spotLight;
	private int a1, a2;
	public float difficultyMultiplier;
	private void Start()
	{
		carSpeed1 = Random.Range(CarSpeedMin, CarSpeedMax);
		carSpeed2 = Random.Range(CarSpeedMin, CarSpeedMax);
	}


	private void increaseDifficulty()
	{
		CarSpeedMax *= difficultyMultiplier;
		CarSpeedMin *= difficultyMultiplier;
		FindObjectOfType<Controller>().moveSpeed *= difficultyMultiplier;
	}


	private void Update()
	{

		scoreText.text = "Score: " + Score;
		//Debug.Log(player.transform.position);
		if(Input.GetAxis("Horizontal") != 0)
		{
			Vector3 targetPos = player.transform.position;
			targetPos.x = Mathf.Clamp(targetPos.x + (Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed), MinX, MaxX);
			//Debug.Log(targetPos);
			player.transform.position = targetPos;
		}

		CivilianCar1.transform.Translate(0, -carSpeed1 * Time.deltaTime, 0);

		if (CivilianCar1.transform.position.y < -5.3)
		{

			a1 = Random.Range(0, 3);
			carSpeed1 = Random.Range(CarSpeedMin, CarSpeedMax);

			CivilianCar1.sprite = carVariants[Random.Range(0, carVariants.Length)];

			Score += scoreUpdatePerCar;


			if(a1 == 0)
			{
				CivilianCar1.transform.position = LeftLaneStart.position;
			}
			else if(a1 == 1)
			{
				CivilianCar1.transform.position = MiddleLaneStart.position;
			}
			else
			{
				CivilianCar1.transform.position = RightLaneStart.position;
			}
		}

		CivilianCar2.transform.Translate(0, -carSpeed2 * Time.deltaTime, 0);


		if (CivilianCar2.transform.position.y < -5.3)
		{

			a2 = Random.Range(0, 3);

			if(a1 == a2)
			{
				Debug.Log("Same Pos");
				a2 = Random.Range(0, 3);
			}
			carSpeed2 = Random.Range(CarSpeedMin, CarSpeedMax);

			Score += scoreUpdatePerCar;
			increaseDifficulty();

			CivilianCar2.sprite = carVariants[Random.Range(0, carVariants.Length)];

			if (a2 == 0)
			{
				CivilianCar2.transform.position = LeftLaneStart.position;
			}
			else if (a2 == 1)
			{
				CivilianCar2.transform.position = MiddleLaneStart.position;
			}
			else
			{
				CivilianCar2.transform.position = RightLaneStart.position;
			}
		}
	}
}
