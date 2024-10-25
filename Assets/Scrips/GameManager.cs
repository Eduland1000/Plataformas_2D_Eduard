using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  
  public static GameManager instance;
   private int coins = 0;
   private int star = 0;

   private   bool pauseAnimation;

   [SerializeField] Text _cointext;
   private bool isPaused; 
   [SerializeField] GameObject _pauseCanvas;
   [SerializeField] Text _startext;
   [SerializeField] private Animator _pausePanelAnimator;
    [SerializeField] private Slider _healthBar;

    private float progresoDeCarga;
 
 void Update()
 {
    if(Input.GetKeyDown(KeyCode.L)) StartCoroutine(LoadAsync("Main Menu"));
 }
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

    _pausePanelAnimator = _pauseCanvas.GetComponentInChildren<Animator>();

   }

   public void Pause()
   {
    if(!isPaused && !pauseAnimation)
    {
        isPaused = true;
        Time.timeScale = 0;
        _pauseCanvas.SetActive(true);
    }
    else if(isPaused && !pauseAnimation)
    {
        pauseAnimation = true;

        StartCoroutine(ClosePauseAnimation());
    }
   }

   IEnumerator ClosePauseAnimation()
    {
        _pausePanelAnimator.SetBool("Close",true);

        yield return new WaitForSecondsRealtime(0.20f);

        Time.timeScale =1;

       
        _pauseCanvas.SetActive(false);
        isPaused=false;
        pauseAnimation = false;
    }

   public void AddCoin()
   {
    coins++;
    _cointext.text = coins.ToString();
    //coins += 1;
   }

    public void AddStar()
   {
    star++;
    _startext.text = star.ToString();
    //coins += 1;
   }
public void SetHealthBar(int maxHealth)
 {
    _healthBar.maxValue = maxHealth;
    _healthBar.value = maxHealth;
 } 
   public void UpdateHealthBar(int health)
   {
    _healthBar.value = health;
   }
   
   public void SceneLoader(string sceneName)
   {
      SceneManager.LoadScene(sceneName);
   }
    
IEnumerator LoadAsync (string scene) 
{
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

    while(!asyncLoad.isDone) 
    {
        if(asyncLoad.progress <= 0.9f)
        {
            progresoDeCarga = asyncLoad.progress;
            Debug.Log(progresoDeCarga);
        }
        yield return null;
    }
}
}


