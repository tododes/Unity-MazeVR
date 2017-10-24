using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {

    private Vector3 desiredPosition;
    private float timer;

    void Start()
    {
        desiredPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime * 5f;
        desiredPosition.y = Mathf.Sin(timer) * 0.2f;
        transform.position = desiredPosition;
    }
}
