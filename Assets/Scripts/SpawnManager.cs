using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public bool isPlayerAlive;
    public double playerLife;
    public static SpawnManager instance;

    [HideInInspector]
    public bool isRespawning;

    public Vector3 respawnPoint;

    private PlayerController playerController;


    private void Awake() {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        isPlayerAlive = true;
        playerController = FindObjectOfType<PlayerController>();
        respawnPoint = playerController.transform.localPosition;
        // StartCoroutine(spawnRoutine(4.0f));

        
    }

    // Update is called once per frame
    void Update()
    {
        checkLife();
    }
    public void checkLife(){
        if (playerLife <= 0){
            setIsPlayerAlive(false);
            Debug.Log("Game over"); 
            playerController.gameObject.SetActive(false);
        }
    }

    public double PlayerHealthManager(){
        
        if (playerLife > 0){
            StartCoroutine(spawnRoutine(4.0f));

        } else {

            setIsPlayerAlive(false);
            Debug.Log("Game over"); 
            playerController.gameObject.SetActive(false);
  

        }
        return playerLife;
    }

    IEnumerator spawnRoutine(float seconds){
    
        if(isPlayerAlive){
            // GameObject newEnemy = Instantiate(enemyPrefab,new Vector3(0.0f,6.0f,0.0f) ,Quaternion.identity);
            // newEnemy.transform.parent = enemyContainer.transform;
            playerController.gameObject.SetActive(false);
            playerLife = playerLife - 3;
            isRespawning = true;
            UIController.instance.UpdateHealthDisplay(playerLife);
            yield return new WaitForSeconds(seconds);
            playerController.transform.position = respawnPoint + Vector3.up;
            playerController.gameObject.SetActive(true);
            isRespawning = false;
        }

    }

    public void setIsPlayerAlive(bool isPlayerAlive){
        this.isPlayerAlive = isPlayerAlive;
        isRespawning = isPlayerAlive;
    }

    public bool getIsRespawning(){
        return isRespawning;
    }

    public void setPlayerLife(double playerLife){
        this.playerLife = playerLife;
        UIController.instance.UpdateHealthDisplay(this.playerLife);
        if (playerLife <= 0){
            PlayerHealthManager();
        }
    }


}
