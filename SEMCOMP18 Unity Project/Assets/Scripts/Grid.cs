using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;

public class Grid : MonoBehaviour {
	/// Prefab do Nó, a ser criado pela Grid
	public GameObject node;
	/// Matriz de todos os nós
	private GameObject[] allNodes;

	public int linhas;
	public int colunas;

	// Use this for initialization
	void Start () {
		float diameter = node.transform.localScale.x;
		float radius = diameter / 2.0f;
		float side = diameter / ((float)Math.Sqrt (3));
		Vector3 basePosition = new Vector3 (colunas / (-2.0f) - radius, linhas / (-2.0f) + radius, 0);

		allNodes = new GameObject[linhas * colunas];

		for (int i = 0; i < linhas; i++) {
			for (int j = 0; j < colunas; j++) {
				GameObject obj = (GameObject) Instantiate (node, new Vector3 ((j + i/2.0f) * diameter, i * 1.5f * side, -5) + basePosition, Quaternion.identity);
				obj.transform.parent = transform;
				allNodes[(i * colunas) + j] = obj;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
