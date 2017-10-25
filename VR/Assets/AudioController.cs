using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    private AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.volume = PlayerPrefs.GetInt(gameObject.tag) * 1f / 10f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
