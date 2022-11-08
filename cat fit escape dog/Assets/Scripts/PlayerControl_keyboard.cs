using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_keyboard : MonoBehaviour
{
    Rigidbody2D body;
    public Animator animator;

    private bool isjumpped;
    private bool isSlide;
    private bool isRun;
    public int height;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        isjumpped = false;
        isSlide = false;
        isRun = false;
        speed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") && isjumpped == false){
            // jump action
            body.velocity = new Vector3(0,height,0); // make player jump
            isjumpped = true;
            animator.SetBool("isjump",true);

            isSlide = false;
            animator.SetFloat("speed",speed);
            isRun = false;
        } 
        else if (Input.GetKey("down")  && isjumpped == false){
            // Slide action
            isSlide = true;
            animator.SetBool("isslide",true);
            
            isjumpped = false;
            animator.SetFloat("speed",speed);
            isRun = false;
        }
        else if ((Input.GetKey("right") == true) && (isjumpped == false)){
            // Slide action
            speed = Mathf.Min(1,speed+1);
            animator.SetFloat("speed",speed);
        }
        else{
            speed = Mathf.Max(0,speed-1);
            animator.SetFloat("speed",speed);
            isSlide = false;
            animator.SetBool("isslide",false);
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        isjumpped = false;
        isSlide = false;
        animator.SetBool("isjump",false);
        animator.SetBool("isslide",false);
    }
}

