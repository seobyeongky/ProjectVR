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
        float angles = 0;
        Vector3 forward = Vector3.zero;
        foreach (var trs in player.hmdTransforms)
        {
            if (trs.gameObject.activeInHierarchy)
            {
                angles = trs.localEulerAngles.y;
                forward = trs.forward;
            }
        }
        float movementPower = 0;
        foreach (var hand in player.hands)
        {
            if (hand != null && hand.gameObject.activeInHierarchy && hand.controller != null)
            {
                if (hand.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    //velocity : forward +z, right +x, aligned to "front" in room setup
                    movementPower = Mathf.Min(movementPower, (Quaternion.Euler(0, -angles, 0) * hand.controller.velocity).z);
                    //Debug.Log(movementPower);
                }
            }
        }

        if (movementPower < -movementThreshold)
        {
            player.characterController.SimpleMove(forward * -movementPower * speed);
        }
    }
}