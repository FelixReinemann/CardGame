using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Controls : MonoBehaviour {

	public Camera activeCam;
	int mask, cardMask;
	Unit_Behaviour selectedUnit;
    Tile_Behaviour selectedTile;

    Card_Behaviour hoveredCard;

    void Start(){
		activeCam = Camera.main;
		mask = 1 << LayerMask.NameToLayer("Units");
		mask |= 1 << LayerMask.NameToLayer("Tiles");
        cardMask = 1 << LayerMask.NameToLayer("Cards");

    }

	RaycastHit hit;
    Ray ray;
	void Update(){
		if(Input.GetMouseButtonUp(0)){
			//if(Physics.Raycast(activeCam.transform.position,activeCam.transform.forward, out hit,Mathf.Infinity,mask)){
			ray = activeCam.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray,out hit,Mathf.Infinity,mask)){
				if(hit.rigidbody.tag == "Unit"){
                    if (selectedUnit != hit.rigidbody.GetComponent<Unit_Behaviour>()) {
                        ClearHighlightedAreas();
                        ClearSelectedUnits();
                    }
                    selectedUnit = hit.rigidbody.GetComponent<Unit_Behaviour>();
					selectedUnit.OnClick();
					if(!selectedUnit.exhausted){
						HighLightWalkableTiles(selectedUnit.FindTileBeneath(),selectedUnit.myStats.value_Movement);
					}
				}
				if(hit.rigidbody.tag == "Tile"){
                    selectedTile = hit.rigidbody.GetComponent<Tile_Behaviour>();
                    if (selectedUnit != null) {
                        if (selectedTile.highlighted) {
                            selectedUnit.MoveUnitToTile(selectedTile.myTile);
                        }
                    }
                    ClearHighlightedAreas();
                    ClearSelectedUnits();
                    //hit.rigidbody.GetComponent<Tile_Behaviour>().OnClick();
                }
			} else if (Physics.Raycast(ray, out hit, Mathf.Infinity, cardMask)) {
                hit.collider.GetComponent<Card_Behaviour>().PlayCard();
            }
        }
        CheckForCards();
    }
    
    void CheckForCards() {
        ray = activeCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, cardMask)) {
            if (hoveredCard!=null && hoveredCard != hit.collider.gameObject.GetComponent<Card_Behaviour>()) {
                hoveredCard.mouseOver = false;
            }
            hoveredCard = hit.collider.gameObject.GetComponent<Card_Behaviour>();
            hoveredCard.mouseOver = true;
        } else if(hoveredCard!=null){
            hoveredCard.mouseOver = false;
        }

    }

    List<int> indexesToCheck;
	List<int> indexesChecked;
	List<int> indexesOfHighlighted;
	void HighLightWalkableTiles(Tile_Data tile, int walkLength){
        //Debug.Log("--------------------");
		int steps = 0;
		int thisStepSize = 0;
		indexesToCheck = new List<int>();
		indexesChecked = new List<int>();
		indexesOfHighlighted = new List<int>();
		indexesToCheck.Add(tile.index);
		while(steps < walkLength){
			thisStepSize = indexesToCheck.Count;
			for(int j=0; j < thisStepSize;j++){
				Tile_Data tileToCheck = GameBoard.singleton.tiles[ indexesToCheck[0] ];
				//Debug.Log(tileToCheck.myGO.name);
				//checking one tile for neighbours
				for(int i=0; i<tileToCheck.neighbours.Length;i++){
					if(tileToCheck.neighbours[i]>=0){
						if(!indexesChecked.Contains(tileToCheck.neighbours[i])){
							if(!indexesToCheck.Contains(tileToCheck.neighbours[i])){
								

                                if (GameBoard.singleton.tiles[tileToCheck.neighbours[i]].status == statuses.clear) {
                                    //Debug.Log(GameBoard.singleton.tiles[tileToCheck.neighbours[i]].myGO.name + " is clear");
                                    GameBoard.singleton.tiles[tileToCheck.neighbours[i]].myBehaviour.highlighted = true;
                                    GameBoard.singleton.tiles[tileToCheck.neighbours[i]].myBehaviour.CheckForHighlight();
                                    indexesOfHighlighted.Add(tileToCheck.neighbours[i]);
                                    indexesToCheck.Add(tileToCheck.neighbours[i]);
                                } else if (GameBoard.singleton.tiles[tileToCheck.neighbours[i]].status == selectedUnit.unitOwner) {
                                    //Debug.Log(GameBoard.singleton.tiles[tileToCheck.neighbours[i]].myGO.name + " is owned by my unit");
                                    indexesToCheck.Add(tileToCheck.neighbours[i]);
                                } else {
                                    //Debug.Log(GameBoard.singleton.tiles[tileToCheck.neighbours[i]].myGO.name + " is blocked");
                                }

                            } else {
								//Debug.Log(tileToCheck.neighbours[i]+" is already in list to be checked");
							}
						} else {
							//Debug.Log(tileToCheck.neighbours[i]+" is already checked");
						}
					}
				}
				indexesToCheck.Remove(tileToCheck.index);
				indexesChecked.Add(tileToCheck.index);
			}
            
            steps++;
		}
	}

	public void ClearHighlightedAreas(){
		if(indexesOfHighlighted==null || indexesOfHighlighted.Count ==0){
			return;
		}
		for(int i=0; i < indexesOfHighlighted.Count;i++){
			GameBoard.singleton.tiles[indexesOfHighlighted[i]].myBehaviour.highlighted=false;
			GameBoard.singleton.tiles[indexesOfHighlighted[i]].myBehaviour.CheckForHighlight();
		}
	}

    public void ClearSelectedUnits() {
        if (selectedUnit != null) {
            selectedUnit.selected_active = false;
            selectedUnit = null;
        }
    }


}
