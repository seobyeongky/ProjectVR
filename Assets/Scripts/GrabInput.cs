using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class GrabInput : MonoBehaviour
{
    public Player player;
    [Range(-1, 1)]
    public float movementThreshold = 0.5f;
    [Range(0, 10)]
    public float speed = 1;
    [Range(0, 10)]
    public float moveDuration = 3;

    private Hand trackedHand;
    private bool tracking = false;
    private float startTime;
    private Vector3 startPos;
    private Vector3 direction;

    private float endTime;

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
        float currentTime = Time.unscaledTime;
        if (currentTime < endTime)
        {
            player.characterController.SimpleMove(direction * speed);
        }
        else
        {
            player.characterController.SimpleMove(Vector3.zero);
        }

        Hand  newHand = null;
        Vector3 tempPos = Vector3.zero;
        foreach (Hand hand in player.hands)
        {
            if (IsValidHand(hand))
            {
                if (tracking)
                {
                    if (IsGrabReleased(hand))
                    {
                        if (trackedHand == hand) //This is the pointer hand
                        {
                            GrabPull(startPos, hand.transform.localPosition, currentTime - startTime);
                        }
                    }
                }

                if (IsGrabPressed(hand))
                {
                    newHand = hand;
                    tempPos = newHand.transform.localPosition;
                }
            }
        }

        if (!tracking && newHand != null)
        {
            trackedHand = newHand;
            startPos = tempPos;
            startTime = currentTime;
            tracking = true;
        }
    }

    private bool IsValidHand(Hand hand)
    {
        return hand != null && hand.controller != null && hand.gameObject.activeInHierarchy;
    }

    private bool IsGrabReleased(Hand hand)
    {
        return hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad);
    }
    private bool IsGrabPressed(Hand hand)
    {
        return hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad);
    }

    private void GrabPull(Vector3 start, Vector3 end, float duration)
    {
        tracking = false;
        trackedHand = null;
        if (duration == 0)
            duration = .1f;
        direction = start - end;//inverse
        direction.y = 0;
        if (direction.magnitude <= movementThreshold)
            direction = Vector3.zero;
        if (duration < 0.1f)
            direction = Vector3.zero;
        direction = direction / duration;
        endTime = Time.unscaledTime + moveDuration;
        //Debug.LogFormat("[{0}],[{1}]", start, end);

    }
}