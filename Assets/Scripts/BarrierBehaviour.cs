using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarrierBehaviour : MonoBehaviour
{
    public int life = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life<1){
            print("LOSE\n");
            SceneManager.LoadScene("DefeatScreen");
            //Time.timeScale = 0; 
        }
    }
    void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag.Equals("enemy-l") || collision.gameObject.tag.Equals("enemy-h") || collision.gameObject.tag.Equals("enemy-n")){
            life -= 1;
            Destroy(collision.gameObject);
        }
    }
}
