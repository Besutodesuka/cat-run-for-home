using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_keyboard : MonoBehaviour
{
    Rigidbody2D body;

    bool isjumpped;
    public int height;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        isjumpped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") && isjumpped == false){
            // jump action
            body.velocity = new Vector3(0,height,0); // make player jump
            isjumpped = true;
        } else if (Input.GetKey("down") && isjumpped ==false){
            // Slide action
            
        }
        else{
            // Idle Action
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        isjumpped = false;
    }
}

