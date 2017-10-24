using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyOrbText : Text, PlayerObserver
{
    public void observe(Controller playerController){
        text = playerController.getKeyOrbAmount().ToString();
    }
}
