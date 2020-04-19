using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
	[TextArea(2, 4)]
	public string[] tips;

	public Image spotLight;
	public MobileGameController mobileController;
	public Controller babeController;
	public OnlineScores onlineScores;
	public TMP_Text tipText;
	public TMP_Text uploadingNotice;
	public GameObject gameOverUI;
	public bool gameOver;
	public string playerName;
	public bool started;
	public GameObject startUI;
	public AudioSource gameOverSound;

	private void Start()
	{
		playerName = PlayerPrefs.GetString("playerName");
	}


	private void Update()
	{
		if (gameOver)
		{
			if (onlineScores.UploadComplete)
			{
				if (!onlineScores.downloading && !onlineScores.downloadComplete)
				{
					onlineScores.DownloadScores();
				}

				if(onlineScores.downloadComplete)
				{
					int i;
					for (i = 0; i < onlineScores.HighScores.Length; i++)
					{
						if(playerName == onlineScores.HighScores[i].UserName)
						{
							break;
						}
					}

					string targetText = "";
					if (i % 10 == 0 || i == 0)
					{
						targetText = "You Ranked " + (i + 1) + "st";
					}
					else if(i % 11 == 0 || i == 1)
					{
						targetText = "You Ranked " + (i + 1) + "nd";
					}
					else if (i % 12 == 0 || i == 2)
					{
						targetText = "You Ranked " + (i + 1) + "rd";
					}
					else
					{
						targetText = "You Ranked" + (i + 1) + "th";
					}
					uploadingNotice.text = targetText;
				}
				else
				{
					uploadingNotice.text = "Uploading Your Score...";
				}
			}


			if(Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene("Game");
			}
			else if(Input.GetKeyDown(KeyCode.Escape))
			{

				SceneManager.LoadSceneAsync("Menu");
				FindObjectOfType<Fade>().fadeIn = false;
			}
		}

		if(!started)
		{

			if (Input.GetKeyDown(KeyCode.Space))
			{
				started = true;
				startUI.SetActive(false);
				babeController.enabled = true;
				mobileController.enabled = true;
			}
		}
	}




	public void GameOver(Vector3 spotLightTarget)
	{
		gameOverSound.Play();
		gameOver = true;
		spotLight.gameObject.SetActive(true);
		Vector3 finalPos = spotLightTarget;
		finalPos.z = spotLight.transform.position.z;
		spotLight.transform.position = finalPos;
		babeController.wannaPoop.SetActive(false);

		tipText.text = "Tip: " + tips[Random.Range(0, tips.Length)];


		onlineScores.UpdateScore(playerName, mobileController.Score.ToString());
		gameOverUI.SetActive(true);


		mobileController.enabled = false;
		babeController.enabled = false;
	}

}
