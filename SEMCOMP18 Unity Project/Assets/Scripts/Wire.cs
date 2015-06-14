using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IPointerClickHandler {

    public GameObject inNode = null;
    public GameObject outNode = null;

    public void OnPointerClick(PointerEventData pointerEventData) {
        if(pointerEventData.button == PointerEventData.InputButton.Right){
            inNode.GetComponent<Node>().DeleteWire(gameObject);
            outNode.GetComponent<Node>().DeleteWire(gameObject);
            Destroy(this.gameObject);
        }
    }
}
