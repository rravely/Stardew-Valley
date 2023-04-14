using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;

    public void Selected()
    {
        Transform selectedSquare = transform.GetChild(0).GetChild(1); //아이템의 자식 객체인 selectedSquare 가져오기
        Debug.Log(selectedSquare.gameObject);
        SlotItem slotItme = transform.GetChild(0).gameObject.GetComponent<SlotItem>();
        selectedSquare.gameObject.SetActive(true);
    }
    public void Deselected()
    {
        //slotItem.GetChild.clicked = false;
        Transform selectedSquare = transform.GetChild(0).GetChild(1);
        SlotItem slotItme = transform.GetChild(0).gameObject.GetComponent<SlotItem>();
        slotItme.clicked = true;
        selectedSquare.gameObject.SetActive(false);
    }

    

    public void OnDrop(PointerEventData eventData) 
    {
        Debug.Log(transform.childCount);
        if (transform.childCount == 0) 
        {
            SlotItem slotItem = eventData.pointerDrag.GetComponent<SlotItem>();
            slotItem.parentAfterDrag = transform;
        }
        else 
        {
            SlotItem slotItem = eventData.pointerDrag.GetComponent<SlotItem>();
            transform.GetChild(0).SetParent(slotItem.currentParent);
            Debug.Log(slotItem.currentParent);
            slotItem.parentAfterDrag = transform;
        }
    }
}
