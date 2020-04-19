using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public float fadeInSpeed;
	public float fadeOutSpeed;
	public AudioSource baseMusic;
	public AudioSource extraMusic;
	public AudioSource bothMusic;
	public bool Extra;




	private void Start()
	{
		baseMusic.time = Time.time % baseMusic.clip.length;
		extraMusic.time = Time.time % baseMusic.clip.length;
		bothMusic.time = Time.time % bothMusic.clip.length;
	}


	private void Update()
	{
		if(Extra)
		{
			extraMusic.volume = Mathf.MoveTowards(extraMusic.volume, baseMusic.volume, fadeInSpeed * Time.deltaTime);
		}
		else
		{
			extraMusic.volume = Mathf.MoveTowards(extraMusic.volume, 0, fadeOutSpeed * Time.deltaTime);

		}
	}
}
