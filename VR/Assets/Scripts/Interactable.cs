using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public virtual void OnInteract(Controller playerController){

    }

    public virtual void OnContinuouslyInteract(Controller playerController){

    }

    public virtual void OnStopInteract(Controller playerController){

    }
}
