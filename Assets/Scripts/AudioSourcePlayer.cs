using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour
{
	new private AudioSource audio;

	private void Awake()
	{
		audio = GetComponent<AudioSource>();
		if (audio == null)
			Destroy(this);
	}

	// Use this for initialization
	void Start()
	{
		audio.Stop();
	}

	public void PlayAudio()
	{
		audio.Play();
	}

	public void PlayAudio(float duration)
	{
		audio.Play();
		UnityEngine.Events.UnityEvent events = new UnityEngine.Events.UnityEvent();
		UnityEngine.Events.UnityAction action = new UnityEngine.Events.UnityAction(StopAudio);
		events.AddListener(action);
		Timer.Instance.AddNotification(duration, events, false);
	}

	public void PlayAudio(bool loop)
	{
		//not yet implemented
	}

	public void PlayAudio(float duration, bool loop)
	{
		//not yet implemented
	}

	public void StopAudio()
	{
		audio.Stop();
	}
}
