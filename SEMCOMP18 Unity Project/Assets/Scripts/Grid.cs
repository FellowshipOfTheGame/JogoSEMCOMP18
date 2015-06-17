using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;

public class Grid : MonoBehaviour {
    /// Prefab do Nó, a ser criado pela Grid
    public GameObject nodePrefab;
    public GameObject GeneratorPrefab;
	/// Matriz de todos os nós
	private GameObject[] allNodes;

	public int linhas;
	public int colunas;

    public enum Direction { StraightRight, UpperRight, UpperLeft, StraightLeft, BottomLeft, BottomRight, NONE}

	// Use this for initialization
	void Start () {
		float diameter = nodePrefab.transform.localScale.x;
		float radius = diameter / 2.0f;
		float side = diameter / ((float)Math.Sqrt (3));
		Vector3 basePosition = new Vector3 (colunas / (-2.0f) - radius, linhas / (-2.0f) + radius, 0);

		allNodes = new GameObject[linhas * colunas];

		for (int i = 0; i < linhas; i++) {
			for (int j = 0; j < colunas; j++) {
				GameObject obj = (GameObject) Instantiate (nodePrefab);
                obj.transform.position = new Vector3((j + i / 2.0f) * diameter, i * 1.5f * side, nodePrefab.transform.position.z) + basePosition;
                obj.transform.rotation = Quaternion.identity;
				obj.transform.parent = transform;
				allNodes[(i * colunas) + j] = obj;
                Node nodeScript = obj.GetComponent<Node>();
                if (nodeScript != null) {
                    nodeScript.x = j;
                    nodeScript.y = i;
                }
			}
		}
        Destroy(allNodes[0]);
        GameObject o = (GameObject)Instantiate(GeneratorPrefab);
        o.transform.position = new Vector3(0, 0, GeneratorPrefab.transform.position.z) + basePosition;
        o.transform.rotation = Quaternion.identity;
		o.transform.parent = transform;
        allNodes[0] = o;
        Generator genScript = o.GetComponent<Generator>();
        if (genScript != null) {
            genScript.x = 0;
            genScript.y = 0;
        }
	}

    public void Beat(int beatCounter) {
        for (int i = 0; i < allNodes.Length; i++) {
            allNodes[i].GetComponent<Node>().OnBeat(beatCounter);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public static bool AreNeighbors(Node n1, Node n2) {
        return  ((n1.x + 1 == n2.x) && (n1.y == n2.y))     ||
                ((n1.x + 1 == n2.x) && (n1.y - 1 == n2.y)) || 
                ((n1.x == n2.x)     && (n1.y - 1 == n2.y)) || 
                ((n1.x - 1 == n2.x) && (n1.y == n2.y))     || 
                ((n1.x - 1 == n2.x) && (n1.y + 1 == n2.y)) || 
                ((n1.x == n2.x)     && (n1.y + 1 == n2.y));
    }

    public static Direction GetDirection(Node from, Node to) {
        if ((from.x + 1 == to.x) && (from.y == to.y)) { return Direction.StraightRight; }
        else if ((from.x + 1 == to.x) && (from.y - 1 == to.y)) { return Direction.UpperRight; }
        else if ((from.x == to.x) && (from.y - 1 == to.y)) { return Direction.UpperLeft; }
        else if ((from.x - 1 == to.x) && (from.y == to.y)) { return Direction.StraightLeft; }
        else if ((from.x - 1 == to.x) && (from.y + 1 == to.y)) { return Direction.BottomLeft; }
        else if ((from.x == to.x) && (from.y + 1 == to.y)) { return Direction.BottomRight; }
        else return Direction.NONE;
    }

    public static bool IsValidDirection(Direction dir) {
        return dir != Direction.NONE;
    }
}
