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
    [SerializeField]private List<TileBase> beanTile = new List<TileBase>();

    //change coordinate 
    private int x, y;
    

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
        Vector3Int playerPosInt = seedTileMap.LocalToCell(playerPos); //Vector3Int로 변환
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

    public void PlayerBeanFarm(Vector3 playerPos, int direction) {
        float x = playerPos.x;
        float y = playerPos.y - 0.1f; //다리 쪽으로 바꾸기
        playerPos = new Vector3(x, y, y);

        //seed tile map 갱신
        Vector3Int playerPosInt = seedTileMap.LocalToCell(playerPos); //Vector3Int로 변환
        switch (direction) {
            case 1: //right
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-2, -1, 0), beanTile[0]);
                break;
            case 2:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-4, -1, 0), beanTile[0]);
                break;
            case 3:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-3, 0, 0), beanTile[0]);
                break;
            case 4:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-3, -2, 0), beanTile[0]);
                break;
        }
    }

    public void GrowningCrops() {
        for (int i = 0; i < 26; i++) {
            for (int j = 0; j < 43; j++) {
                if (farmMap.farmResData[i, j].Equals(6)) { //물 준 상태면
                    if (farmMap.parsnipGrowing[i, j] > 0) { //파스닙이 있다면
                        farmMap.parsnipGrowing[i, j]++; //1 증가

                        //좌표에 맞는 위치
                        x = 9 + j; //9 + j
                        y = 30 - i; //29 - i
                        Vector3Int mapCellPos = new Vector3Int(x, y, 0);
                    
                        //작물 타일 바꾸기
                        seedTileMap.SetTile(mapCellPos, seedTile[farmMap.parsnipGrowing[i, j] - 1]);
                    }
                    else if (farmMap.beanGrowing[i, j] > 0) { //완두콩이 있다면
                        farmMap.beanGrowing[i, j]++; //1 증가

                        //좌표에 맞는 위치
                        x = 9 + j; //9 + j
                        y = 30 - i; //29 - i
                        Vector3Int mapCellPos = new Vector3Int(x, y, 0);
                    
                        //작물 타일 바꾸기
                        if (farmMap.beanGrowing[i, j] < 12) {
                            seedTileMap.SetTile(mapCellPos, beanTile[farmMap.beanGrowing[i, j] - 1]);
                        } else if (farmMap.beanGrowing[i, j].Equals(14)) {
                            seedTileMap.SetTile(mapCellPos, beanTile[10]);
                        } else {
                            seedTileMap.SetTile(mapCellPos, beanTile[11]);
                        }

                    }
                }
            }
        }
        ChangeDirtState();
    }
    
    private void ChangeDirtState() {
        for (int i = 0; i < 26; i++) {
            for (int j = 0; j < 43; j++) {
                if (farmMap.farmResData[i, j].Equals(5) && farmMap.parsnipGrowing[i, j].Equals(0)) { //호미질한 땅인데 씨가 자라지 않으면
                    farmMap.farmResData[i, j] = 0; //아무 것도 없는 땅으로 변경

                    //좌표에 맞는 위치
                    x = 9 + j; //9 + j
                    y = 30 - i; //29 - i
                    Vector3Int mapCellPos = new Vector3Int(x, y, 0);

                    dirtTileMap.SetTile(mapCellPos, null);
                }
                else if (farmMap.farmResData[i, j].Equals(6)) { //물 준 땅이면
                    farmMap.farmResData[i, j] = 5; //물 안준 땅으로 변경
                    
                    //좌표에 맞는 위치
                    x = 9 + j; //9 + j
                    y = 30 - i; //29 - i
                    Vector3Int mapCellPos = new Vector3Int(x, y, 0);

                    dirtTileMap.SetTile(mapCellPos, hoeDirt);
                }
            }
        }
    }

    public void ResetDirt(Vector3 playerPos, int direction) {
        float x = playerPos.x;
        float y = playerPos.y - 0.1f; //다리 쪽으로 바꾸기
        playerPos = new Vector3(x, y, y);

        //seed tile map 갱신
        Vector3Int playerPosInt = seedTileMap.LocalToCell(playerPos); //Vector3Int로 변환
        switch (direction) {
            case 1: //right
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-2, -1, 0), null);
                break;
            case 2:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-4, -1, 0), null);
                break;
            case 3:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-3, 0, 0), null);
                break;
            case 4:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-3, -2, 0), null);
                break;
        }
    }
}
