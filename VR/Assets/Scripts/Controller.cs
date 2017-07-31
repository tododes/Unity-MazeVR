using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public GameObject ground;
    [SerializeField] private bool walking;

    private Vector3 rotVector;
    private Vector3 posVector;
    private Camera cam;

    private Ray ray;
    private Vector3 rotateRate;

	// Use this for initialization
	void Start () {
        walking = true;
        posVector = new Vector3(1f, 0f, 1f);
        rotVector = new Vector3(0f, 1f, 0f);
        ray = new Ray();
        rotateRate = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (!cam)
        {
            cam = Camera.main;
        }

        if (walking)
        {
            transform.position = transform.position + cam.transform.forward * Time.deltaTime;
        }

        rotVector.x = rotVector.z = posVector.y = 0f;
        rotVector.y = cam.transform.eulerAngles.y;
        posVector.x = transform.position.x;
        posVector.z = transform.position.z;
        transform.position = posVector;
        cam.transform.eulerAngles = rotVector;

        ray.origin = Camera.main.transform.position;
        ray.direction = Camera.main.transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.name.Contains("plane") || hit.collider.name.Contains("GRID"))
                walking = false;
            else
                walking = true;
        }
        else
            walking = true;

        rotateRate.y = 45f * Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.eulerAngles += rotateRate;
    }
}
