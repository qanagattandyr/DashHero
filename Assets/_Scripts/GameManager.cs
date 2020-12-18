using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Inspector")]
    public List<Image> hearts;
    public List<Image> deadHearts;
    public GameObject retryBtn;
    [Header("Dynamic")]
    public bool gameOver = false;
    public bool paused = false;
    public int lifes = 3;
    // Start is called before the first frame update
    public static GameManager S;
    private void Awake()
    {
        Time.timeScale = 1;
        S = this;
        gameOver = false;
    }

    public void retry()
    {
        SceneManager.LoadScene("Main");
    }
    public void lifeDecrease()
    {

        lifes--;
        hearts[lifes].enabled = false;
        deadHearts[lifes].enabled = true;

        if(lifes <= 0)
        {
            GameOver();
        }

    }
    public void GameOver()
    {
        gameOver = true;
        print("Dead");
        retryBtn.SetActive(true);
        Time.timeScale = 0;
    }    
    public void Pause()
    {
        if(!paused)
        {
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
