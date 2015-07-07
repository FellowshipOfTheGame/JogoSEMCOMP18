using UnityEngine;
using System.Collections;

public enum Note:int
{
    A = 0,
    B = 1,
    C = 2,
    D = 3,
    E = 4,
    F = 5,
    G = 6,

    Bb = 7,
    Db = 8,
    Eb = 9,
    Gb = 10,
    Ab = 11
}

public class Pitch
{

    public static float[] pitches = { 
                                        1.0f,             // A
                                        1.12246204831f,   // B
                                        1.189207115f,     // C
                                        1.33483985417f,   // D
                                        1.49830707688f,   // E
                                        1.58740105197f,   // F
                                        1.78179743628f,   // G

                                        1.05946309436f,   // A#/Bb
                                        1.25992104989f,   // C#/Db
                                        1.41421356237f,   // D#/Eb
                                        1.68179283051f,   // F#/Gb
                                        1.88774862536f,   // G#/Ab
                                    };

    public static void Play(AudioSource source, Note note)
    {
        source.Stop();
        try
        {
            source.pitch = pitches[(int)note];
        }
        catch (System.ArgumentOutOfRangeException)
        {

            source.pitch = pitches[(int)Note.A];
        }
        source.Play();
    }
}
