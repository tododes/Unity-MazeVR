using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public GameObject ground;
    [SerializeField] private bool walking;

    private Vector3 rotVector;
    private Vector3 posVector;
    private Camera cam;

    private Ray ray;
    private Vector3 rotateRate;

    [SerializeField]
    private List<Transform> transforms = new List<Transform>();

	// Use this for initialization
	void Start () {
        walking = true;
        posVector = new Vector3(1f, 0f, 1f);
        rotVector = new Vector3(0f, 1f, 0f);
        ray = new Ray();
        rotateRate = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!cam)
        {
            cam = Camera.main;
        }

        if (walking)
        {
            transform.position = transform.position + cam.transform.forward * Time.deltaTime;
        }

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
    }

    void LateUpdate(){
        rotVector.x = rotVector.z = posVector.y = 0f;
        rotVector.y = cam.transform.eulerAngles.y;
        posVector.x = transform.position.x;
        posVector.z = transform.position.z;
        transform.position = posVector;
        cam.transform.eulerAngles = rotVector;

        //cam.transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);
        for(int i = 0; i < transforms.Count; i++){
            transforms[i].eulerAngles = new Vector3(0, transforms[i].eulerAngles.y, 0);
        }

        rotateRate.y = 45f * Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.eulerAngles += rotateRate;

    }
}
