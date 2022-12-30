using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    
    public static UIController instance;
    public Slider healthSlider;
    public TMP_Text healthText, timeText, coinText;
    private double playerHealth;
    private float levelTimer;
    private double coinCollected;

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
        UpdateCoinDisplay(0,FindObjectOfType<Coin>().coinList.Count);




        
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

 
}
