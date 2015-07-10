using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;


public class LevelSelect : MonoBehaviour {

	public GameObject levelButtonPrefab;
	public int columns;
	public int levels;
	int screenWidth = Screen.width;
	int screenHeight = Screen.height;
	Text txt;


	// Use this for initialization
	void Start () {
		//Deve gerar e ordenar os botoes de escolha de niveis
		int line = 0; // linha atual
		//nao permite um numero nulo ou negativo para as colunas
		if (columns <= 0)
			columns = 1;

		for (int i=0; i<levels; i++) {
			GameObject level = (GameObject)Instantiate (levelButtonPrefab);
			Button b = level.GetComponent<Button>();
			//set transforms
			b.transform.position = new Vector3( (screenWidth/columns)*((i%columns)+0.5f), screenHeight-(screenHeight/columns)*(int)(i/columns+1),0);
			b.transform.SetParent(transform);
			//txt = b.GetComponentInChildren<Text>().text;
			txt = ((Text)b.GetComponentInChildren(typeof(Text)));
			txt.text = "Level "+(i+1);
			b.name = txt.text;
			b.onClick.AddListener(() => load_level(b.name));
			//break line
			if(i%columns == columns-1){
				line++;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//public void load_level(string levelName){
	//	Application.LoadLevel(levelName);
	//	GUI button;
	//	button.GetComponentInChildren(Tex);
	//}

	public static void load_level(string levelName){
		Application.LoadLevel(levelName);
	}
}
