using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Item item; 
    public Image image; 
    public Text countText;
    public Transform toolbar;
    public PlayerControl playerControl;
    [HideInInspector] public bool clicked = false;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform currentParent;
    [HideInInspector] public int count = 1;

    void Start() 
    {
        //마우스 커서의 위치를 잡기 위하여 toolbar를 가져온다. 
        toolbar = GameObject.FindWithTag("Toolbar").transform;
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }
    public void InitialiseItem(Item newItem)
    {
        //새로운 아이템이 들어오면 sprite 설정
        item = newItem;
        image.sprite = newItem.icon;
        RefreshCount();
    }


    public void RefreshCount(){ //아이템의 개수 증가
        countText.text = count.ToString();
        if (count > 1){
            countText.gameObject.SetActive(true);
        }
        else {
            countText.gameObject.SetActive(false);
        }
        
    }

    public void OnPointerClick(PointerEventData eventData) { //아이템이 선택되면
        if (eventData.button == PointerEventData.InputButton.Left){
            playerControl.selectedToolId = item.id; //플레이어가 선택한 아이템 갱신
            clicked = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) //드래그 시작
    {
        parentAfterDrag = transform.parent; //drag한 곳에 슬롯이 없으면 다시 되돌아와야 하기 때문에 현재 slot을 저장
        currentParent = transform.parent; //drag한 곳에 이미 아이템이 존재하면 자리를 바꿔야하기 때문에 저장
        transform.SetParent(transform.root); //아이템을 root(canvas)의 자식으로 만든다.
        transform.SetAsLastSibling(); //canvas에서 가장 높은 레이어로 설정하기 위해
        image.raycastTarget = false; 
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Text>().enabled = false; //드래그 중에는 아이템의 개수 보여지지 않기
        //마우스 커서의 위치가 정확하지 않아서 그만큼 보정
        float x = toolbar.TransformPoint(Input.mousePosition).x - 3.43f;
        float y = toolbar.TransformPoint(Input.mousePosition).y - 0.247f;
        transform.position = new Vector3(x, y, 0f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Text>().enabled = true; //드래그를 놓으면 아이템 개수 다시 보이기
        transform.SetParent(parentAfterDrag); //다시 부모 객체 설정
        image.raycastTarget = true;

    }
}
