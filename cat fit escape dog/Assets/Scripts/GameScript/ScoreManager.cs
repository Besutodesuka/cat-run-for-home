using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static float score;
    public Text Text;
    void Start()
    {
        Text = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Score : " + (int)score + " Points";
    }
}
