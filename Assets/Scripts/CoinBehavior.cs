using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public static CoinBehavior instance;

private void OnEnable() {
    instance = this;
    ignoreCollectionOfObjects("BuzzBoy");
    ignoreCollectionOfObjects("Ghost");
    
}

private void ignoreCollectionOfObjects(string tag){
    
    foreach (var item in GameObject.FindGameObjectsWithTag(tag))
    {
        Physics.IgnoreCollision(GetComponent<Collider>(),item.GetComponent<Collider>());

    }

}
}
