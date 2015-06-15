using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    public GameObject[] wires = new GameObject[6];
    public int x;
    public int y;
    public GameObject wirePrefab;

    public List<GameObject> energies = new List<GameObject>();

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
                    //other node is my neighbor!
                    Grid.Direction dir = DirectionTo(otherNode);
                    if (Grid.IsValidDirection(dir)) {
                        if (IsDirectionEmpty(dir)) {
                            //There wasn't a wire between us yet.
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

    public virtual void OnBeat() {
        Rout();
    }

    public void Rout() {
        if (energies.Count > 0) {
            GameObject resultEnergy = Energy.JoinEnergies(energies);
            if (resultEnergy != null) {
                for (int i = 0; i < 6; i++) {
                    if (wires[i] != null) {
                        Wire wire = wires[i].GetComponent<Wire>();
                        if (wire.outNode != this) {
                            wire.RecieveEnergy((GameObject)(Instantiate(resultEnergy)));
                        }
                    }
                }
                Destroy(resultEnergy);
            }
            energies.Clear();
        }
    }

    public virtual void RecieveEnergy(GameObject energy) {
        energies.Add(energy);
        energy.transform.position = this.transform.position;
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

    public bool HaveOutput() {
        for (int i = 0; i < 6; i++) {
            if (wires[i] != null) {
                Wire wire = wires[i].GetComponent<Wire>();
                if (wire.outNode != this) {
                    return true;
                }
            }
        }
        return false;
    }
}
