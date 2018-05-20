using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	private static Timer instance;
	public static Timer Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<Timer>();
			}
			return instance;
		}
	}

	public List<Notification> notificationList = new List<Notification>();
	private List<Notification> removeList = new List<Notification>();

	private void Start()
	{
		foreach(var not in notificationList)
		{
			not.Start();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(notificationList.Count > 0)
		{
			foreach(var not in notificationList)
			{
				if (not.End)
				{
					if(!not.repeat)
						removeList.Add(not);
					not.triggeredEvent.Invoke();
				}
			}

			foreach(var not in removeList)
			{
				notificationList.Remove(not);
			}
		}
	}

	public void AddNotification(float delay, UnityEvent unityEvent, bool repeat)
	{

	}

	[System.Serializable]
	public class Notification
	{
		[Tooltip("Seconds to wait before triggering event")]
		public float waitFor = 1;
		public bool repeat = false;
		private float startTime;
		private float endTime;
		public UnityEvent triggeredEvent;

		public void Start()
		{
			startTime = Time.unscaledTime;
			endTime = startTime + waitFor;
		}

		public bool End { get { return endTime <= Time.unscaledTime; } }
	}
}