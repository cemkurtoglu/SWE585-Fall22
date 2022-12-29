using UnityEngine;

public sealed class PlayerAnimation {

    public static readonly string ANIMATION_SPEED = "speed";
    public static readonly string ANIMATION_Y_VELOCITY = "yVel";
    public static readonly string ANIMATION_IS_GROUNDED = "isGrounded";
    public static readonly string ANIMATION_END_LEVEL = "endLevel";


    private Vector3 ballSpeed;
    private Vector3 lastPosition;


    private float speedInTermsOfYDirection;
    private float lastYPosition;


    private Animator animator;

    private static PlayerAnimation animation;

    public static PlayerAnimation GetInstance(){
            
            if (animation == null){

                animation = new PlayerAnimation();
            }
            return animation;

    }

    public void OnStart(Transform transform){
        
        lastPosition = transform.position;
        lastYPosition = transform.localPosition.y;
        
    }

    public float GetSpeed(Transform transform){

        
        if(lastPosition != transform.position) {
            ballSpeed = transform.position - lastPosition;
            ballSpeed /= Time.deltaTime;
            lastPosition = transform.position;
            return ballSpeed.magnitude;
        }

     return 0f;

    }

    public float GetYVelocity(Transform transform){

            if(lastYPosition != transform.localPosition.y) {
            speedInTermsOfYDirection = transform.localPosition.y - lastYPosition;
            speedInTermsOfYDirection /= Time.deltaTime;
            lastYPosition = transform.localPosition.y;
            return speedInTermsOfYDirection;
        }

     return 0f;

    }





}