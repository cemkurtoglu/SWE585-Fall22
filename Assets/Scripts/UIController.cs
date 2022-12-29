using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    
    public static UIController instance;
    public Slider healthSlider;
    public TMP_Text healthText, timeText;
    private double playerHealth;
    private float levelTimer;

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

 
}
