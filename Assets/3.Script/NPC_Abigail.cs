using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPC_Abigail : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    private GameManager gameManager;
    private bool isInTrigger;

    [Header("NPC Potrait")]
    [SerializeField]private Sprite AbigailPortrait;
    [SerializeField]private Sprite AbigailPortrait_Happy;
    private string AbigailName;
    private string AbigailDialog;
 
    [Header("UI")]
    [SerializeField]private Transform DialogUI;
    
    [SerializeField]private Transform portrait;
    [SerializeField]private Text nameText;
    [SerializeField]private Text dialogText;

    

    private void Start() {
        isInTrigger = false;
        AbigailName = "애비게일";
        AbigailDialog = "반가워 !";
    }

    public void OnPointerClick(PointerEventData eventData) //애비게일 클릭하면
    {
        if (eventData.button == PointerEventData.InputButton.Left && isInTrigger) {
            DisplayNPCDialog(AbigailPortrait, AbigailDialog);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        if (collider.gameObject.CompareTag("Player") ) {
            gameManager.playerMouseButtonActive = false;
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")){
            Debug.Log("trigger exit");
            gameManager.playerMouseButtonActive = true;
            DialogUI.gameObject.SetActive(false);
            isInTrigger = false;
        }
        
    }

    //대화창 
    private void DisplayNPCDialog(Sprite portrait, string text) {
        DialogUI.gameObject.SetActive(true);
        DialogUI.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = portrait;
        nameText = GameObject.FindWithTag("DialogName").GetComponent<Text>();
        nameText.text = AbigailName;
        dialogText = GameObject.FindWithTag("DialogText").GetComponent<Text>();
        dialogText.text = text;
    }

    public void OnDrop(PointerEventData eventData) {
        if (!eventData.pointerDrag.GetComponent<SlotItem>().item.isTool && isInTrigger) {
            Destroy(eventData.pointerDrag); //아이템 오브젝트 파괴하고
            DisplayNPCDialog(AbigailPortrait_Happy, "고마워 !");
        }
    }
 }
