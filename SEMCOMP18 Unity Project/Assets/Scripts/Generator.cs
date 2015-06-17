using UnityEngine;
using System.Collections;

public class Generator : Node {

    private int beatTimer = 0;
    public int generationBeatDelay = 2;
    public Energy.EColor generationColor;

    public override void OnBeat(int beatCounter) {
        beatTimer++;
        if (beatTimer == generationBeatDelay) {
            beatTimer = 0;
            if(this.HaveOutput()){
                GameObject energy = GenerateEnergy();
                energies.Add(energy);
            }
        }
        Rout(beatCounter);
    }

    public GameObject GenerateEnergy() {
        GameObject energy = (GameObject) Instantiate(energyPrefab);
        energy.transform.position = new Vector3(transform.position.x, transform.position.y, energyPrefab.transform.position.z);
        energy.transform.rotation = Quaternion.identity;

        energy.GetComponent<Energy>().SetColor(generationColor);
        return energy;
    }
}
