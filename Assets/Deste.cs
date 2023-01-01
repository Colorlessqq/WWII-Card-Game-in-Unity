using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Deste : MonoBehaviour
{
    [SerializeField] bool isEnemyDeck;

    [SerializeField] List<GameObject> myDeck;

    [SerializeField] private GameObject spawnLine;
    StateManager stateManager;

    public void ReturnCardToHand()
    {
        // Eğer tur senin değilse sonlandır
        if (!stateManager.isPlayerTurn) { return; }
        if (myDeck.Count <= 0) 
        {
            stateManager.Announce("Destenizde kart kalmadı"); 
            return;
        }
        int x = Random.Range(0, myDeck.Count);
        GameObject newCard = Instantiate(myDeck[x]);
        myDeck.RemoveAt(x);
        newCard.tag = "Player";
        newCard.transform.SetParent(spawnLine.transform);

    }
    public void ReturnCardToEnemeyHand()
    {
        if (myDeck.Count <= 0) { return; }
        int x = Random.Range(0, myDeck.Count);
        GameObject newCard = Instantiate(myDeck[x]);
        myDeck.RemoveAt(x);
        newCard.transform.SetParent(spawnLine.transform);
        newCard.tag = "Enemy";
        

    }
    private void Start()
    {

        stateManager = FindObjectOfType<StateManager>();
        for (int i = 0; i < 4; i++)
        {
            int x = Random.Range(0, myDeck.Count);
            GameObject newCard = Instantiate(myDeck[x]);
            myDeck.RemoveAt(x);
            if (isEnemyDeck) { newCard.tag = "Enemy"; }
            else
            {
                newCard.tag = "Player";
            }
            newCard.transform.SetParent(spawnLine.transform);
        }

    }

}
