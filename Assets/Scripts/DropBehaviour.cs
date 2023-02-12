using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehaviour : MonoBehaviour
{

    public GameObject player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag.Equals("bullet-he") || collision.gameObject.tag.Equals("bullet-ap")){
            if(gameObject.tag.Equals("drop-dmg")) player.GetComponent<PlayerBehaviour>().IncreaseDamage();
            else if(gameObject.tag.Equals("drop-fr")) player.GetComponent<PlayerBehaviour>().IncreaseRate();
            else if(gameObject.tag.Equals("drop-dmg-ng")) player.GetComponent<PlayerBehaviour>().DecreaseDamage();
            else if(gameObject.tag.Equals("drop-rate-ng")) player.GetComponent<PlayerBehaviour>().DecreaseRate();
            Destroy(gameObject);
        }
    }
}
