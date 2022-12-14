using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // get the pos change of previous obstruction if >= thred then create one
    // Start is called before the first frame update
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;

    public int maxtimer = 1;
    private float timer = 0;
    private float maxtimer_temp;
    public int floating;
    bool trigger;

    IEnumerator waiter(int sec){
        yield return new WaitForSeconds(sec);
    }

    void Start()
    {
        maxtimer_temp = maxtimer;
        trigger = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalParameter.gamemode == 1 && !trigger){
            //wait 3 sec before generate
            waiter(3);
            trigger = true;
        }
        if (trigger){
            if (timer > maxtimer_temp){
                // spawn one of objects in obeject list
                GameObject target_obj;
                int randint = Random.Range(0,3);
                int floating = Random.Range(0,1);
                if (floating == 1) target_obj = obj1;//float plate form
                else if (randint == 0) target_obj = obj1;
                else if (randint == 1) target_obj = obj2;
                else if (randint == 2) target_obj = obj3;
                else if (randint == 3) target_obj = obj4;
                else target_obj = obj1;
                // GameObject target_gen = 
                GameObject newobj = Instantiate(target_obj);
                newobj.transform.position = transform.position;
                Destroy(newobj, 15);
                timer = 0;
                maxtimer_temp = (float)Random.Range(1,maxtimer);
            }
        }
        timer += Time.deltaTime;
    }
}
