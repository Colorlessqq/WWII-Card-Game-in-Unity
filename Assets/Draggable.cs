using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
{
    // Start is called before the first frame update
    public Transform lastParent;
    public Transform original_parent = null;
    StateManager stateManager;

    //GameObject placeHolder = null;
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Eğer tur senin değilse sonlandır
        if (!stateManager.isPlayerTurn && eventData.pointerDrag.CompareTag("Player")){ return; }

        lastParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        original_parent = this.transform.parent;

        //placeHolder = new GameObject();
        //placeHolder.transform.parent.SetParent(this.transform.parent);


        //LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        //le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        //le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        //le.flexibleWidth = 0;
        //le.flexibleHeight = 0;
        //int index = this.gameObject.transform.GetSiblingIndex();
        //placeHolder.transform.SetSiblingIndex(index);

        this.transform.SetParent(this.transform.parent.parent);

    }

    public void OnDrag(PointerEventData eventData)
    {
        // Eğer tur senin değilse sonlandır
        if (!stateManager.isPlayerTurn && eventData.pointerDrag.CompareTag("Player")) { return; }


        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Eğer tur senin değilse sonlandır
        if (!stateManager.isPlayerTurn && eventData.pointerDrag.CompareTag("Player")) { return; }


        this.transform.SetParent(original_parent);
        
        //if (original_parent == placeHolder.transform.parent)
        //{
        //    int index = this.placeHolder.transform.GetSiblingIndex();
        //    this.transform.SetSiblingIndex(index);

        //}
        //Destroy(placeHolder);
 
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
