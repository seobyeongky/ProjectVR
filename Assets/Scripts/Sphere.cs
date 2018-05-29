using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Sphere : MonoBehaviour
{
    public Sphere nextSphere;
    const float R = 3f;

	// Update is called once per frame
	void Update ()
    {
        var playerPos = Player.instance.transform.position;
        var myPos = transform.position;

        if ((playerPos - myPos).sqrMagnitude < R * R)
        {
            Global.instance.survey.gameObject.SetActive(true);
            Destroy(gameObject);
            if (nextSphere != null)
                nextSphere.gameObject.SetActive(true);
        }
    }
}
