using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MapManager.Map;

public class MapEditorTileGenerator : MonoBehaviour
{
    public GameObject genericTilePrefab;
    public Transform mapParent;

    [Serializable]
    public class NodeInterface
    {
        public string name;
        public GameObject model;
        public float height;
    }
    [SerializeField]
    public List<NodeInterface> nodeInterfaces;

    [Serializable]
    public class RoadTileInterface
    {
        public GameObject model;
        public bool[] entrances;
    }

    [SerializeField]
    public RoadTileInterface[] roadTileInterfaces;


    [Serializable]
    public class BuildingInterface
    {
        public string name;
        public GameObject model;
    }
    [SerializeField]
    public List<BuildingInterface> buildingInterfaces;

    // Start is called before the first frame update    

    public GameObject instantiateNodeObj(Node node)
    {
        GameObject result = null;
        if (node.name == "road")
        {
            result = GameObject.Instantiate(getRoadPrefab(node));
            node.height = 1.0f;
        }
        else
        {
            var nodeInterface = nodeInterfaces.First(t => t.name == node.name);
            result = GameObject.Instantiate(nodeInterface.model);
            node.height = nodeInterface.height;
        }

        if (node.building != "")
        {
            var buildingInterface = buildingInterfaces.First(t => t.name == node.building);
            var buildingObj = GameObject.Instantiate(buildingInterface.model, result.transform);
            buildingObj.transform.localScale = Vector3.one * 0.25f;
            buildingObj.transform.localPosition = Vector3.up;
            buildingObj.transform.Rotate(new Vector3(0, 0, 0));
        }
        var generic = GameObject.Instantiate(genericTilePrefab, Vector3.zero, Quaternion.identity);
        result.transform.SetParent(generic.transform);
        result.transform.localPosition = Vector3.zero;
        generic.transform.SetParent(mapParent); // extra cleaning, not important
        return generic;

    }

    private GameObject getRoadPrefab(Node node)
    {
        bool[] entrances = new bool[6];
        for (int i = 0; i < 6; i++)
        {
            entrances[i] = node.data.Contains((i + 1).ToString());
        }
        foreach (var item in roadTileInterfaces)
        {
            var angleToFit = getAngleToFit(item, entrances);
            if (angleToFit >= 0)
            {
                Debug.Log(angleToFit + ":" + node.data);
                item.model.transform.rotation = Quaternion.identity;
                item.model.transform.Rotate(new Vector3(0, -angleToFit, 0));
                return item.model;
            }
        }
        return null;
    }

    public static int getAngleToFit(RoadTileInterface target, bool[] entrances)
    {
        bool isMatch(bool[] a, bool[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
        for (int i = 0; i < 6; i++) // rotate to see if it fits
        {
            if (isMatch(target.entrances, Shift(entrances, i)))
                return i * 60;
        }

        return -1;
    }


    static bool[] Shift(bool[] array, int k)
    {
        var result = new bool[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            if ((i + k) >= array.Length)
                result[(i + k) - array.Length] = array[i];
            else
                result[i + k] = array[i];
        }
        return result;
    }
}
