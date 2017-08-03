using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour {

    private Vector3 desiredRot;
    // Use this for initialization
    void Start () {
        desiredRot = Vector3.zero;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(desiredRot.x != 0f || desiredRot.z != 0f)
            desiredRot.x = desiredRot.z = 0f;
        desiredRot.y = transform.eulerAngles.y;
        transform.eulerAngles = desiredRot;
	}
}
