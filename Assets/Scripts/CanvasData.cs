using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasData : MonoBehaviour 
{
    public float UIScale
    {
        get
        {
            return Screen.width / referenceResolutionX;
        }
    }

    private float referenceResolutionX;

    private void Awake()
    { 
        referenceResolutionX = GetComponent<CanvasScaler>().referenceResolution.x;
    }
}
