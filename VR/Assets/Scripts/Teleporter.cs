using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Interactable {

    [SerializeField] private Teleporter partner;
    [SerializeField] private bool onReadyTeleport;

    public void teleport(Controller playerController){
        playerController.transform.position = partner.transform.position;
    }

    public override void OnContinuouslyInteract(Controller playerController)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            teleport(playerController);
    }

    public override void OnInteract(Controller playerController)
    {
        onReadyTeleport = true;
    }

    public override void OnStopInteract(Controller playerController)
    {
        onReadyTeleport = false;
    }
}
