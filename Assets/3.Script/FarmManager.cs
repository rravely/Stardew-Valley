using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmManager : MonoBehaviour
{
    //Save dropped item data
    [HideInInspector]public List<GameObject> droppedItem = new List<GameObject>();

    //dirt
    [SerializeField]private Tilemap dirtTileMap;
    [SerializeField]private TileBase hoeDirt;
    [SerializeField]private TileBase waterDirt;
    

    void SpawnItem(){
        
    }

    public void ChangeHoeDirt(Vector3 playerPos, int direction) {
        float x = playerPos.x;
        float y = playerPos.y - 0.1f; //다리 쪽으로 바꾸기
        playerPos = new Vector3(x, y, y);

        Vector3Int playerPosInt = dirtTileMap.LocalToCell(playerPos); //Vector3Int로 변환
        //Debug.Log(playerPosInt.x + "," + playerPosInt.y);
        
        switch (direction) {
            case 1: //right
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-2, -1, 0), hoeDirt);
                break;
            case 2:
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-4, -1, 0), hoeDirt);
                break;
            case 3:
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-3, 0, 0), hoeDirt);
                break;
            case 4:
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-3, -2, 0), hoeDirt);
                break;
        }
        
    }

    public void ChangeWateringDirt(Vector3 playerPos, int direction) {
        float x = playerPos.x;
        float y = playerPos.y - 0.1f; //다리 쪽으로 바꾸기
        playerPos = new Vector3(x, y, y);

        Vector3Int playerPosInt = dirtTileMap.LocalToCell(playerPos); //Vector3Int로 변환
        switch (direction) {
            case 1: //right
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-2, -1, 0), waterDirt);
                break;
            case 2:
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-4, -1, 0), waterDirt);
                break;
            case 3:
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-3, 0, 0), waterDirt);
                break;
            case 4:
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-3, -2, 0), waterDirt);
                break;
        }
    }
}
