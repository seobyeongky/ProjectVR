using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRInputControls : MonoBehaviour
{
	public enum EInteractionMode
	{
		Standard,
		Teleport,
		ArmSwing,
		GrabPull,
	}

	public EInteractionMode mode = EInteractionMode.Standard;

	public Player player;

	public GameObject locomotion;
	public GameObject teleporter;
	public GameObject armSwing;
	public GameObject grabPull;

	private void Awake()
    {

    }

	private void Start()
	{
		player = Player.instance;
        if (player == null)
        {
            Debug.LogError("No player instance found!");
            gameObject.SetActive(false);
        }
	}

	// Update is called once per frame
	void Update()
    {	

    }
}