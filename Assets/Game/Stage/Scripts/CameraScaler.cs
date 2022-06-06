using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    #region//Cached variables
    CinemachineVirtualCamera myCam;
    #endregion

    #region//Camera values
    [Header("Current Values")]
    [SerializeField] float myAR;

    [Header("Base Values")]
    [SerializeField] float baseAR = 16 / 9f;
    float baseFOV;

    [Header("Thin Values")]
    [SerializeField] float thinFOV = 65f;
    [SerializeField] float thinAR = 1.333f;

    [Header("Wide Values")]
    [SerializeField] float wideFOV = 45f;
    [SerializeField] float wideAR = 2.333f;
    #endregion


    #region/Monobehaviour
    private void Awake()
    {
        myCam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        baseFOV = myCam.m_Lens.FieldOfView;
        UpdateFOV();
    }

    private void Update()
    {
        if (myAR != myCam.m_Lens.Aspect)
            UpdateFOV();
    }
    #endregion

    void UpdateFOV()
    {
        myAR = myCam.m_Lens.Aspect;

        if (myCam.m_Lens.Aspect <= baseAR)
            ThinFOVUpdate();
        else
            WideFOVUpdate();
    }

    void ThinFOVUpdate()
    {
        float frac = (baseAR - myCam.m_Lens.Aspect) / (baseAR - thinAR);
        myCam.m_Lens.FieldOfView = Mathf.Lerp(baseFOV, thinFOV, frac);
    }

    void WideFOVUpdate()
    {
        float frac = (wideAR - myCam.m_Lens.Aspect) / (wideAR - baseAR);
        myCam.m_Lens.FieldOfView = Mathf.Lerp(wideFOV, baseFOV, frac);
    }
}
