using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin instance;

    public List<GameObject> coinList;
    private double coinCollected;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        coinCollected = 0;
    }

    public void DestroyCoin(Collision coin){

        Destroy(coin.gameObject);
        coinCollected++;


        FindObjectOfType<UIController>().UpdateCoinDisplay(coinCollected,coinList.Count);

    }
}
