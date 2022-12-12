using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Gameovermenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void InitialGame(){
        // gameObject.SetActive(true);
        // Gameovermenu.SetActive(false);
        // ScoreManager.score = 0;
        // GlobalParameter.gamemode  = 0;
        // HealthHeartManager.health = 3;
        //close camera by call on destroy
        
        // dispose model
        // PoseVisuallizer.detecter.Dispose();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
