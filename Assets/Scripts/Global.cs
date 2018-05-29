using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;
    public Survey survey;

    void Awake()
    {
        instance = this;
    }

}
