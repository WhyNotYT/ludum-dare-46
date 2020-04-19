using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NamesSceneManager : MonoBehaviour
{

	private void Awake()
	{
		if(!string.IsNullOrEmpty(PlayerPrefs.GetString("playerName")))
		{
			SceneManager.LoadScene("Menu");
		}
	}



	public void UpdateName(string value)
	{
		if(value.Length > 16)
		{
			value = value.Substring(16);
		}
		PlayerPrefs.SetString("playerName", WWW.EscapeURL(value));
		SceneManager.LoadSceneAsync("Menu");
		FindObjectOfType<Fade>().fadeIn = false;
	}
}
