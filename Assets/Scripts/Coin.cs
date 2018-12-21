using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    Rigidbody _rig;
	// Use this for initialization
	void Start () {
        _rig = GetComponent<Rigidbody>();
        _rig.AddForce(Vector3.up*300);
        StartCoroutine(rotate());
        Destroy(gameObject, 3);

	}

    IEnumerator rotate()
    {
        Quaternion rotate = transform.rotation;
        Vector3 look = Vector3.zero;
        while (true)
        {
            look += new Vector3(0, 100, 0);
            rotate.SetLookRotation(look);

            transform.rotation = rotate;
            yield return new WaitForFixedUpdate();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
