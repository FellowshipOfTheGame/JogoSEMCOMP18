using UnityEngine;
using System.Collections;

public class LED : Node {


    public override void RecieveEnergy(GameObject energy) {
        energies.Add(energy);

        if (energies.Count > 0) {
            GameObject resultEnergy = Energy.JoinEnergies(energies);
            Light(resultEnergy.GetComponent<Energy>().eColor);
        }
    }

    public void Light(Energy.EColor ec) {

    }
}
