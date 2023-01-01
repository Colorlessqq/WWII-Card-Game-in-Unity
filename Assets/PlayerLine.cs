using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLine : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI ammunation;
    private int maxCard = 3;
    StateManager stateManager;
    public void OnDrop(PointerEventData eventData)
    {
        if (!stateManager.isPlayerTurn) { return; }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        Interactive inter = eventData.pointerDrag.gameObject.GetComponent<Interactive>();

        //Eğer kartın safı değişmedi ise sonlandır
        if (d.lastParent == transform)
        {
            d.original_parent = this.transform;
            return;
        }
        if (!d.gameObject.CompareTag("Player")) { return; }
        // Eğer tur senin değilse sonlandır

        // Eğer safta yetirnce kart varsa yeni kart atılmasını engeller
        if (this.gameObject.transform.childCount >= maxCard) { print("maxium card"); return; }

        //Kartın ücretinin oynamasına engel olup olmadığını kontrol eder
        if (inter.isPlayed == false)
        {
            if (!stateManager.PlayCard(inter)) { return; }
        }
        // Kartın cephane ücreti ödenir
        else 
        {
            if (!stateManager.MoveCard(inter)) { return; } 
        }
       

        //Her şey doğru ise kartın yerini değiştirir
        inter.CardLine = 0;
        d.original_parent = this.transform;


    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("OnPointerEnter");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //print(" OnPointerExit");
    }
    private void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }
}
