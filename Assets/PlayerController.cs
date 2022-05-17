using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 3;
    public float jumpStrength = 500;
    public int maxJumpCount = 3;


    [Header("References")]
    public Rigidbody rb;
    public InputManager input;
    public int curJumpCount;

    public bool canWallJump;
    public Vector3 wallNormal;
    
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        input = InputManager.Instance;
    }

    private void FixedUpdate() {
        move(input.moveDirection);

        
        
        if(input.flagJump){
            jump();
            input.flagJump = false;
        }
        




        if(isGrounded() && !input.flagJump){
            curJumpCount = maxJumpCount;
        }
        input.flagJump = false;
    }
    #region Movement Code

    void move(float dir){
        rb.velocity = new Vector3(dir * speed, rb.velocity.y,0);
    }

    void jump(){
        if(curJumpCount > 0){
            rb.AddForce(Vector3.up * jumpStrength);
            curJumpCount--;
        }
    }
    void wallJump(){

    }
    #endregion

    #region Helpers
    bool isGrounded(){
        RaycastHit ray1,ray2;
        Physics.Raycast(transform.position + new Vector3(-0.45f,-1f,0), Vector3.down, out ray1);
        Physics.Raycast(transform.position + new Vector3(0.45f,-1f,0), Vector3.down, out ray2);

        return (ray1.distance < .1f && ray2.distance < .1f);
    }


    #endregion

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + new Vector3(-0.45f,-1f,0), Vector3.down);
        Gizmos.DrawRay(transform.position + new Vector3(0.45f,-1f,0), Vector3.down);
    }

}
