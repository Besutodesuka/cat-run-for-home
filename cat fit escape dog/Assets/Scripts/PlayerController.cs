using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    public DataReceiver _receiver;
    public Animator animator;
    public BoxCollider2D collider;

    public Vector2 standingsize;
    public Vector2 slidesize;

    private bool isjumpped;
    private bool isRun;
    private bool IsPause = false;

    public int height;
    private float speed;

    private string[] cam_controls;
    string action;
    string positions;
    string gamemodes;

    Dictionary<string, string> ActionsDict = new Dictionary<string, string>(){
        {"0","Standing"}, {"1","Slide"}, {"2","Jump"}
    };
    Dictionary<string, string> PositionsDict = new Dictionary<string, string>(){
        {"-1","Left"}, {"0","center"}, {"1","Right"}
    };
    Dictionary<string, string> GamemodesDict = new Dictionary<string, string>(){
        {"0","mainmenu"}, {"1","ingame"}, {"2","game over"}, {"3","pause"}
    };

    // Start is called before the first frame update
    void GetControl(){
        try{
            // try read data from web sockets
            string data = _receiver.data;
            // check raw data

            // unpack the command (Action,positions,gamemodes)
            cam_controls = data.Split(',');
            // decrypt using dictionary
        }catch(Exception err)
        {
            // print(err.ToString());
        }
    }

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
        action = "4";
        positions = "4";
        gamemodes = "4";

        // get input from camera
        GetControl();
        try{
            action = ActionsDict[cam_controls[0]];
            gamemodes = GamemodesDict[cam_controls[2]];
        }catch(Exception err){

        }
        

        // jump and start game 
        if ((Input.GetKey("up") || action  == "Jump") && isjumpped == false){
                // jump action
                body.velocity = new Vector3(0,height,0); // make player jump
                animator.SetBool("isjump",true);
                GlobalParameter.gamemode = 1;
        } else if (IsPause == true && gamemodes == "ingame"){
            UnPauseGame();
        }

        // controls in game
        if (GlobalParameter.gamemode == 1){
            // increment the score
            ScoreManager.score += Time.deltaTime*4/3;
            if (isjumpped == true){
                speed = 1;
            }
            else if ((Input.GetKey("up") || action == "Jump") && isjumpped == false){
                // jump action
                body.velocity = new Vector3(0,height,0); // make player jump
                isjumpped = true;
                animator.SetBool("isjump",true);
    ;

                isRun = false;

                //collider.size = standingsize;
            } 
            else if ((Input.GetKey("down") || action == "Slide") && isjumpped == false){
                // Slide action
                animator.SetBool("isslide",true);

                speed = Mathf.Min(1,speed+1);
                isjumpped = false;

                isRun = false;

                collider.size = slidesize;
            }
            // TODO: add runing action if possible in futher works
            else if ((Input.GetKey("right") == true) && (isjumpped == false)){
                // Run action
                speed = Mathf.Min(1,speed+1);

                //collider.size = standingsize;
            }
            else if (gamemodes == "pause"){
                PauseGame();
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

    void PauseGame(){
        // TODO: PauseGame
        // set pause menu visible
        // set game object freeze
        IsPause = true;
    }

    void UnPauseGame(){
         //TODO: unpause the game;
        // set all objects active
        // set pause menu visble to false
    }
    

    private void OnCollisionEnter2D(Collision2D col){
        isjumpped = false;
        animator.SetBool("isjump",false);
        animator.SetBool("isslide",false);
        //collider.size = standingsize;
    }
}
