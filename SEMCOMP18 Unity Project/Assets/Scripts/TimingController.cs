using UnityEngine;
using System;
using System.Collections;

public class TimingController : MonoBehaviour {

    public int bpm = 120;
	// Timer, que conta tempo entre batidas
    private float timer = 0;

    private float delta = 0;
    private int beatsToRun = 0;

	// Use this for initialization
	// Contador de Batidas
    public int beatCounter = 0;
	
>>>>>>> origin/Visual
    void Update() {
	}

    void LateUpdate() {
		timer += Time.deltaTime;
		float beatsPassed = (timer * bpm / 60f);
        int beatsToRun = (int)Math.Floor (beatsPassed);
        beatCounter += beatsToRun;
        if(beatsToRun > 0) {
            FindObjectOfType<Grid>().Beat(beatsToRun);
			timer -= beatsToRun * (60f / bpm);
        }
	}
}
