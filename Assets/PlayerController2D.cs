using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Shizounu.Library.Editor;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 3;
    public float jumpStrength = 500;
    public bool canMultiJump;
    public int maxJumpCount = 3;

    [Header("References")]
    [ReadOnly, SerializeField] private Rigidbody2D rb;
    [ReadOnly, SerializeField] private InputManager input;
    [ReadOnly, SerializeField] private int curJumpCount;
    [ReadOnly, SerializeField] private float coyoteTime;
    [ReadOnly, SerializeField] public bool bufferedJump;
    [ReadOnly, SerializeField] public bool bufferedHighJump;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        input = InputManager.Instance;
    }

    private void FixedUpdate() {
        move(input.moveDirection);

        if(!canMultiJump)
            coyoteTime -= Time.deltaTime;

        //Buffer jumps if in air
        if(!isGrounded() && input.flagJump && (curJumpCount == 0)){
            input.flagJump = false;
            bufferedJump = true;
        }
        if(!isGrounded() && input.flagHighJump && (curJumpCount == 0)){
            input.flagHighJump = false;
            bufferedHighJump = true;
        }

        //actual jump handling
        if(input.flagJump && coyoteTime > 0){
            jump();
            input.flagJump = false;
        }
        if(input.flagHighJump && coyoteTime > 0){
            highJump();
            input.flagHighJump = false;
        }


        if(isGrounded()){
            curJumpCount = (canMultiJump) ? maxJumpCount : 1;
            coyoteTime = .4f;

            if(bufferedJump){
                jump();
                bufferedJump = false;
            }
            if(bufferedHighJump){
                highJump();
                bufferedHighJump = false;
            }
        }
    }

    
    void move(float dir){
        rb.velocity = new Vector3(dir * speed, rb.velocity.y,0);
    }
    void jump(){
        if(curJumpCount > 0){
            rb.AddForce(Vector3.up * jumpStrength);
            curJumpCount--;
        }
    }
    void highJump(){
        if(curJumpCount > 0){
            rb.AddForce(Vector3.up * jumpStrength * 2);
            curJumpCount--;
        }
    }
    bool isGrounded(){
        RaycastHit2D ray1;
        ray1 = Physics2D.Raycast(transform.position + new Vector3(0,-1.005f,0), Vector3.down);
        return ray1.distance < .2f;
    }
}
