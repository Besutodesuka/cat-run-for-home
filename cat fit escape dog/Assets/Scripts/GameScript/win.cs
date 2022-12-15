using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public GameObject Winscene;
    float targetTime;
    [SerializeField] public int time = 1;

    void Start()
    {
        targetTime = Time.realtimeSinceStartup + (time * 60);
    }

    void Update()
    {
        if (Time.realtimeSinceStartup >= targetTime)
        {
            float translation = -1 * Time.deltaTime * 10;
            transform.Translate( translation , 0, 0);
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        // Check this GameObject hits target
        if (col.gameObject == target)
        {
            Winscene.SetActive(true);
            Time.timeScale = 0f;    
        }
    }
}
