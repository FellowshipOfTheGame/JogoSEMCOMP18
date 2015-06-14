using UnityEngine;
using System.Collections;

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

    private EColor ecolor;

    private SpriteRenderer sprite;
    
    public EColor eColor {
        get {
            return ecolor;
        }
        set {
            ecolor = value;
            if (sprite != null) {
                sprite.color = ToColor(ecolor);
            }
        }
    }
    
    void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

	public static EColor Join(EColor a, EColor b){
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
