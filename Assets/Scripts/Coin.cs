using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin instance;

    public int totalCoins;
    private double coinCollected;

    private void Awake() {
        instance = this;


    }
    private void Start() {
        coinCollected = 0;

    }

    private void Update(){
        if(coinCollected >= totalCoins){
            UIController.instance.DisplaySuccessMessage();

        }
    }

    public void DestroyCoin(Collision coin){

        Destroy(coin.gameObject);
        coinCollected++;


        FindObjectOfType<UIController>().UpdateCoinDisplay(coinCollected,totalCoins);

    }


    
}
