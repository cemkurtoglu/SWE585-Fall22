using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;
    public float turnSpeed;
    public Transform[] patrolPoints;
    private int currentPatrolPoints;
    public Rigidbody enemyRigidbody;
    private float yStore;
    private PlayerController player;
    public Vector3 moveDirection, lookTarget;
    public enum EnemyState {idle, patroling, chasing, returning};
    public EnemyState currentState;
    [Header("Set the amount of time and chance for staying idle")]
    [Space(10)]
    public float waitTime;
    public float waitChance;
    private float waitCounter;
    [Header("Set the distances to start and stop chasing.")]
    [Space(10)]
    public float chaseDistance;
    public float chaseSpeed;
    public float loseDistance;
    [Tooltip("Wait X amount of seconds to return")]
    public float returnCounter = 3f;



    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentState = EnemyState.idle;
        waitCounter = waitTime;
        
    }

    // Update is called once per frame
    void Update()
    {
     SetEnemyState();   

     if(player.transform.position.y > 100){
        player.transform.position = new Vector3(player.transform.position.x, 80, player.transform.position.z);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
     }

        
    }
    private void OnCollisionEnter(Collision other) {
        
        string player = other.collider.tag;

        if(player == "Player"){
            

                PlayerController playerController = FindObjectOfType<PlayerController>();
                other.rigidbody.AddForce(other.rigidbody.velocity *100, ForceMode.Impulse);
                playerController.GetSpawnManager().playerLife = playerController.GetSpawnManager().playerLife - 0.5;
                UIController.instance.UpdateHealthDisplay(playerController.GetSpawnManager().playerLife);
                Debug.Log("Player Life: " + playerController.GetSpawnManager().playerLife);
                currentState = EnemyState.idle;
            
            

        }
                
               
           
    }


    private void EnemyPatroling(){

        yStore = enemyRigidbody.velocity.y;
        moveDirection = patrolPoints[currentPatrolPoints].position - transform.position;
        moveDirection.y = 0f;
        moveDirection.Normalize();
        enemyRigidbody.velocity = moveDirection * moveSpeed;
        enemyRigidbody.velocity = new Vector3(enemyRigidbody.velocity.x, yStore, enemyRigidbody.velocity.z);

        if(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoints].position)<=1f){
            NextPatrolPoint();
        } else {
            lookTarget = patrolPoints[currentPatrolPoints].position;
        }
        lookTarget.y = transform.position.y;
        // transform.LookAt(lookTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), turnSpeed * Time.deltaTime);

    }

    private void SetEnemyState(){

        isPlayerCloseToEnemy();


 
        switch(currentState){
            case EnemyState.idle:
                    lookTarget = player.transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), turnSpeed * Time.deltaTime);
                    waitCounter -= Time.deltaTime;
                    if(waitCounter <=0){
                        currentState = EnemyState.patroling;
                        NextPatrolPoint();
                    }
            break;

            case EnemyState.patroling:
            EnemyPatroling();

            break;
            case EnemyState.chasing:
            EnemyChasing();

            break;
            case EnemyState.returning:
            returnCounter -= Time.deltaTime;
            if (returnCounter <= 0f)
            {
                currentState = EnemyState.patroling;
                returnCounter = 3f;
                
            }

            break;

        }
        

    }
    private void EnemyChasing(){

        lookTarget = player.transform.position;
        yStore = enemyRigidbody.velocity.y;
        moveDirection = player.transform.position - transform.position;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        enemyRigidbody.velocity = moveDirection * chaseSpeed;
        enemyRigidbody.velocity = new Vector3(enemyRigidbody.velocity.x, yStore, enemyRigidbody.velocity.z);
        lookTarget.y = transform.position.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), turnSpeed * Time.deltaTime);



    }

    private void isPlayerCloseToEnemy(){
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance < chaseDistance){
            currentState = EnemyState.chasing;
        } else if (distance > loseDistance && currentState == EnemyState.chasing)
            currentState = EnemyState.returning;

    }

    private void NextPatrolPoint(){

        if(Random.Range(0f,100f)< waitChance){
            waitCounter = waitTime;
            currentState = EnemyState.idle;
        } else {

            if(patrolPoints.Length-1 > currentPatrolPoints){
                currentPatrolPoints++;

            } else {
                currentPatrolPoints = 0;

            }
        }
    }
}
