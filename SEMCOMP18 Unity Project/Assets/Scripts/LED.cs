using UnityEngine;
using System.Collections;

public class LED : Node {


    public override void RecieveEnergy(GameObject energy) {
        base.RecieveEnergy (energy);

        if (energy != null) {
            Light(energy.GetComponent<Energy> ().eColor);
        }
    }

    public void Light(Energy.EColor ec) {

    }
}
