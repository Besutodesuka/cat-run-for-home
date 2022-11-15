using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Import the library 'UnityEngine.SceneManagement' to manage the scene.
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    //Declare a public variable. this variable show on Inspector in Unity to get the scene name on Unity.
    public string sceneName;
    void Start()
    {

    }

    void Update()
    {

    }

    // Declare a public method, this method will show in Unity and you can call it from Unity.
    public void OnNextScene()
    {
        // Load scene from scene name in 'sceneName' variable;
        SceneManager.LoadScene(sceneName);
    }
}
