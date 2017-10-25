using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazedEntity : MonoBehaviour {

    protected bool isGazed;
    [SerializeField] protected float gazeTimer;
    [SerializeField] protected float gazeThreshold;
    [SerializeField] protected bool thresholdReached;

	protected virtual void Start () {
        isGazed = false;
	}

    protected virtual void LateUpdate () {
        if (isGazed){
            gazeTimer += Time.deltaTime;
        }
        else{
            if(gazeTimer > 0f)
                gazeTimer = 0f;
        }

        if(gazeTimer >= gazeThreshold){
            if (!thresholdReached){
                OnLongEnoughToGaze();
                thresholdReached = true;
            }
            
        }
	}

    public virtual void OnStartGazed(){
        if (isGazed)
            return;
        isGazed = true;
    }

    public virtual void OnExitGazed(){
        isGazed = false;
        thresholdReached = false;
    }
    
    protected virtual void OnLongEnoughToGaze(){

    }
}
