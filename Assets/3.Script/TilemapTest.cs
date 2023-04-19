using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TilemapTest : MonoBehaviour
{
    public Camera mainCamera;
    public Tilemap tileMap;
    public TileBase tileBase;
    private Vector3 clickPos = Vector3.zero;
    private Vector3Int tilemapCell = Vector3Int.zero;

    public GameObject player;

    void Update() {
        ChangeTile();
    }

    /*
	IEnumerator OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            yield break;
        }

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        
        Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 tilePos = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z)) - transform.position;

        _tilemapCell = _tileMap.LocalToCell(tilePos);        
        _clickPos = _tileMap.GetCellCenterLocal(_tilemapCell);

        Debug.Log(_tilemapCell);

        yield return null;
    }
    */

    void ChangeTile() {
        tilemapCell = tileMap.LocalToCell(player.transform.position);
        tileMap.SetTile(tilemapCell + new Vector3Int(0, -1, 0), tileBase);
    }

}
