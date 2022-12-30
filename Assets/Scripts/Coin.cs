using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public List<GameObject> coinList;
    private double coinListSize;

    private void Awake() {
        coinListSize = coinList.Count;
       FindObjectOfType<UIController>().UpdateCoinDisplay(0,coinList.Count);
    }

    public void DestroyCoin(Collision coin){

        Destroy(coin.gameObject);

        // double coinCollected = coinList.Count - coinListSize;

        FindObjectOfType<UIController>().UpdateCoinDisplay(coinList.Count, coinListSize);

    }
}
