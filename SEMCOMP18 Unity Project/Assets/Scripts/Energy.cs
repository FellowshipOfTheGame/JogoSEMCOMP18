using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Energy : MonoBehaviour {

    public enum EColor {
        BLACK,
        RED,
        GREEN,
        YELLOW,
        BLUE,
        PURPLE,
        CYAN,
        WHITE
    }

    public EColor eColor;

    public void SetColor(EColor c){
        eColor = c;
        GetComponent<SpriteRenderer>().color = ToColor(c);
    }

    public static GameObject JoinEnergies(List<GameObject> energies) {
        GameObject result = null;
        foreach (GameObject go in energies) {
            if(go != null){
                Energy e = go.GetComponent<Energy>();
                if (e != null) {
                    result = e.JoinEnergy(result);
                }
            }
        }
        return result;
    }

    public GameObject JoinEnergy(GameObject other) {
        if (other != null) {
            Energy otherEnergy = other.GetComponent<Energy>();
            if (otherEnergy != null) {
                this.JoinColor(otherEnergy);
                Destroy(other);
            }
        }
        return gameObject;
    }

    public void JoinColor(Energy otherEnergy) {
        this.SetColor(Sum(this.eColor, otherEnergy.eColor));
    }

	public static EColor Sum(EColor a, EColor b){
        int result = (int)a + (int)b;
        if (result > (int)EColor.WHITE){
            result = (int)EColor.WHITE;
        }
        return (EColor)result;
    }

    public static Color ToColor(EColor eC) {
        Color result = Color.black;
        switch (eC) {
            case EColor.BLACK:
                result = Color.black;
                break;
            case EColor.BLUE:
                result = Color.blue;
                break;
            case EColor.CYAN:
                result = Color.cyan;
                break;
            case EColor.GREEN:
                result = Color.green;
                break;
            case EColor.PURPLE:
                result = Color.magenta;
                break;
            case EColor.RED:
                result = Color.red;
                break;
            case EColor.WHITE:
                result = Color.white;
                break;
            case EColor.YELLOW:
                result = Color.yellow;
                break;
        }
        return result;
    }
}
