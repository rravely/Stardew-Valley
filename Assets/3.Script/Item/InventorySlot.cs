using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;

    public void Selected() //선택되면
    {
        Transform selectedSquare = transform.GetChild(0).GetChild(1); //아이템의 자식 객체인 selectedSquare 가져오기
        SlotItem slotItme = transform.GetChild(0).gameObject.GetComponent<SlotItem>();
        selectedSquare.gameObject.SetActive(true);
    }
    public void Deselected() //선택되지 않으면
    {
        //slotItem.GetChild.clicked = false;
        if (transform.childCount > 0 ) {
            Transform selectedSquare = transform.GetChild(0).GetChild(1);
            SlotItem slotItme = transform.GetChild(0).gameObject.GetComponent<SlotItem>();
            slotItme.clicked = true;
            selectedSquare.gameObject.SetActive(false);
        }
        
    }

    

    public void OnDrop(PointerEventData eventData) //마우스를 놓았을 때
    {
        if (transform.childCount == 0) //자식 객체가 없으면(아이템이 없으면)
        {
            SlotItem slotItem = eventData.pointerDrag.GetComponent<SlotItem>(); 
            slotItem.parentAfterDrag = transform; //부모 객체를 이 slot으로 지정
        }
        else //이미 아이템이 존재하면 swap
        {
            SlotItem slotItem = eventData.pointerDrag.GetComponent<SlotItem>();
            transform.GetChild(0).SetParent(slotItem.currentParent); //드래그한 아이템의 슬롯에 현재 슬롯에 있는 아이템을 가져다 놓고
            slotItem.parentAfterDrag = transform; //이 슬롯에 드래그한 아이템 놓기
        }
    }
}
