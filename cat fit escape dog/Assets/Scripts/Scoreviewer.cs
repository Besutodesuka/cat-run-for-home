using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreviewer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text Text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Score : " + (int)ScoreManager.score + " Points";
        
    }
}
