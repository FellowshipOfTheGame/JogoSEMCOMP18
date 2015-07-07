using UnityEngine;
using System.Collections;

public class Generator : Node {

    private int beatTimer = 0;
    public int generationBeatDelay = 1;
    public Energy.EColor generationColor;

    public override void OnBeat(int beatCounter) {
        beatTimer += beatCounter;
        if (beatTimer >= generationBeatDelay) {
            beatTimer -= generationBeatDelay;
            if(this.HaveOutput()){
                base.RecieveEnergy (GenerateEnergy());
            }
        }
        Rout(beatCounter);
    }

    public GameObject GenerateEnergy() {
		GameObject energy = (GameObject) Instantiate(energyPrefab, 
				new Vector3(transform.position.x, transform.position.y, energyPrefab.transform.position.z),
				Quaternion.identity);

        energy.GetComponent<Energy>().SetColor(generationColor);
        return energy;
    }
}
