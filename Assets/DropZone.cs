using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{


    public GameObject firstParent;
    int maxCard = 3;
    public void OnDrop(PointerEventData eventData)
    {
        print(eventData.pointerDrag.name + "OnDrag Zone: " + gameObject.name);

        if (this.gameObject.transform.childCount >= maxCard ) { print("maxium card"); return; }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
