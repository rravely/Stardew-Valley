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
    public Transform canvas;
    [HideInInspector] public bool clicked = false;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform currentParent;
    [HideInInspector] public int count = 1;

    void Start()
    {
        canvas = GameObject.FindWithTag("toolbar").transform;
    }
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.icon;
        RefreshCount();
    }


    public void RefreshCount(){
        countText.text = count.ToString();
        if (count > 1){
            countText.gameObject.SetActive(true);
        }
        else {
            countText.gameObject.SetActive(false);
        }
        
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left){
            Debug.Log("Mouse Click Button : Left");
            clicked = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        currentParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Text>().enabled = false;

        Debug.Log(canvas.TransformPoint(Input.mousePosition));
        float x = canvas.TransformPoint(Input.mousePosition).x - 3.43f;
        float y = canvas.TransformPoint(Input.mousePosition).y - 0.247f;
        transform.position = new Vector3(x, y, 0f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Text>().enabled = true;
        //Debug.Log(parentAfterDrag.IsEmpty);
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;

    }
}
