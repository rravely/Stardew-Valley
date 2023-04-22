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
    [SerializeField]private List<TileBase> hoeDirt;
    [SerializeField]private TileBase waterDirt;

    //Refresh HoeDirt
    private List<int> hoeDirtDir;
    private int hoeDirtDirSum = 0;

    //seed
    [SerializeField]private Tilemap seedTileMap;
    [SerializeField]private List<TileBase> seedTile = new List<TileBase>();
    [SerializeField]private List<TileBase> beanTile = new List<TileBase>();

    //change coordinate 
    private int x, y;
    

    void Start() {
        farmMap = gameObject.GetComponent<FarmMap>();
        hoeDirtDir = new List<int>();
    }

    public void ChangeHoeDirt(Vector3 playerPos, int direction) {
        float x = playerPos.x;
        float y = playerPos.y - 0.1f; //다리 쪽으로 바꾸기
        playerPos = new Vector3(x, y, y);

        Vector3Int playerPosInt = dirtTileMap.LocalToCell(playerPos); //Vector3Int로 변환
        
        switch (direction) {
            case 1: //right
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-2, -1, 0), hoeDirt[0]);
                break;
            case 2:
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-4, -1, 0), hoeDirt[0]);
                break;
            case 3:
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-3, 0, 0), hoeDirt[0]);
                break;
            case 4:
                dirtTileMap.SetTile(playerPosInt + new Vector3Int(-3, -2, 0), hoeDirt[0]);
                break;
        }
        
    }

    public void RefreshHoeDirt(int posY, int posX) {
        for (int i = posY - 1; i < posY + 2; i++) {
            for (int j = posX - 1; j < posX + 2; j++) {
                hoeDirtDir.Clear();
                
                Debug.Log("상하좌우 확인");
                //상하좌우 확인해서 물뿌려진 곳이면 1 아니면 0 추가
                if (farmMap.farmResData[i + 1, j].Equals(5))  hoeDirtDir.Add(1);
                else  hoeDirtDir.Add(0); //상
                if (farmMap.farmResData[i - 1, j].Equals(5))  hoeDirtDir.Add(1);
                else hoeDirtDir.Add(0); //하
                if (farmMap.farmResData[i, j - 1].Equals(5)) hoeDirtDir.Add(1);
                else hoeDirtDir.Add(0); //좌
                if (farmMap.farmResData[i, j + 1].Equals(5)) hoeDirtDir.Add(1);
                else hoeDirtDir.Add(0); //우

                Debug.Log("1몇개인지 구하기");
                //1 몇개인지 구하기
                for (int k = 0; k < hoeDirtDir.Count; i++) {
                    hoeDirtDirSum += hoeDirtDir[k];
                }
                /*
                //좌표에 맞는 위치
                int x = 9 + j; //9 + j
                int y = 30 - i; //29 - i
                Vector3Int mapCellPos = new Vector3Int(x, y, 0);

                //1이 몇개인지에 따라 나누기
                switch (hoeDirtDirSum) {
                    case 0: //넷다 없는 경우
                        dirtTileMap.SetTile(mapCellPos, hoeDirt[0]);
                        break;
                    case 1: //넷 중에 한 방향만
                        if (hoeDirtDir[0].Equals(1)) { //상
                            dirtTileMap.SetTile(mapCellPos, hoeDirt[12]);
                        } else if (hoeDirtDir[1].Equals(1)) { //하
                            dirtTileMap.SetTile(mapCellPos, hoeDirt[4]);
                        } else if (hoeDirtDir[2].Equals(1)) { //좌
                            dirtTileMap.SetTile(mapCellPos, hoeDirt[15]);
                        } else if (hoeDirtDir[3].Equals(1)) { //우
                            dirtTileMap.SetTile(mapCellPos, hoeDirt[13]);
                        }
                        break;
                    case 2: //두 방향
                        if (hoeDirtDir[0].Equals(1) && hoeDirtDir[1].Equals(1)) { //상하 있는 경우
                            dirtTileMap.SetTile(mapCellPos, hoeDirt[8]); 
                        }
                        break;
                    case 3: //세 방향
                        if (hoeDirtDir[0].Equals(0)) {
                            dirtTileMap.SetTile(mapCellPos, hoeDirt[2]);
                        } else if (hoeDirtDir[0].Equals(0)) {
                            dirtTileMap.SetTile(mapCellPos, hoeDirt[10]);
                        } else if (hoeDirtDir[0].Equals(0)) {
                            dirtTileMap.SetTile(mapCellPos, hoeDirt[7]);
                        } else if (hoeDirtDir[0].Equals(0)) {
                            dirtTileMap.SetTile(mapCellPos, hoeDirt[5]);
                        }
                        break;
                    case 4: //네 방향 다
                        dirtTileMap.SetTile(mapCellPos, hoeDirt[6]);
                        break;
                    default:
                        dirtTileMap.SetTile(mapCellPos, hoeDirt[1]);
                        break;
                }
                */
                //합 0으로 만들기
                hoeDirtDirSum = 0;
            }
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
                if (farmMap.farmResData[i, j].Equals(5) && (farmMap.parsnipGrowing[i, j].Equals(0) || farmMap.beanGrowing[i, j].Equals(0))) { //호미질한 땅인데 씨가 자라지 않으면
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

                    dirtTileMap.SetTile(mapCellPos, hoeDirt[0]);
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

    public void ResetBean(Vector3 playerPos, int direction) {
        float x = playerPos.x;
        float y = playerPos.y - 0.1f; //다리 쪽으로 바꾸기
        playerPos = new Vector3(x, y, y);

        //seed tile map 갱신
        Vector3Int playerPosInt = seedTileMap.LocalToCell(playerPos); //Vector3Int로 변환
        switch (direction) {
            case 1: //right
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-2, -1, 0), beanTile[11]);
                break;
            case 2:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-4, -1, 0), beanTile[11]);
                break;
            case 3:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-3, 0, 0), beanTile[11]);
                break;
            case 4:
                seedTileMap.SetTile(playerPosInt + new Vector3Int(-3, -2, 0), beanTile[11]);
                break;
        }
    }
}
