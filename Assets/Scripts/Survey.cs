using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survey : MonoBehaviour
{
    [SerializeField]
    List<RectTransform> numberTransforms = new List<RectTransform>();

    [SerializeField]
    RectTransform caretTransform;


    int cursor = 3;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        var leftController = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost));
        //right for view alignment
        var rightController = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost));

        if (leftController.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            cursor--;
            if (cursor < 0) cursor = 0;
            UpdateCursor();
        }

        if (rightController.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            cursor++;
            if (cursor >= numberTransforms.Count) cursor = numberTransforms.Count - 1;
            UpdateCursor();
        }

        if (leftController.GetHairTriggerDown()
            || rightController.GetHairTriggerDown())
        {
            Commit();
        }
    }

    void UpdateCursor()
    {
        caretTransform.position = numberTransforms[cursor].position;
    }

    void Commit()
    {
        Debug.Log("commit score : " + cursor);
        gameObject.SetActive(false);
    }
}
