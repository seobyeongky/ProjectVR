using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInputControls : MonoBehaviour
{
    // 1
    public SteamVR_TrackedObject trackedObj;
    // 2
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        if(trackedObj == null)
            trackedObj = GetComponent<SteamVR_TrackedObject>();
        if (trackedObj == null)
            trackedObj = GameObject.FindObjectOfType<SteamVR_TrackedObject>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}