using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownText : MonoBehaviour {

    //private Vector3 desiredPosition;
    private float timer;

    void Start()
    {
        //desiredPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime * 15f;
        transform.Translate(Vector3.up * (Mathf.Sin(timer) * 6f) * Time.deltaTime);
    }
}
