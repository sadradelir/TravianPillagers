using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnitManager;

public class UnitGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [Serializable]
    public class UnitInterface
    {
        public string name;
        public GameObject model;
    }
    [SerializeField]
    public List<UnitInterface> unitInterfaces;


    public GameObject generateUnit(Unit unit)
    {
        var unitInterface = unitInterfaces.First(t=>t.name == unit.name);
        return GameObject.Instantiate(unitInterface.model);
    }

}
