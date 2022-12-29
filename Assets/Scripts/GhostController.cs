using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    public float speed;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public List<Collider> avoidCollisionList;


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
    }

    private void reuseAliveEnemy(){

        if (transform.position.z <= endPosition.z){
            initRandomPosition();
        }

    }
    private void setAvoidableCollisionObjects(){

        ignoreCollectionOfObject("Tree");
        ignoreCollectionOfObject("BuzzBoy");


        


    }
    private void ignoreCollectionOfObject(string tag){
        
        foreach (var item in GameObject.FindGameObjectsWithTag(tag))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(),item.GetComponent<Collider>(),true);

        }

    }
    

}
