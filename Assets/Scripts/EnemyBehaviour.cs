using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    
    [SerializeField] private GameObject[] drops;
    [SerializeField] private GameObject[] drop_neg;
    public SetIntensity si;
    int id_drop = 0;
    int chance = 0;
    public float Speed = 1f;
    public float life = 4;
    private GameObject mainRoot;

    void Start(){
        mainRoot = GameObject.Find("Root");
        GameObject manager = GameObject.Find("Manager");
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.material.SetFloat("_LightStrength", manager.GetComponent<SetIntensity>().intensity);
    }

    void Update()
    {
        transform.position += -1f*transform.right * Time.deltaTime * Speed;
        if(life <= 0){
            id_drop = Random.Range(0, drops.Length);
            chance = Random.Range(0, 100);
            if(chance%4==0){
                Vector2 pos = gameObject.transform.position;
                Instantiate(drops[id_drop], pos, Quaternion.identity);
            }
            else if(chance%7==0){
                Vector2 pos = gameObject.transform.position;
                Instantiate(drop_neg[id_drop], pos, Quaternion.identity);
            }
            mainRoot.GetComponent<RootBehaviour>().increaseCharge();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag.Equals("bullet-he")){
            
            int mtp = collision.gameObject.GetComponent<BulletBehaviour>().dmg_mtp;
            mainRoot.GetComponent<RootBehaviour>().playFx(0);
            collision.gameObject.GetComponent<BulletBehaviour>().createExplosion();
            
            if(gameObject.tag.Equals("enemy-l")) life -=2 * mtp;
            else if(gameObject.tag.Equals("enemy-h")) life -=1 * mtp;
            else if(gameObject.tag.Equals("enemy-n")) life -= 1.5f * mtp;
            collision.gameObject.GetComponent<BulletBehaviour>().reduceBullet();

        } else if(collision.gameObject.tag.Equals("bullet-ap")){
            int mtp = collision.gameObject.GetComponent<BulletBehaviour>().dmg_mtp;
            mainRoot.GetComponent<RootBehaviour>().playFx(1);

            if(gameObject.tag.Equals("enemy-l")){
                life -=1 * mtp;
                collision.gameObject.GetComponent<BulletBehaviour>().reduceBullet();
            }
            else if(gameObject.tag.Equals("enemy-n")){
                life -= 1.5f * mtp;
                collision.gameObject.GetComponent<BulletBehaviour>().reduceBullet();
            } 
            else if(gameObject.tag.Equals("enemy-h")){
                life -= (2 * mtp);
                Destroy(collision.gameObject);
            }
            
        } else if(collision.gameObject.tag.Equals("explosion")){
            if(gameObject.tag.Equals("enemy-l")) life -=2;
            else if(gameObject.tag.Equals("enemy-h")) life -=1;
            else if(gameObject.tag.Equals("enemy-n")) life -=4;
        }
    }
}
