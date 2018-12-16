using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameBoard))]
public class Editor_GameBoard : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector();
		GameBoard myScript = (GameBoard)target;
		if(GUILayout.Button("Build Board")){
			myScript.createBoard();
		}
	}
}
