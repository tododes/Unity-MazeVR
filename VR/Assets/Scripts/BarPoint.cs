using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarPoint : GazedEntity {

    [SerializeField] private int index;
    [SerializeField] private Bar myBar;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Color activeColor;

    protected override void Start(){
        base.Start();
        char indexC = gameObject.name[gameObject.name.Length - 2];
        index = (int)indexC - '0';
        myBar = GetComponentInParent<Bar>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    public override void OnStartGazed(){
        base.OnStartGazed();
        myBar.changeVolumePoint(index + 1);
    }

    public void OnVolume(bool enable){
        spriteRenderer.color = enable? activeColor : Color.white;
    }
}
