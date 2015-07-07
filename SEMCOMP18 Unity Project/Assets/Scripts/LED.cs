using UnityEngine;
using System.Collections;

public class LED : Node {
    private AudioSource _source;
    private int beat;

    public Note[] notes;

    public void Start()
    {
        _source = GetComponent<AudioSource>();
        beat = 0;
    }
    public override void RecieveEnergy(GameObject energy) {
        base.RecieveEnergy (energy);
        if (energies.Count > 0) {
            GameObject resultEnergy = Energy.JoinEnergies(energies);
            Light(resultEnergy.GetComponent<Energy>().eColor);
            if (notes != null && notes.Length > 0)
            {
                Pitch.Play(_source, notes[beat]);
            }
        }
    }

    public override void OnBeat(int beatCounter)
    {
        base.OnBeat(beatCounter);
        if (notes != null && notes.Length > 0)
        {
            beat = beatCounter % notes.Length;
		}
        if (energy != null) {
            Light(energy.GetComponent<Energy> ().eColor);
        }
    }

    public void Light(Energy.EColor ec) {

    }
}
