using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    public float speed;
    public Vector3 startPosition;
    public Vector3 endPosition;


    // Start is called before the first frame update
    private void Awake() {
        setAvoidableCollisionObjects();
    }
    void Start()
    {
        initRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f,0f,1f) * speed * Time.deltaTime);
        reuseAliveEnemy();
        
    }

    private void initRandomPosition(){
        
        float objectWidth = transform.GetComponent<CapsuleCollider>().bounds.size.x;
        float x = Random.Range(-30 - objectWidth, 30 + objectWidth);
        float y = startPosition.y;
        // float z = Random.Range(-15,70);
        float z = startPosition.z;
        transform.position = new Vector3(x,y,z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,180f,0f),Time.deltaTime);
    }

    private void reuseAliveEnemy(){

        if (transform.position.z <= endPosition.z){
            initRandomPosition();
        }

    }
    private void setAvoidableCollisionObjects(){

        ignoreCollectionOfObjects("Tree");
        ignoreCollectionOfObjects("BuzzBoy");
        ignoreCollectionOfObjects("Trampoline");
        ignoreCollectionOfObjects("Mushroom");
        ignoreCollectionOfObjects("Coin");




        


    }
    private void ignoreCollectionOfObjects(string tag){
        
        foreach (var item in GameObject.FindGameObjectsWithTag(tag))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(),item.GetComponent<Collider>(),true);

        }

    }

    private void OnCollisionEnter(Collision other) {
        
        if(other.collider.tag == "Player"){
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.SetIsHit(true);
            playerController.GetComponent<Rigidbody>().AddForce(new Vector3(0f,2f,0f)* 2.0f, ForceMode.Impulse);
            playerController.GetSpawnManager().playerLife = playerController.GetSpawnManager().playerLife - 1;
            UIController.instance.UpdateHealthDisplay(playerController.GetSpawnManager().playerLife);
            Debug.Log("Player Life: " + playerController.GetSpawnManager().playerLife);
            gameObject.SetActive(false);
            playerController.SetIsHit(true);

        }
    }
    

}
