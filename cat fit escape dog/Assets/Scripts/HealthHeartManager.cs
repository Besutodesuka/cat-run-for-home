using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeartManager : MonoBehaviour
{
    public static int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Image image in hearts)
        {
            image.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++){
            hearts[i].sprite = fullHeart;
        }
    }
}
