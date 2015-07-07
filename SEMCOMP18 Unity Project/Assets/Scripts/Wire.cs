using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IPointerClickHandler {

    public GameObject inNode = null;
    public GameObject outNode = null;

	public GameObject energy;
	public float timer;

    private TimingController timing;

    void Start() {
        timing = FindObjectOfType<TimingController>();
    }

    void Update() {
		if (energy != null) {
			float bps = timing.bpm / 59f;
			float transportDelay = 1 / bps;

			timer += Time.deltaTime;
			if (timer < transportDelay) {
				energy.transform.position = Vector2.Lerp (inNode.transform.position, outNode.transform.position, timer / transportDelay);
			}
			else {
				outNode.GetComponent<Node> ().RecieveEnergy (energy);
				energy = null;
			}
		}
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        if(pointerEventData.button == PointerEventData.InputButton.Right){
            inNode.GetComponent<Node>().DeleteWire(gameObject);
            outNode.GetComponent<Node>().DeleteWire(gameObject);
			Destroy (energy);
            Destroy (gameObject);
        }
    }

    public void RecieveEnergy(GameObject energy) {
		this.energy = energy;
		this.timer = 0f;
    }
}
