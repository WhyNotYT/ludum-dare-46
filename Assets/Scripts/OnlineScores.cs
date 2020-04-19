using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class OnlineScores : MonoBehaviour
{

	public HighScore[] HighScores;

	private const string privateCode = "";
	private const string publicCode = "";
	private const string URL = "";

	public bool uploading;
	public bool downloading;
	public bool uploadFailed;
	public bool downloadFailed;
	public bool UploadComplete;
	public bool downloadComplete;
	public void UpdateScore(string PlayerName, string Score)
	{
		StartCoroutine(UploadScore(PlayerName, Score));

	}


	IEnumerator UploadScore(string PlayerName, string Score)
	{
		WWW www = new WWW(URL + privateCode + "/add/" + UnityWebRequest.EscapeURL(PlayerName) + "/" + Score);
		Debug.Log("Uploading");
		uploading = true;
		yield return www;


		if (string.IsNullOrEmpty(www.error))
		{
			Debug.Log("Upload Sucessful");
			uploading = false;
			uploadFailed = false;
			UploadComplete = true;
		}
		else
		{
			Debug.Log(www.error);
			uploading = false;
			uploadFailed = true;
		}
	}

	IEnumerator DownloadScore()
	{
		WWW www = new WWW(URL + publicCode + "/pipe/");
		Debug.Log("Downloading...");
		downloading = true;
		yield return www;


		if (string.IsNullOrEmpty(www.error))
		{
			Debug.Log("Download Sucessful");
			FormatHighScores(www.text);
			downloading = false;
			downloadComplete = true;
			
		}
		else
		{
			Debug.Log(www.error);
			downloadFailed = true;
			downloading = false;
		}
	}

	void FormatHighScores(string TextStream)
	{
		string[] entries = TextStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		HighScores = new HighScore[entries.Length];


		for (int i = 0; i < entries.Length; i++)
		{
			string[] Info = entries[i].Split(new char[] { '|' });
			string UserName = Info[0];
			string Score = Info[1];
			HighScores[i] = new HighScore(UserName, Score);
		}
	}


	public void DownloadScores()
	{
		StartCoroutine(DownloadScore());
	}



	public struct HighScore
	{
		public string UserName;
		public string Score;




		public HighScore(string _Username, string _Score)
		{
			UserName = UnityWebRequest.UnEscapeURL(_Username);
			
			Score = _Score;

		}
	}
}
