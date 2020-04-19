using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
	public TMP_Text names;
	public TMP_Text scores;
	public OnlineScores onlineScores;
	public TMP_Text YourRank;


	private void Start()
	{
		onlineScores.DownloadScores();
	}

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}

	private void Update()
	{
		if(onlineScores.downloadComplete)
		{
			string namesText = "Name\n";
			string scoresText = "Score\n";
			for (int i = 0; i < onlineScores.HighScores.Length; i++)
			{
				if (i < 10)
				{
					namesText += onlineScores.HighScores[i].UserName + "\n";
				}
				else
				{
					break;
				}
			}
			for (int i = 0; i < onlineScores.HighScores.Length; i++)
			{
				if (i < 10)
				{
					scoresText += onlineScores.HighScores[i].Score + "\n";
				}
				else
				{
					break;
				}
			}


			int j;
			string playerName = PlayerPrefs.GetString("playerName");
			for (j = 0; j < onlineScores.HighScores.Length; j++)
			{
				if (playerName == onlineScores.HighScores[j].UserName)
				{
					break;
				}
			}

			string targetText = "Your Rank is ";

			if (j % 10 == 0 || j == 0)
			{
				targetText += (j + 1) + "st";
			}
			else if (j % 11 == 0 || j == 1)
			{
				targetText += (j + 1) + "nd";
			}
			else if (j % 12 == 0 || j == 2)
			{
				targetText += (j + 1) + "rd";
			}
			else
			{
				targetText += (j + 1) + "th";
			}
			YourRank.text = targetText;

			names.text = namesText;
			scores.text = scoresText;
		}
	}

	public void loadGame()
	{
		Debug.Log("BtnClicked");
		SceneManager.LoadSceneAsync("Game");
		FindObjectOfType<Fade>().fadeIn = false;
	}


	public void changeName()
	{
		PlayerPrefs.DeleteAll();
		SceneManager.LoadSceneAsync("Name");
		FindObjectOfType<Fade>().fadeIn = false;
	}

	public void quitGame ()
	{
		Application.Quit();
	}



}
