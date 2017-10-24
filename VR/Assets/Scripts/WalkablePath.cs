using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkablePath : Interactable {

    public override void OnInteract(Controller playerController){
        playerController.setCurrentGrid(GetComponent<Grid>());
    }

    public void OnInteract(EnemyController enemyController){
        enemyController.setCurrentGrid(GetComponent<Grid>());
    }

    public void OnContinuouslyInteract(EnemyController enemyController){
        enemyController.setCurrentGrid(GetComponent<Grid>());
    }
}
