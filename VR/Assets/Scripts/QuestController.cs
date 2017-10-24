using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour {

    public static QuestController singleton { get; private set; }

    [SerializeField]
    private int KeyOrbAmountToUnlockMaze;

    [SerializeField]
    private GameObject keyOrbObj;

    [SerializeField]
    private GameObject finishingDiamond;

    private MazeGenerator mazeGen;

    void Awake(){
        singleton = this;
    }

    void Start(){
        mazeGen = MazeGenerator.singleton;
        GameObject finishingTile = mazeGen.getFinishingGrid().gameObject;
        setupFinishingTile(finishingTile);
    }

    private void setupFinishingTile(GameObject finishingTileObj){
        Instantiate(finishingDiamond, finishingTileObj.transform);
        BoxCollider finishingCollider = finishingTileObj.AddComponent<BoxCollider>();
        finishingCollider.isTrigger = true;
        FinishTile finishingTile = finishingTileObj.AddComponent<FinishTile>();
    }

    public int getKeyOrbAmountRequirement() { return KeyOrbAmountToUnlockMaze; }

	// Update is called once per frame
	void Update () {
		if(Controller.singleton.getKeyOrbAmount() >= KeyOrbAmountToUnlockMaze){
            MazeGenerator.singleton.UnlockFinishTile();
        }
	}

    public void Initialize(List<Grid> unwalledGrid){
        List<Grid> keySpawnPoints = new List<Grid>(unwalledGrid);
        for (int i = 0; i < 5; i++) {
            int rnd = Random.Range(0, keySpawnPoints.Count);
            Instantiate(keyOrbObj, keySpawnPoints[rnd].transform.position, Quaternion.identity);
            keySpawnPoints.RemoveAt(rnd);
        }
    }
}
