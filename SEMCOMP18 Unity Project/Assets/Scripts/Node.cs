using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public GameObject[] wires = new GameObject[6];
    public int x;
    public int y;
    public GameObject wirePrefab;


    public void OnPointerDown(PointerEventData pointerEventData) {
    }

    public void OnPointerUp(PointerEventData pointerEventData) {
        GameObject otherObject = pointerEventData.pointerCurrentRaycast.gameObject;
        if (otherObject != null) {
            //Dropped on something...
            Node otherNode = otherObject.GetComponent<Node>();
            if (otherNode != null && otherNode != this) {
                //otherObject is a Node too! And its not me!
                if (IsNeighbor(otherNode)) {
                    Debug.Log("Other node is my neighbor!");//other node is my neighbor!
                    Grid.Direction dir = DirectionTo(otherNode);
                    if (Grid.IsValidDirection(dir)) {
                        Debug.Log("\tI have his direction!");
                        if (IsDirectionEmpty(dir)) {
                            Debug.Log("\t\tThere wasn't a wire between us yet.");//There wasn't a wire between us yet.
                            Vector2 pontoMedio = (Vector2)(transform.position + otherObject.transform.position) / 2;
                            GameObject wire = (GameObject)Instantiate(wirePrefab);
                            wire.transform.position = new Vector3(pontoMedio.x, pontoMedio.y, wirePrefab.transform.position.z);
                            float angle = Vector2.Angle(new Vector2(1, 0), (Vector2)(otherObject.transform.position - transform.position));
                            wire.transform.Rotate(new Vector3(0, 0, -1), (transform.position.y > otherObject.transform.position.y) ? angle : -angle);
                            //Now there is!
                            this.LinkWith(dir, wire);
                            otherNode.LinkWith(otherNode.DirectionTo(this),wire);
                            Wire wireScript = wire.GetComponent<Wire>();
                            wireScript.inNode = this.gameObject;
                            wireScript.outNode = otherObject;
                        }
                    }
                }
            }
        }
    }

    public bool IsNeighbor(Node other) {
        return Grid.AreNeighbors(this, other);
    }

    public Grid.Direction DirectionTo(Node other) {
        return Grid.GetDirection(this, other);
    }

    public bool IsDirectionEmpty(Grid.Direction dir){
        return (wires[(int)dir] == null);
    }

    public void LinkWith(Grid.Direction dir, GameObject wire) {
        wires[(int)dir] = wire;
    }

    public void DeleteWire(GameObject wire) {
        for (int i = 0; i < 6; i++) {
            if (wires[i] == wire) {
                wires[i] = null;
                return;
            }
        }
    }
}
