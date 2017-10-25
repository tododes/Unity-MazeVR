using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gazer : MonoBehaviour {

    private Ray gazeRay;
    private RaycastHit gazeHit;
    private LayerMask menuGazeMask;
    private GazedEntity currentGazedEntity;
    private bool gazingAGazedEntity;
	// Use this for initialization
	void Start () {
        gazeRay = new Ray();
        menuGazeMask = LayerMask.GetMask("Menu Gaze Layer");
	}
	
	void FixedUpdate () {
        gazeRay.origin = transform.position;
        gazeRay.direction = transform.forward;
        gazingAGazedEntity = false;
		if(Physics.Raycast(gazeRay, out gazeHit, 100f, menuGazeMask)){
            currentGazedEntity = gazeHit.collider.GetComponent<GazedEntity>();
            if (currentGazedEntity){
                gazingAGazedEntity = true;
                currentGazedEntity.OnStartGazed();
            }
        }

        if (!gazingAGazedEntity && currentGazedEntity){
            currentGazedEntity.OnExitGazed();
            currentGazedEntity = null;
        }
	}
}
