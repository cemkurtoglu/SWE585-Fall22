using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody playerRigidbody;
    public Animator animator;
    private PlayerAnimation playerAnimation;
    private SpawnManager spawnManager;
    private float contactThreshold = 30;          // Acceptable difference in degrees for Trampoline
    private Vector3 validDirection = Vector3.up;  // What you consider to be upwards for Trampolune

    [Header("Set the Respawn Flashing Behavior")]
    [Space(10)]

    public GameObject[] flameBoyParts;
    public float flashCounter;
    private float flashTime;
    private bool isHit;

    


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        isGrounded = true;

        playerAnimation = PlayerAnimation.GetInstance();
        playerAnimation.OnStart(transform);

        spawnManager =  GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        UIController.instance.UpdateHealthDisplay(spawnManager.playerLife);

        isHit = false;
        flashTime = flashCounter;




    }

    private void OnCollisionEnter(Collision other) {

        SetCollisionBasedBehaviour(other,true); 

    }

    private void OnCollisionExit(Collision other) {

        SetCollisionBasedBehaviour(other,false);
    }


    // Update is called once per frame
    void Update()
    {

        SetDirection();
        SetJumpBehavior();
        SetPlayerMovementAnimation();
        FlashWhenHit();

        
   
    }

    private void SetDirection(){

       if (Input.GetKey(KeyCode.UpArrow))
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
            transform.Translate(-transform.forward * moveSpeed * Time.deltaTime, Space.World);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Rotate the sprite about the Y axis in the positive direction
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotationSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Rotate the sprite about the Y axis in the negative direction
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rotationSpeed, Space.World);
        }
    }

    private void SetJumpBehavior(){

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            isGrounded = false;
            playerRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
    }

    private void SetCollisionBasedBehaviour(Collision other, bool isGrounded){

        string colliderTag = other.collider.tag;

        switch (colliderTag)
        {
            case "Surface":
                this.isGrounded = isGrounded;
                break;
            case "Water":
                // Physics.IgnoreCollision(other.collider,GetComponent<Collider>());
                Debug.Log("Player is dead");   
                double health = spawnManager.PlayerHealthManager();
                Debug.Log("Player health is: " + health);
                break; 

            case "Mushroom":
                Debug.Log("Poisoned!");
                playerRigidbody.AddForce(new Vector3(0f,2f,-2f)* jumpForce, ForceMode.Impulse);
                spawnManager.setPlayerLife(spawnManager.playerLife - 0.5);
                other.gameObject.SetActive(false);
                isHit = true;
                
                break;
            case "Trampoline":
                for (int k=0; k < other.contacts.Length; k++) {
                    if (Vector3.Angle(other.contacts[k].normal, validDirection) <= contactThreshold){
                        // Collided with a surface facing mostly upwards
                        playerRigidbody.AddForce(new Vector3(0f,4f,0f) * jumpForce, ForceMode.Impulse);
                        break;
             }
         }
                break;
            case "Coin":
                FindObjectOfType<Coin>().DestroyCoin(other);                
                break;
            
        }

    }

    private void SetPlayerMovementAnimation(){

        animator.SetFloat(PlayerAnimation.ANIMATION_SPEED, playerAnimation.GetSpeed(transform));
        animator.SetBool(PlayerAnimation.ANIMATION_IS_GROUNDED,isGrounded);
        animator.SetFloat(PlayerAnimation.ANIMATION_Y_VELOCITY,playerAnimation.GetYVelocity(transform));
    

    }
    
    public SpawnManager GetSpawnManager(){
        return spawnManager;
    }

    public void FlashWhenHit(){

        if (isHit){


            flashCounter -= Time.deltaTime;

            if(flashCounter >= 0 ){

                foreach (var item in flameBoyParts){
                    item.SetActive(!item.activeSelf);
                }
                
            } else {
                isHit = false;
                foreach (var item in flameBoyParts){
                item.SetActive(true);
                flashCounter = flashTime;
                }

            }
            
        }


        


    }

    public void SetIsHit(bool isHit){
        this.isHit = isHit;
    }

}

