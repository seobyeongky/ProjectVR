using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ControllerInput : MonoBehaviour
{
	public Player player;
	[Range(0, 1)]
	public float touchThreshold = 0.5f;

	public SteamVR_Controller.Device leftController;
	public SteamVR_Controller.Device rightController;

	[SerializeField]
	private Vector2 movementDirection = Vector2.zero;
	[SerializeField]
	private Vector2 facingDirection = Vector2.zero;

	//meter per sec
	public float movementSpeed = 1;

	public float rotationSpeed = 1;

	public void Start()
	{
		player = Player.instance;
		if (player == null)
			Debug.LogError("No player instance found!");
		gameObject.SetActive(false);
	}

	public void Update()
	{
		//Find the controller every frame to make sure inputs are aligned no matter what
		//left for strafing
		leftController = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost));
		//right for view alignment
		rightController = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost));

		//strafing
		if (leftController.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
		{
			movementDirection = GetNormalizedInputVector2(leftController.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0), touchThreshold);
		}

		Vector3 pos = transform.position;
		pos.x += movementDirection.x * movementSpeed * Time.deltaTime;
		pos.y += movementDirection.y * movementSpeed * Time.deltaTime;
		transform.position = pos;

		//rotation
		if (rightController.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
		{
			facingDirection = GetNormalizedInputVector2(rightController.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0), touchThreshold);
		}
		Quaternion rot = transform.rotation;
		rot *= Quaternion.Euler(facingDirection.x * rotationSpeed * Time.deltaTime, 0, 0);
		rot *= Quaternion.Euler(0, facingDirection.y * rotationSpeed * Time.deltaTime, 0);
		transform.rotation = rot;

	}

	public static Vector2 GetNormalizedInputVector2(Vector2 input, float threshold)
	{
		Vector2 temp = Vector2.zero;
		if (input.x > threshold || input.x < -threshold)
			temp.x = input.x;
		if (input.y > threshold || input.y < -threshold)
			temp.y = input.y;
		temp.Normalize();
		return temp;
	}
}