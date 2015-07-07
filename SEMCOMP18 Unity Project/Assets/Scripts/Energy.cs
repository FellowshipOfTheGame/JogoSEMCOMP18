using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Energy : MonoBehaviour {
	// Cores básicas são bits separados, pra podermos somar facilmente com um OR bit-a-bit
	// As outras cores devem estar nessa ordem bonitinho, pra que a "soma" dê certo
    public enum EColor {
        BLACK = 0,
        RED = 1,
        GREEN = 2,
        YELLOW = 3,
        BLUE = 4,
        PURPLE = 5,
        CYAN = 6,
        WHITE = 7
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
        this.SetColor (Sum (this.eColor, otherEnergy.eColor));
    }

	public static EColor Sum(EColor a, EColor b){
        return (EColor)((int) a | (int) b);
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
