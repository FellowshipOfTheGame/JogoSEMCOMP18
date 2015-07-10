using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class God_BHV : MonoBehaviour {
	
	public float Frequency = 1.0f;

	public float PathFactor = 0.0f;
	public bool SemiPath2 = false;
	public float SemiPathFactor = 0.5f;

	public int SemipathCount = 2;
	private float [] Semipath;
	private bool [] SemipathTrigger;
	private int TimeIndex = 0;

	public Material [] ColorMaterials;

	public bool GetSemipathTrigger (int Index){

		if (Index >= 0 && Index <= Semipath.Length){

			return SemipathTrigger [Index];

		}
		else {

			return false;

		}

	}

	public Material GetColorMaterial (int ColorIndex){

		if (ColorIndex >= 0 && ColorIndex < ColorMaterials.Length){

			return ColorMaterials [ColorIndex];
		
		}
		else{

			return ColorMaterials [0];

		}
	
	}

	public int BlendSpark (List <int> ColorList){
		
		int DefaultColor = -1;
		bool HasRed     = ColorList.Contains (0);
		bool HasGreen   = ColorList.Contains (1);
		bool HasBlue    = ColorList.Contains (2);
		bool HasCyan    = ColorList.Contains (3);
		bool HasYellow  = ColorList.Contains (4);
		bool HasMagenta = ColorList.Contains (5);
		bool HasWhite   = ColorList.Contains (6);
		
		if (HasWhite){
			
			DefaultColor = 6;
			
		}
		else if (HasYellow){
			
			if (HasBlue || HasMagenta || HasCyan){DefaultColor = 6;}
			else                                 {DefaultColor = 4;}
			
		}
		else if (HasMagenta){
			
			if (HasGreen || HasCyan){DefaultColor = 6;}
			else                    {DefaultColor = 5;}
			
		}
		else if (HasCyan){
			
			if (HasRed){DefaultColor = 6;}
			else       {DefaultColor = 3;}
			
		}
		
		//Only primaries
		else if ( HasRed &&  HasGreen &&  HasBlue){ DefaultColor = 6;}
		else if (!HasRed &&  HasGreen &&  HasBlue){ DefaultColor = 3;}
		else if ( HasRed && !HasGreen &&  HasBlue){ DefaultColor = 5;}
		else if ( HasRed &&  HasGreen && !HasBlue){ DefaultColor = 4;}
		else if ( HasRed && !HasGreen && !HasBlue){ DefaultColor = 0;}
		else if (!HasRed &&  HasGreen && !HasBlue){ DefaultColor = 1;}
		else if (!HasRed && !HasGreen &&  HasBlue){ DefaultColor = 2;}
		
		return DefaultColor;
		
	}

	// Use this for initialization
	void Start () {

		Semipath = new float[SemipathCount];
		SemipathTrigger = new bool[SemipathCount];

	}
	
	// Update is called once per frame
	void Update () {

		for (int i = Semipath.Length-1 ; i >= 0 ; i--){

			if (TimeIndex == i){

				Semipath [i] = Semipath [i] + Time.deltaTime*Frequency*((float)Semipath.Length);

				if (Semipath [i] >= 1.0f){
				
					Semipath [i] = 0.0f;
					SemipathTrigger [i] = true;	

					if (TimeIndex+1 < Semipath.Length){

						TimeIndex ++;

					}
					else{

						TimeIndex = 0;

					}

				}

			}
			else {

				SemipathTrigger [i] = false;	

			}

		}


		PathFactor = (1.0f/((float)Semipath.Length))*((float)TimeIndex+Semipath[TimeIndex]);

	}

}
