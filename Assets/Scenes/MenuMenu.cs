using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMenu : MonoBehaviour
{

    [SerializeField] Canvas canvas2;
    [SerializeField] Canvas canvas3;
    [SerializeField] Canvas canvas4;
    public void startButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ShowCards()
    {
        canvas4.gameObject.SetActive(true);
        canvas3.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(false);
      
    }
    public void HowToPlay()
    {
        print("dsfsdf");
        canvas4.gameObject.SetActive(false);
        canvas3.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);

    }

   
    public void escButton()
    {
        canvas4.gameObject.SetActive(false);
        canvas3.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(true);

    }
    public void quit()
    {
        Application.Quit();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escButton();
        }
    }
}
