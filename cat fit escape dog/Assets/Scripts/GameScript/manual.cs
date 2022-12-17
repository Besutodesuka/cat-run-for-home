using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manual : MonoBehaviour
{
    [SerializeField] public GameObject Homebutton;
    [SerializeField] public GameObject Manual1;
    [SerializeField] public GameObject Manual2;
    [SerializeField] public GameObject Manual3;
    [SerializeField] public GameObject Manual4;
    [SerializeField] public GameObject Manual5;
    [SerializeField] public GameObject Manual6;
    [SerializeField] public GameObject bg;
    
    public void manualpage1(){
            bg.SetActive(true);
            Manual1.SetActive(true);
            Homebutton.SetActive(false);
            Manual2.SetActive(false);
            Manual3.SetActive(false);
            Manual4.SetActive(false);
            Manual5.SetActive(false);
            Manual6.SetActive(false);
	    }

    public void manualpage2(){
            bg.SetActive(true);
            Manual2.SetActive(true);
            Homebutton.SetActive(false);
            Manual1.SetActive(false);
            Manual3.SetActive(false);
            Manual4.SetActive(false);
            Manual5.SetActive(false);
            Manual6.SetActive(false);
	    }

    public void manualpage3(){
            bg.SetActive(true);
            Manual3.SetActive(true);
            Homebutton.SetActive(false);
            Manual1.SetActive(false);
            Manual2.SetActive(false);
            Manual4.SetActive(false);
            Manual5.SetActive(false);
            Manual6.SetActive(false);
	    }

    public void manualpage4(){
            bg.SetActive(true);
            Manual4.SetActive(true);
            Homebutton.SetActive(false);
            Manual1.SetActive(false);
            Manual2.SetActive(false);
            Manual3.SetActive(false);
            Manual5.SetActive(false);
            Manual6.SetActive(false);
	    }

    public void manualpage5(){
            bg.SetActive(true);
            Manual5.SetActive(true);
            Homebutton.SetActive(false);
            Manual1.SetActive(false);
            Manual2.SetActive(false);
            Manual3.SetActive(false);
            Manual4.SetActive(false);
            Manual6.SetActive(false);
	    }

    public void manualpage6(){
            bg.SetActive(true);
            Manual6.SetActive(true);
            Homebutton.SetActive(false);
            Manual1.SetActive(false);
            Manual2.SetActive(false);
            Manual3.SetActive(false);
            Manual4.SetActive(false);
            Manual5.SetActive(false);
	    }

    public void closemanual(){
            bg.SetActive(false);
            Homebutton.SetActive(true);
            Manual1.SetActive(false);
            Manual2.SetActive(false);
            Manual3.SetActive(false);
            Manual4.SetActive(false);
            Manual5.SetActive(false);
            Manual6.SetActive(false);
	    }
}
