using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Behaviour : MonoBehaviour {

    //public enum players{player1,player2}
    //public players unitOwner = 0;
    public statuses unitOwner = statuses.player1;

    public Unit_Data myUnit;
    public Unit_Stats myStats;
    public int lifeLeft;

	public bool exhausted=true;
    [Header("Logic-Stuff")]

    public Card_Data myCard;

    public int tile_index;
	public bool selected_active=false;

    void Start() {
        //Claim Tile
        FindTileBeneath().status = unitOwner;
        GameBoard.singleton.tiles[ FindTileBeneath().index ].status = unitOwner;
        myStats = myUnit.unitStats;
    }

    public void OnClick(){
		//Debug.Log("clicked "+gameObject.name);
		//FindTileBeneath().myBehaviour.OnClick();
		if(!selected_active){
			selected_active = true;
		}
		tile_index = FindTileBeneath().index;
	}

    public void MoveUnitToTile(Tile_Data targetTile) {
        GameBoard.singleton.tiles[tile_index].status = statuses.clear;

        transform.position = targetTile.myGO.transform.position;
        tile_index = targetTile.index;

        GameBoard.singleton.tiles[tile_index].status = unitOwner;
        targetTile.status = unitOwner;

        exhausted = true;
    }

    int tileMask;
	RaycastHit hit;
	public Tile_Data FindTileBeneath(){
		tileMask = 1 << LayerMask.NameToLayer("Tiles");
		if(Physics.Raycast(transform.position+Vector3.up,Vector3.down,out hit,10,tileMask)){
			return hit.rigidbody.GetComponent<Tile_Behaviour>().myTile;
		} else {
			Debug.Log("Couldnt find tile beneath Unit "+gameObject.name);
			return null;

		}
	}

}

[System.Serializable]
public class Unit_Stats{
	public int value_Attack;
	public int value_Life;
	public int value_Movement;
}
