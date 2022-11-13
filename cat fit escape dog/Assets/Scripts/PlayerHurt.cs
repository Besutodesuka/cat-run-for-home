using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.transform.tag == "obstruction"){
            HealthHeartManager.health--;
            if(HealthHeartManager.health <= 0){
                GlobalParameter.gamemode = 2;//game over
                gameObject.SetActive(false);
            } else{
                //make player invisible a second
                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt(){
        Physics2D.IgnoreLayerCollision(7,8);
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(7,8,false);
    }
}
