using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
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
            body.velocity = new Vector3(0,height,0); // make player jump
            isjumpped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        isjumpped = false;
    }
}
