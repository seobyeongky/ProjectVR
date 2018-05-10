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
	public float movementSpeed = 2;

	public float rotationSpeed = 10;

	public void Start()
	{
		player = Player.instance;
        if (player == null)
        {
            Debug.LogError("No player instance found!");
            gameObject.SetActive(false);
        }
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
        if (leftController.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
            movementDirection = Vector2.zero;

        player.movementRigidBody.velocity = Vector3.Scale(transform.forward, new Vector3(movementDirection.y,0,movementDirection.x)).normalized * movementSpeed;

		//rotation
		if (rightController.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
		{
			facingDirection = GetNormalizedInputVector2(rightController.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0), touchThreshold);
        }
        if(rightController.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
            facingDirection = Vector2.zero;

        foreach(var trs in player.hmdTransforms)
        {
            Quaternion rot = trs.rotation;
            rot *= Quaternion.Euler(facingDirection.y * rotationSpeed * Time.deltaTime, facingDirection.x * rotationSpeed * Time.deltaTime, 0);
            trs.rotation = rot;
        }       

    }

	public static Vector2 GetNormalizedInputVector2(Vector2 input, float threshold)
	{
		Vector2 temp = Vector2.zero;
		if (input.x > threshold || input.x < -threshold)
			temp.x = input.x;
		if (input.y > threshold || input.y < -threshold)
			temp.y = input.y;
        //if(temp.magnitude > 0)
        //    temp.Normalize();
		return temp;
	}
}