using UnityEngine;
using System;
using System.Collections;

public class TimingController : MonoBehaviour {

    public int bpm = 120;
	// Timer, que conta tempo entre batidas
    private float timer = 0;
	// Contador de Batidas
    public int beatCounter;
	
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
