using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    StateManager stateManager;
    [SerializeField] Canvas canvas;
    [SerializeField] Canvas canvas2;
    [SerializeField] Canvas canvas3;
    [SerializeField] Canvas canvas4;

    public void ShowCards()
    {
        canvas4.gameObject.SetActive(true);
        canvas3.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }
    public void HowToPlay()
    {
        canvas4.gameObject.SetActive(false);
        canvas3.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }
    public void startButton()
    {
        canvas4.gameObject.SetActive(false);
        canvas3.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
    }
    public void escButton()
    {
        canvas4.gameObject.SetActive(false);
        canvas3.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(true);
        canvas.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (stateManager.GameOver) { SceneManager.LoadScene("Menu"); }
            else { escButton(); }
        }
    }
    public void quit()
    {
        Application.Quit();
    }
    private void Start()
    {
        stateManager = FindObjectOfType<StateManager>();
    }
}