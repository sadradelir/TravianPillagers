using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public MapManager mapManager;
    public UnitGenerator unitGenerator;
    Army army;

    public class Unit
    {
        public string name;
    }

    public class Army
    {
        public List<Unit> units;
        public Army()
        {
            this.units = new List<Unit>();
            units.Add(new Unit() { name = "soldier" });
            units.Add(new Unit() { name = "soldier" });
            units.Add(new Unit() { name = "soldier" });
            units.Add(new Unit() { name = "soldier" });
            units.Add(new Unit() { name = "archer" });
            units.Add(new Unit() { name = "archer" });
            units.Add(new Unit() { name = "cavalry" });
        }
    }

    // Start is called before the first frame update    

    public void initiateUnits()
    {
        army = new Army();
        var firstRoad = mapManager.map.getNodesList().First(t => t.name == "road");
        foreach (var unit in army.units)
        {
            var unitItem = unitGenerator.generateUnit(unit);
            unitItem.transform.position = firstRoad.gameObject.transform.position + Vector3.up *0.8f;
            unitItem.transform.localScale = Vector3.one * 0.3f;
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}
