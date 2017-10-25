using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    [SerializeField]
    private string saveKey;

    [SerializeField]
    private int currentVolumePoint;

    [SerializeField]
    private List<BarPoint> points;

    public void changeVolumePoint(int point){
        currentVolumePoint = point;
        for(int i = 0; i < currentVolumePoint; i++){
            points[i].OnVolume(true);
        }
        for (int i = currentVolumePoint; i < points.Count; i++){
            points[i].OnVolume(false);
        }

        PlayerPrefs.SetInt(saveKey, currentVolumePoint);
    }
	// Use this for initialization
	void Start () {
        points = new List<BarPoint>(GetComponentsInChildren<BarPoint>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
