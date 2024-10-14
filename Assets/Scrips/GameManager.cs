using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  
  public static GameManager instance;
   private int coins = 0;

   [SerializeField] Text _cointext;
   private bool isPaused; 
   [SerializeField] GameObject _pauseCanvas;
 
   void Awake()
   {
    if(instance != null && instance != this)
    {
        Destroy(gameObject);
    }
    else
    {
        instance = this;
    }
   }

   public void Pause()
   {
    if(!isPaused)
    {
        Time.timeScale = 0;
        isPaused=true;
        _pauseCanvas.SetActive(true);
    }
    else
    {
        Time.timeScale =1;
        isPaused=false;
        _pauseCanvas.SetActive(false);

    }
   }
   public void AddCoin()
   {
    coins++;
    _cointext.text = coins.ToString();
    //coins += 1;
   }

    public void AddStar()
   {
    coins++;
    _startext.text = Star.ToString();
    //coins += 1;
   }
}


