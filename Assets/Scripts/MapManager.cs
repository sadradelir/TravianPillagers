using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using System.Text.RegularExpressions;

public class MapManager : MonoBehaviour
{
    public MapEditorTileGenerator nodePrefabGenerator;
    public TextAsset mapFile;
    public Map map;

    public class Map
    {

        Dictionary<String, String> mapCodes = new Dictionary<string, string>{
            {"land" , "_"},
            {"water" , "w"},
        };

        public class Node
        {
            public string name;
            public string data;
            public string building = "";
            public float height = 0;
            public GameObject gameObject;
        }
        public Node[,] nodes;
        private List<Node> nodesList;

        public List<Node> getNodesList()
        {
            if (nodesList != null && nodesList.Count > 0)
            {
                return nodesList;
            }
            else
            {
                var result = new List<Node>();
                for (int j = 0; j < nodes.GetLength(0); j++)
                {
                    for (int i = 0; i < nodes.GetLength(0); i++)
                    {
                        result.Add(nodes[i, j]);
                    }
                }
                nodesList = result;
                return result;
            }
        }

        public Map(TextAsset mapFile)
        {
            var lines = mapFile.text.Split('\n');
            nodes = new Node[7, 13];

            for (int j = 0; j < nodes.GetLength(0); j++)
            {
                var line = lines[j].Split(',');
                for (int i = 0; i < nodes.GetLength(0); i++)
                {
                    var mapCode = line[i];
                    if (mapCode == mapCodes["land"])
                    {
                        nodes[i, j] = new Node() { name = "land" };
                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            nodes[i, j].building = Random.Range(0, 100) < 50 ? "tower" : "barracks";
                        }
                    }
                    else if (mapCode == mapCodes["water"])
                    {
                        nodes[i, j] = new Node() { name = "water" };
                    }
                    else if (Regex.Match(mapCode, @"\d+", RegexOptions.IgnoreCase).Success) //numbers 
                    {
                        nodes[i, j] = new Node() { name = "road", data = mapCode };
                    }
                }
            }
        }
    }


    public void initiateMap()
    {
        map = new Map(mapFile);
        for (int j = 0; j < map.nodes.GetLength(0); j++)
        {
            for (int i = 0; i < map.nodes.GetLength(0); i++)
            {
                var nodeGameObj = nodePrefabGenerator.instantiateNodeObj(map.nodes[i, j]);

                var xOffset = i % 2 == 1 ? 0 : 1;
                nodeGameObj.transform.position = new Vector3(Mathf.Sqrt(3) * i, map.nodes[i, j].height, 2 * j + xOffset);
                nodeGameObj.transform.Rotate(new Vector3(0, 30, 0));
                nodeGameObj.name = $"( {i} , {j} )";
                map.nodes[i, j].gameObject = nodeGameObj;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
