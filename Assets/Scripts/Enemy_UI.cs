using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UI : MonoBehaviour {
    public static Enemy_UI instance;

    public Image hpbar;
    public Text level;
    public Image hpbox;
    public float UIscale;
	void Start () {
        instance = this;
        UIscale = GetComponentInParent<CanvasData>().UIScale;
        hid();
	}
	

    public void hid()
    {
        hpbox.enabled = false;
        hpbar.enabled = false;
        level.enabled = false;
    }
    public void show()
    {
        hpbox.enabled = true;
        hpbar.enabled = true;
        level.enabled = true;
    }
	// Update is called once per frame
	void Update () {


		
	}
}
