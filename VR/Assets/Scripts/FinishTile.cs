using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTile : Interactable {

    public override void OnInteract(Controller playerController){
        InterSceneImage.singleton.FinishScene("Post Game");
    }
}
