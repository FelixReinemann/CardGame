using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

	public static GameBoard singleton;
	void Awake(){
		if(GameBoard.singleton==null){
			GameBoard.singleton = this;
		} else{
			Debug.Log("more than one gameboard_script");
			Destroy(this);
		}
	}
	public GameObject TilePrefab;

	public int height=9,width=12;

	public float tile_size=1;

    public bool mirror;

    public Tile_Data[] tiles;

	Vector3 tile_pos;

	

	public void createBoard(){
		tiles = new Tile_Data[height*width];
		/*for(int i=0; i < tiles.Length; i++){
			tiles[i] = new Tile_Data();
		}*/


		foreach(Tile_Behaviour child in transform.GetComponentsInChildren<Tile_Behaviour>()){
			DestroyImmediate(child.gameObject);
		}
		int index;
		for(int i=0; i<height; i++){
			for(int j=0; j<width; j++){
                if (mirror) {
                    tile_pos = new Vector3((float)j * -tile_size, 0, (float)i * tile_size);
                }else {
                    tile_pos = new Vector3((float)j * tile_size, 0, (float)i * tile_size);
                }
                GameObject tile_go = Instantiate(TilePrefab,tile_pos,Quaternion.identity,transform);
				index=i*width+j;

				//----Data-Structure---------------
				tiles[index] = new Tile_Data();
				tiles[index].index = index;
				tiles[index].myGO = tile_go;
				tiles[index].myBehaviour = tile_go.GetComponent<Tile_Behaviour>();

				if(i!=0){
					tiles[index].neighbours[0]=(i-1)*width+j;
				}
				if(i!=height-1){
					tiles[index].neighbours[1]=(i+1)*width+j;
				}
				if(j!=0){
					tiles[index].neighbours[2]=i*width+j-1;
				}
				if(j!=width-1){
					tiles[index].neighbours[3]=i*width+j+1;
				}
				//-----GO-Setup--------------
				tile_go.name = "Tile ["+index+"] "+i+" : "+j;
				tile_go.GetComponent<Tile_Behaviour>().myTile = tiles[index];
				tile_go.GetComponent<Tile_Behaviour>().index = index;

			}
		}

	}
	

}
