using UnityEngine;
using System.Collections;

public class Generator : Node {

    public GameObject energyPrefab;

    public int generationBeatDelay;
    public Energy.EColor generationColor;

    public GameObject GenerateEnergy() {
        GameObject energy = (GameObject) Instantiate(energyPrefab);
        energy.transform.position = new Vector3(transform.position.x, transform.position.y, energyPrefab.transform.position.z);
        energy.transform.rotation = Quaternion.identity;

        energy.GetComponent<Energy>().eColor = generationColor;
        return energy;
    }
}
