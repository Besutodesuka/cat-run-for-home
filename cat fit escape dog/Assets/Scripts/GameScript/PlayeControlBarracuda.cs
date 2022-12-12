using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeControlBarracuda : MonoBehaviour
{
    Rigidbody2D body;
    public Animator animator;
    public BoxCollider2D collider;

    public Vector2 standingsize;
    public Vector2 slidesize;

    private bool isjumpped;
    private bool isRun;

    public int height;
    private float speed;

    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        isjumpped = false;
        isRun = false;
        speed = 0;
        collider = GetComponent<BoxCollider2D>();
        //collider.size = standingsize;
    }
    // Update is called once per frame
    void Update()
    {
        // jump and start game 
        if ((Input.GetKey("up") || PoseVisuallizer.input_pose == "jump") && isjumpped == false){
                // jump action
                body.velocity = new Vector3(0,height,0); // make player jump
                animator.SetBool("isjump",true);
                start = true;
                GlobalParameter.gamemode = 1;
            }
        if (GlobalParameter.gamemode == 1 && start){
            ScoreManager.score += Time.deltaTime*4/3;
            if (isjumpped == true){
                speed = 1;
            }
            else if (Input.GetKey("up") && isjumpped == false){
                // jump action
                body.velocity = new Vector3(0,height,0); // make player jump
                isjumpped = true;
                animator.SetBool("isjump",true);
    ;

                isRun = false;

                //collider.size = standingsize;
            } 
            else if (Input.GetKey("down") && isjumpped == false){
                // Slide action
                animator.SetBool("isslide",true);

                speed = Mathf.Min(1,speed+1);
                isjumpped = false;

                isRun = false;

                collider.size = slidesize;
            }
            else if ((Input.GetKey("right") == true) && (isjumpped == false)){
                // Run action
                speed = Mathf.Min(1,speed+1);


                //collider.size = standingsize;
            }
            else{
                // Idle action
                // speed = Mathf.Max(0,speed-1);
                speed = Mathf.Min(1,speed+1);

                animator.SetBool("isslide",false);

                //collider.size = standingsize;
            }
            GlobalParameter.global_speed = speed;
            animator.SetFloat("speed",speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        isjumpped = false;
        animator.SetBool("isjump",false);
        animator.SetBool("isslide",false);
        //collider.size = standingsize;
    }
}