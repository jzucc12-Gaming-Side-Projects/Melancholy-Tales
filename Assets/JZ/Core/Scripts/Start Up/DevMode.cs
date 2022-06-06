using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevMode : MonoBehaviour
{
    [SerializeField] bool devMode = false;
    public static bool inDevMode = true;

    void Awake()
    {
        inDevMode = devMode;
    }
}
