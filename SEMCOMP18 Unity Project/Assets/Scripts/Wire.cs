using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IPointerClickHandler {

    public GameObject inNode = null;
    public GameObject outNode = null;

    public Queue<GameObject> energyReferences = new Queue<GameObject>();
    public Queue<float> energyTimers = new Queue<float>();
    public Queue<int> energyBeat = new Queue<int>();

    private TimingController timing;

    void Start() {
        timing = FindObjectOfType<TimingController>();
    }

    void Update() {
        float tempTimer;
        GameObject tempEnergy;

        float bps = timing.bpm / 60;
        float transportDelay = 1 / bps;

        int n = energyTimers.Count;

        for(int i = 0; i < n; i++){
            tempTimer = energyTimers.Dequeue() + Time.deltaTime;
            tempEnergy = energyReferences.Dequeue();
            if (tempTimer < transportDelay) {
                tempEnergy.transform.position = Vector2.Lerp(inNode.transform.position, outNode.transform.position, tempTimer / transportDelay);
                energyTimers.Enqueue(tempTimer);
                energyReferences.Enqueue(tempEnergy);
            }
            else {
                outNode.GetComponent<Node>().RecieveEnergy(tempEnergy);
            }
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        if(pointerEventData.button == PointerEventData.InputButton.Right){
            inNode.GetComponent<Node>().DeleteWire(gameObject);
            outNode.GetComponent<Node>().DeleteWire(gameObject);
            while (energyReferences.Count > 0) {
                Destroy(energyReferences.Dequeue());
            }
            Destroy(this.gameObject);
        }
    }

    public void RecieveEnergy(GameObject energy, int beatCounter) {
        energyReferences.Enqueue(energy);
        energyTimers.Enqueue(0f);
        energyBeat.Enqueue(beatCounter);
    }
}
