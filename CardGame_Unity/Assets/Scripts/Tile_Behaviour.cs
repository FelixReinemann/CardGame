using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Behaviour : MonoBehaviour {

	public int index;
	public Tile_Data myTile;

	public bool highlighted=false;

	public Material regularMaterial;
	public Material highlightedMaterial;
	public Renderer myGraphic;

	public void OnClick(){
		Debug.Log("clicked "+gameObject.name);
	}

	void Update(){
		
	}

	public void CheckForHighlight(){
		if(highlighted){
			myGraphic.material = highlightedMaterial;
		} else {
			myGraphic.material = regularMaterial;
		}
	}


}

public enum statuses { clear, blocked, player1, player2 }

[System.Serializable]
public class Tile_Data {
	public int index;
	public GameObject myGO;
	public Tile_Behaviour myBehaviour;
	public int[] neighbours;

	
	public statuses status;

	public Tile_Data(){
		neighbours = new int[4];
		for(int i=0; i < neighbours.Length; i++){
			neighbours[i]= -999;
            status = statuses.clear;
        }
	}

	//public Tile_Data[] adjacentTiles;

}
