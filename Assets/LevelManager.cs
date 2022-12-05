using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public MapManager mapManager;
    public UnitManager unitManager;

    void Start()
    {
        mapManager.initiateMap();
        unitManager.initiateUnits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
