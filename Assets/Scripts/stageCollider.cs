﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageCollider : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StageController.instance.mapUpdate();
            Debug.Log("进入");
        }
    }
    
}