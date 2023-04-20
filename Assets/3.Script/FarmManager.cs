using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmManager : MonoBehaviour
{
    //Save dropped item data
    [HideInInspector]public List<GameObject> droppedItem = new List<GameObject>();

    //Farm Map
    FarmMap farmMap;

    //dirt
    [SerializeField]private Tilemap dirtTileMap;
    [SerializeField]private TileBase hoeDirt;
    [SerializeField]private TileBase waterDirt;

    //seed
    [SerializeField]private Tilemap seedTileMap;
    [SerializeField]private List<TileBase> seedTile = new List<TileBase>();
    

    void Start() {
        farmMap = gameObject.GetComponent<FarmMap>();
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

    public void PlayerSeeding(Vector3 playerPos, int direction) {
        float x = playerPos.x;
        float y = playerPos.y - 0.1f; //다리 쪽으로 바꾸기
        playerPos = new Vector3(x, y, y);

        //seed tile map 갱신
        Vector3Int playerPosInt = dirtTileMap.LocalToCell(playerPos); //Vector3Int로 변환
        switch (direction) {
            case 1: //right
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-2, -1, 0), seedTile[0]);
                break;
            case 2:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-4, -1, 0), seedTile[0]);
                break;
            case 3:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-3, 0, 0), seedTile[0]);
                break;
            case 4:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-3, -2, 0), seedTile[0]);
                break;
        }
    }

    public void GrowningCrops() {
        for (int i = 0; i < 26; i++) {
            for (int j = 0; j < 43; j++) {
                if (farmMap.seedGrowing[i, j] > 0) {
                    farmMap.seedGrowing[i, j]++; //1 증가

                    //좌표에 맞는 위치
                    int x = 9 + j; //9 + j
                    int y = 30 - i; //29 - i
                    Vector3Int mapCellPos = new Vector3Int(x, y, 0);
                    
                    //작물 타일 바꾸기
                    seedTileMap.SetTile(mapCellPos, seedTile[farmMap.seedGrowing[i, j] - 1]);
                }
            }
        }
        ChangeWaterDirtToHoeDirt();
    }

    public void ChangeWaterDirtToHoeDirt() {
        for (int i = 0; i < 26; i++) {
            for (int j = 0; j < 43; j++) {
                if (farmMap.farmResData[i, j].Equals(6)) { //물 준 땅이면
                    farmMap.farmResData[i, j] = 5; //물 안준 땅으로 변경
                    
                    //좌표에 맞는 위치
                    int x = 9 + j; //9 + j
                    int y = 30 - i; //29 - i
                    Vector3Int mapCellPos = new Vector3Int(x, y, 0);

                    dirtTileMap.SetTile(mapCellPos, hoeDirt);
                }
            }
        }
    }
}
