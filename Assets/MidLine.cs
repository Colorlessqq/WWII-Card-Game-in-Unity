using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MidLine : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    public Transform playerLine1;
    public Transform playerLine2;
    int maxCard = 5;
    StateManager stateManager;
    string Ownerside;
    public void OnDrop(PointerEventData eventData)
    {
        if (this.gameObject.transform.childCount == 0) { Ownerside = "None"; }
        else
        {
            Ownerside = gameObject.transform.GetChild(0).tag;
        }
        
      
        if (this.gameObject.transform.childCount >= maxCard) { print("maxium card"); return; }

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d.gameObject.tag != Ownerside && Ownerside != "None")
        {
            stateManager.Announce("Düşman varkan ilerleyemezsiniz");
            return;
        }
        if (d.lastParent == transform)
        {
            d.original_parent = this.transform;
            return;
        }

        //if  (d.gameObject.CompareTag("Player") && (d.original_parent.transform  != playerLine1.transform && d.original_parent.transform != playerLine2.transform))
        //{
        //  return; 
        //}

        Interactive inter = eventData.pointerDrag.gameObject.GetComponent<Interactive>();
        if (!stateManager.MoveCard(inter)) { return; }
        inter.CardLine = 1;
        d.original_parent = this.transform;
    }
    public bool MoveCardToMidLine(Interactive inter)
    {
        if (inter.gameObject.tag != Ownerside && Ownerside != "None")
        {
            print("ben aga");
            return false;
        }
        if (this.gameObject.transform.childCount >= maxCard) { print("maxium card"); print("ben aga aga"); return false; }
        inter.CardLine = 1;
        inter.transform.SetParent(this.transform);
        return true;
    }


    private void Start()
    {
        Ownerside= "None";
        stateManager = FindObjectOfType<StateManager>();
    }
}
