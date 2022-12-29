using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

{
    public Transform player;
    public Transform surface;
    public Vector3 offset;

    public Vector3 cameraOffset;
    public float smoothSpeed = 0.125f;

    private SpawnManager spawnManager;

    private float playerLife;

        // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0f,3f,-5f);
        spawnManager = SpawnManager.instance;

        // Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {
        //get the players position and add it with offset, then store it to transform.position aka the cameras position
        // transform.position = player.position + offset;

        if (!spawnManager.getIsRespawning()){
            Vector3 desiredPosition = player.position + player.rotation * offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = player.rotation * Quaternion.Euler(new Vector3(12f,0f,0f));
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;
        
        } else {

            Vector3 desiredPosition = surface.position + surface.rotation * cameraOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = surface.rotation * Quaternion.Euler(cameraOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;

        }



       


    }
}
