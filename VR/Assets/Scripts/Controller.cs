using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public static Controller singleton { get; private set; }

    public GameObject ground;
    [SerializeField] private bool walking;

    private Vector3 rotVector;
    private Vector3 posVector;
    private Camera cam;

    private Ray ray;
    private Vector3 rotateRate;

    [SerializeField] private Grid currentGrid;
    [SerializeField] private List<Transform> transforms = new List<Transform>();
    [SerializeField] private int KeyOrbAmount;

    private List<PlayerObserver> observers = new List<PlayerObserver>();

    void Awake() {
        singleton = this;
    }

	// Use this for initialization
	void Start () {
        walking = true;
        posVector = new Vector3(1f, 0f, 1f);
        rotVector = new Vector3(0f, 1f, 0f);
        ray = new Ray();
        rotateRate = new Vector3(0, 0, 0);

        observers.Add(GameObject.Find("Key Orb Text").GetComponent<KeyOrbText>());
        letObserversObserveMe();
	}

    public Grid getCurrentGrid() { return currentGrid; }
    public void setCurrentGrid(Grid g) { currentGrid = g; }

    public int getKeyOrbAmount() { return KeyOrbAmount; }
    public void AddKeyOrb() { KeyOrbAmount++; }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!cam){
            cam = Camera.main;
        }

        if (walking){
            transform.position = transform.position + cam.transform.forward * Time.deltaTime;
        }

        ray.origin = Camera.main.transform.position;
        ray.direction = Camera.main.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
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

    void OnTriggerEnter(Collider coll){
        Interactable interactedCollider = coll.GetComponent<Interactable>();
        if (interactedCollider){
            interactedCollider.OnInteract(this);
            letObserversObserveMe();
        }
    }

    private void letObserversObserveMe(){
        for(int i = 0; i < observers.Count; i++){
            observers[i].observe(this);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        Interactable interactedCollider = coll.GetComponent<Interactable>();
        if (interactedCollider)
        {
            interactedCollider.OnContinuouslyInteract(this);
        }
    }

    void OnTriggerStay(Collider coll)
    {
        Interactable interactedCollider = coll.GetComponent<Interactable>();
        if (interactedCollider)
        {
            interactedCollider.OnStopInteract(this);
        }
    }
}
