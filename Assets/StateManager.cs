using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public bool isPlayerTurn = true;
    Vector3 velecotiy = Vector3.zero;
    public TextMeshProUGUI playerGold;
    public TextMeshProUGUI enemyGold;
    public GameObject mesage;
    [SerializeField] EnemyLine enemyLine1;
    [SerializeField] EnemyLine enemyLine2;
    [SerializeField] MidLine midLine;
    BasicAI basicAI;
    public bool weNeedAnimation = false;
    public void EndPlayerTurn()
    {
        if (isPlayerTurn) { isPlayerTurn = false; }
        else { return; }
        playerGold.text = (int.Parse(playerGold.text) + 4 ).ToString();
        basicAI.PlayAI();
        EndEnemyTurn();

    }
    public void EndEnemyTurn()
    {
        if (isPlayerTurn) { return; }
        else { isPlayerTurn = true; }
        enemyGold.text = (int.Parse(enemyGold.text) + 4).ToString();
    }

    public bool isAttack(Interactive attacker, Interactive attacked)
    {
        if (attacker.range + attacker.CardLine + attacked.CardLine <= 1) {return false; }
        if (int.Parse(enemyGold.text) < int.Parse(attacker.cost.text))
        {
            return false;
        }
        return true;
    }

    public void EnemyAttack(Interactive attacker, Interactive attacked)
    {

        attacked.CardAttacked(attacker);
        enemyGold.text = (int.Parse(enemyGold.text) - int.Parse(attacker.cost.text)).ToString();
    }


    public bool Attack(Interactive attacker,Interactive attacked)
    {
        if (attacker.range + attacker.CardLine + attacked.CardLine <= 1) { print("menzil yetersiz"); return false; }
        if (attacker.CompareTag("Enemy")) { return true; }
        if (int.Parse(playerGold.text) < int.Parse(attacker.cost.text)) 
        {
            Announce("Saldırmak için yeterli cephane");
            print("cephane yetersiz");
            return false;
        }
        if (attacker.CompareTag("Player"))
        {
            playerGold.text = (int.Parse(playerGold.text) - int.Parse(attacker.cost.text)).ToString();
            print("playerGold");
        }
        else
        {
            enemyGold.text = (int.Parse(enemyGold.text) - int.Parse(attacker.cost.text)).ToString();
            print("enemyGold");
        }
        return true;
    }

    
    public bool PlayCard(Interactive playedCard)
    {
        if (isPlayerTurn && !playedCard.isPlayed)
        {
            //Kartın ücretinin oynamasına engel olup olmadığını kontrol eder

            {
                if (int.Parse(playerGold.text) < int.Parse(playedCard.cost.text))
                {
                    Announce("Hareket etmek için yeterli cephane");
                    return false;
                }
                playedCard.isPlayed= true;
                playerGold.text = (int.Parse(playerGold.text) - int.Parse(playedCard.cost.text)).ToString();
                return true;
            }
        }
        return false;
    }


    public bool isPlayEnemyCard(Interactive playedCard)
    {
        if (!isPlayerTurn && !playedCard.isPlayed)
        {
            //Kartın ücretinin oynamasına engel olup olmadığını kontrol eder

            {
                if (int.Parse(enemyGold.text) < int.Parse(playedCard.cost.text))
                {
                    return false;
                }
                return true;
            }
        }
        return false;
    }
    public bool PlayEnemyCard(Interactive playedCard)
    {
        if (!isPlayerTurn && !playedCard.isPlayed)
        {
            //Kartın ücretinin oynamasına engel olup olmadığını kontrol eder

            {
                
                if (int.Parse(enemyGold.text) < int.Parse(playedCard.cost.text))
                {
                    return false;
                }
                neden:
                int rnd = Random.Range(0, 2);

                if (rnd == 0)
                {
                    if (enemyLine1.transform.childCount >= enemyLine1.maxCard) { goto neden; }
                    enemyLine1.DropCardEnemyLine(playedCard);
                }
                else
                {
                    if (enemyLine1.transform.childCount >= enemyLine1.maxCard) { goto neden; }
                    enemyLine2.DropCardEnemyLine(playedCard);
                }
                enemyGold.text = (int.Parse(enemyGold.text) - int.Parse(playedCard.cost.text)).ToString();
                return true;
            }
        }
        return false;
    }

    //public bool PlayEnemyCard(Interactive playedCard)
    //{
    //    if (!isPlayerTurn && !playedCard.isPlayed)
    //    {
    //        //Kartın ücretinin oynamasına engel olup olmadığını kontrol eder

    //        {
    //            if (int.Parse(enemyGold.text) < int.Parse(playedCard.cost.text))
    //            {
    //                //TODO : Hareket etmek için yeterli cephane yok uyarısı
    //                return false;
    //            }
    //            print(playedCard.name + "is playable");
    //            playedCard.isPlayed = true;
    //            enemyGold.text = (int.Parse(enemyGold.text) - int.Parse(playedCard.cost.text)).ToString();
    //            return true;
    //        }
    //    }
    //    return false;
    //}





    public void MoveEnemyCard(Interactive movedCard)
    {
        print(movedCard.name);
        if (int.Parse(enemyGold.text) < int.Parse(movedCard.cost.text)) { return ; }
        if (midLine.MoveCardToMidLine(movedCard))
        {
            enemyGold.text = (int.Parse(enemyGold.text) - int.Parse(movedCard.cost.text)).ToString();
        }

    }
    public bool MoveCard(Interactive movedCard)
    {
        if (movedCard.tag == "Enemy") { return true; }
        if (!movedCard.isPlayed) { return false; }
        if (int.Parse(playerGold.text) < int.Parse(movedCard.cost.text)) { return false; }
        playerGold.text = (int.Parse(playerGold.text) - int.Parse(movedCard.cost.text)).ToString();
        return true;

    }

    private void Start()
    {
        basicAI = FindObjectOfType<BasicAI>();

        StartCoroutine(TextPopout());

    }
    public void Announce(string str)
    {
        StartCoroutine(TextPopout(str));
    }
    public IEnumerator TextPopout(string text = null)
    {
        if (text != null) { mesage.GetComponent<TextMeshProUGUI>().text = text; }
        mesage.SetActive(true);
        
        yield return new WaitForSeconds(2.5f);

        mesage.SetActive(false);
    }
}
