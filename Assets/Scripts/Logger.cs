using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour
{
	public bool trackPosition = false;
	public bool trackRotation = false;

	public string fileName = "Temp.txt";
	public bool logEnabled = false;

	StreamWriter file;

	public void Start()
	{
		StartLogging();
	}

	private void OnDisable()
	{
		EndLogging();
	}

	private void OnDestroy()
	{
		EndLogging();
	}

	private void FixedUpdate()
	{
		if (logEnabled && file != null)
		{
			if (trackPosition)
			{
				file.WriteLine(string.Format("x:{0:F6} y:{1:F6} z:{2:F6}", transform.position.x, transform.position.y, transform.position.z));
			}
			if (trackRotation)
			{
				file.WriteLine(string.Format("x:{0:F6} y:{1:F6} z:{2:F6} w:{3:F6}", transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
			}
		}
	}

	public void StartLogging()
	{
		logEnabled = true;
		if (file != null)
		{
			file.Flush();
			file.Close();
			file = null;
		}
		fileName = System.DateTime.Now.ToString("yyyy_MM_dd_h_mm_ss") + "_" + gameObject.name + ".txt";
		file = new StreamWriter(fileName);
		file.WriteLine("##Log Start##\nTracking " + (trackPosition ? "position" : "") + (trackRotation ? "rotation" : ""));
	}

	public void EndLogging()
	{
		logEnabled = false;
		if (file != null)
		{
			file.WriteLine("Count : " + ChestItem.count);
			file.Flush();
			file.Close();
			file = null;
		}
	}
}