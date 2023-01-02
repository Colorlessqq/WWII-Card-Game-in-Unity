using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interactive : MonoBehaviour, IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] public TextMeshProUGUI cost;
    [SerializeField] TextMeshProUGUI ammunation;
    [SerializeField] public Image image;
    [SerializeField] public int range;
    [SerializeField] Canvas naziwin;
    [SerializeField] Canvas sovyetwin;
    public int CardLine = -10;
    public bool isPlayed = false;
    StateManager stateManager;
    public bool isBase = false;
    public void OnDrop(PointerEventData eventData)
    {
        // Eğer tur senin değilse sonlandır
        if (!stateManager.isPlayerTurn) { return; }
        Interactive dropeed = eventData.pointerDrag.gameObject.GetComponent<Interactive>();
        CardAttacked(dropeed);
    }
    public void CardAttacked(Interactive dropeed)
    {
        
        if (dropeed.tag != this.tag && dropeed.isPlayed && isPlayed)
        {
            if (stateManager.Attack(dropeed, this.GetComponent<Interactive>()))
            {
                TextMeshProUGUI damage = dropeed.attack;
                health.text = (int.Parse(health.text) - int.Parse(damage.text)).ToString();
                if (int.Parse(health.text) < 1)
                {
                    Destroy(this.gameObject);
                    if (isBase) 
                    {
                        if (CompareTag("Player")) 
                        {
                            stateManager.GameOver = true;
                            naziwin.gameObject.SetActive(false);
                            sovyetwin.gameObject.SetActive(true);

                        }
                        else
                        {
                            naziwin.gameObject.SetActive(true);
                            sovyetwin.gameObject.SetActive(false);
                            stateManager.GameOver = true;
                        }
                    }
                }
            }
        }
    }

    [System.Obsolete]
    public void OnPointerEnter(PointerEventData eventData)
    {
        try
        {
            Image image1 = gameObject.GetComponentInChildren<Image>();
            if (eventData.pointerDrag.gameObject.tag == this.tag && isPlayed)
            {

                image1.color = Color.gray;
            }
            else if (eventData.pointerDrag.gameObject.tag != this.tag && isPlayed)
            {
                image1.color = Color.red;
            }
            else
            {
                image1.color = Color.gray;
            }
        }
        catch (NullReferenceException)
        {
        }
        
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Image image1 = gameObject.GetComponentInChildren<Image>();
        image1.color = Color.white;
    }

    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }

    //private void Update()
    //{
    //    if (weNeedAnimation)
    //    {

    //        Vector3.SmoothDamp(this.transform.position, enemyLine.transform.position, ref velecotiy, 2);
    //    }
    //}


}
