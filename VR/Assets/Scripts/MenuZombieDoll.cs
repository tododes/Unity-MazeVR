using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuZombieDoll : GazedEntity {

    [SerializeField] private Animator anim;
    [SerializeField] private UpDownText upDownText;
    [SerializeField] private string nextScene;

    public override void OnStartGazed(){
        base.OnStartGazed();
        if(anim)
            anim.SetBool("Gazed", true);
        if (upDownText)
            upDownText.enabled = true;
    }

    public override void OnExitGazed(){
        base.OnExitGazed();
        if (anim)
            anim.SetBool("Gazed", false);
        if (upDownText)
            upDownText.enabled = false;
    }

    protected override void OnLongEnoughToGaze(){
        if (nextScene.Contains("Exit")){
            Application.Quit();
            return;
        }
        InterSceneImage.singleton.FinishScene(nextScene);
    }
}
