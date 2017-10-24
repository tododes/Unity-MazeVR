using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    [SerializeField] private Grid currentGrid, nextGrid;
    private Controller playerController;
    [SerializeField] private List<Grid> pathToPlayer;
    private NavMeshAgent agent;
    private int gridIndex;
	// Use this for initialization
	void Start () {
        playerController = Controller.singleton;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(TryFindPathToPlayer());
	}
	
	// Update is called once per frame
	void Update () {
        if (pathToPlayer != null && pathToPlayer.Count > 0){
            nextGrid = pathToPlayer[pathToPlayer.IndexOf(currentGrid) + 1];
            transform.LookAt(nextGrid.transform.position);
            transform.Translate(Vector3.forward * 4f * Time.deltaTime);
            //agent.SetDestination(nextGrid.transform.position);
            //Debug.Log(pathToPlayer[gridIndex + 1]);
        }
	}

    private IEnumerator TryFindPathToPlayer(){
        while (!PathGenerator.singleton.accessible){
            yield return new WaitForSeconds(Time.deltaTime);
        }
        pathToPlayer = PathGenerator.singleton.FindPath(currentGrid, playerController.getCurrentGrid());
        gridIndex = 0;
    }

    public void setCurrentGrid(Grid g){
        currentGrid = g;
    }

    void OnTriggerEnter(Collider coll)
    {
        Interactable interactableCollider = coll.GetComponent<Interactable>();
        if (interactableCollider)
        {
            if (interactableCollider is WalkablePath)
            {
                WalkablePath interactablePath = (WalkablePath)interactableCollider;
                interactablePath.OnInteract(this);
                if (gridIndex < pathToPlayer.Count - 1)
                    gridIndex++;
            }
        }
    }

    void OnTriggerStay(Collider coll){
        Interactable interactableCollider = coll.GetComponent<Interactable>();
        if (interactableCollider){
            if(interactableCollider is WalkablePath){
                WalkablePath interactablePath = (WalkablePath)interactableCollider;
                interactablePath.OnContinuouslyInteract(this);
                if(gridIndex < pathToPlayer.Count - 1)
                    gridIndex++;
            }
        }
    }
}
