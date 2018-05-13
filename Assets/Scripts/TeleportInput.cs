using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportInput : MonoBehaviour
{
    public Player player;

    private bool tracking = false;

    Hand trackedHand;

    // Use this for initialization
    void Start()
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
        TrackTeleport();
    }

    void TrackTeleport()
    {
        Hand oldHand = trackedHand, newHand = null;
        foreach (Hand hand in player.hands)
        {
            if (tracking)
            {
                if (hand == trackedHand)
                {
                    if (IsPressedUp(hand))
                    {

                    }
                }
            }
            if (IsPressedDown(hand))
            {
                newHand = hand;
            }
        }

        if (newHand && !tracking)
        {

        }
    }

    bool IsPressedDown(Hand hand)
    {
        return hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad);
    }
    bool IsPressedUp(Hand hand)
    {
        return hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad);
    }
}