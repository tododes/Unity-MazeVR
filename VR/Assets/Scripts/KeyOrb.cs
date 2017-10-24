using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOrb : Interactable {

    private Vector3 desiredPosition;
    private float timer;

    void Start(){
        desiredPosition = transform.position;
    }

    public override void OnInteract(Controller playerController){
        playerController.AddKeyOrb();
        gameObject.SetActive(false);
    }
    
    void Update(){
        timer += Time.deltaTime * 5f;
        desiredPosition.y = Mathf.Sin(timer) * 0.1f;
        transform.position = desiredPosition;
    }
}
