using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    StateManager stateManager;
    public void PlayAI()
    {
        //
        FindAndAttack();
        //
        PlayCard();
        //
        TryToMove();
        
    }

    public void PlayCard()
    {
        Interactive[] inter =  FindLegalMove();
        if (inter.Length == 0)
        {
            print("No valid moves");
            return;
        }
        int index = UnityEngine.Random.Range(0, inter.Length);
        stateManager.PlayEnemyCard(inter[index]);
    }

    public Interactive[] FindLegalMove()
    {

        List<Interactive> playableObjects = new List<Interactive>();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in gameObjects)
        {
            Interactive inter =  gameObject.GetComponent<Interactive>();
            if (stateManager.isPlayEnemyCard(inter)) {playableObjects.Add(inter); }

        }
        return playableObjects.ToArray();
    }
    private void FindAndAttack()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        List<Interactive> playableObjects = new List<Interactive>();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject enemy in enemyObjects)
        {
            Interactive enemyInter = enemy.GetComponent<Interactive>();
            // Eğer bu kart oynanmamışsa atla

            if (!enemyInter.isPlayed) { continue; }
            foreach (GameObject player in playerObjects)
            {
                Interactive playerinter = player.GetComponent<Interactive>();
                if (!playerinter.isPlayed) { continue; }
                if (stateManager.isAttack(enemyInter, playerinter)) {
                    print(enemyInter.name+ " şuna " + playerinter.name + " ateş etti");
                    stateManager.EnemyAttack(enemyInter, playerinter);
                    break;
                }


            }

        }
    }
    private void TryToMove()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjects)
        {
            
            Interactive enemyInter = enemy.GetComponent<Interactive>();
            //Eğer kart oynanmamışsa atla
            if (!enemyInter.isPlayed) { continue; }
            stateManager.MoveEnemyCard(enemyInter);
        }
    }
    void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }


}
