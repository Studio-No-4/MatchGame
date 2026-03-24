using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapNodeData StartingNode;
    public static List<List<MapNodeData>> NodeLayers = new();

    public Transform LayerPrefab;
    public MapNode NodePrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (StartingNode == null) GenerateMap();
        InstantiateVisuals();
    }

    public void GenerateMap()
    {
        StartingNode = new MapNodeData();
        List<MapNodeData> layer = new() { StartingNode };
        NodeLayers.Add(layer);
        for (int i = 0; i < 7; i++)
        {
            layer = new();
            for (int j = 0; j < Random.Range(3, 5); j++)
            {
                layer.Add(new());
            }
            NodeLayers.Add(layer);
        }
    }

    public void InstantiateVisuals()
    {
        for (int i = 0; i < NodeLayers.Count; i++)
        {
            Transform newLayer = Instantiate(LayerPrefab, transform);
            newLayer.SetAsFirstSibling();
            print("LAYER " + i.ToString());
            for (int j = 0; j < NodeLayers[i].Count; j++)
            {
                print(NodeLayers[i][j]);
                MapNode newNode = Instantiate(NodePrefab, newLayer);
                newNode.transform.GetChild(0).localPosition = NodeLayers[i][j].Offset;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class MapNodeData
{
    public List<MapNodeData> NextNodes;
    public Vector3 Offset;

    public MapNodeData(List<MapNodeData> _nextNodes = default)
    {
        NextNodes = _nextNodes;
        Offset = Random.insideUnitCircle * 20f;
    }
}
