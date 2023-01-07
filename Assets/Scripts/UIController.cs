using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    
    public static UIController instance;
    public Slider healthSlider;
    public TMP_Text healthText, timeText, coinText, gameOverText, youWonText;
    private double playerHealth;
    private float levelTimer;
    private double coinCollected;
    public Image fadeScreen;
    private bool isFadingToBlack, isFadingFromBlack;
    public float fadeSpeed = 2f;

    private void Awake() {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<SpawnManager>().playerLife;
        healthSlider.maxValue = (float)playerHealth;
        healthSlider.value = healthSlider.maxValue;
        UpdateHealthDisplay(playerHealth);
        levelTimer = 0f;
        UpdateCoinDisplay(0,FindObjectOfType<Coin>().totalCoins);




        
    }

    // Update is called once per frame
    void Update()
    {

        levelTimer = levelTimer + Time.deltaTime;
        timeText.text = Mathf.RoundToInt(levelTimer).ToString(); 
        
    }

    public void UpdateHealthDisplay(double health){

        healthText.text = "Health: " + health + "/" + playerHealth;
        healthSlider.value = (float)health;

    }

    public void UpdateCoinDisplay(double coins, double size){
        
        coinText.text = coins + "/" + size;

    }

    public void DisplayGameOverMessage(){

        Debug.Log("Game Over Message");

        fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed *Time.deltaTime));
        gameOverText.color = new Color(gameOverText.color.r,gameOverText.color.g,gameOverText.color.b, Mathf.MoveTowards(gameOverText.color.a, 255f, fadeSpeed *Time.deltaTime));


    }

    public void DisplaySuccessMessage(){

    Debug.Log("You Won");

    fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed *Time.deltaTime));
    youWonText.color = new Color(youWonText.color.r,youWonText.color.g,youWonText.color.b, Mathf.MoveTowards(youWonText.color.a, 255f, fadeSpeed *Time.deltaTime));


    }

 
}
