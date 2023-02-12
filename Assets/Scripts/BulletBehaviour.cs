using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBehaviour : MonoBehaviour
{
    
    public GameObject explosion;
    public float speed = 4.5f;
    public int dmg_mtp = 1;
    public int bulletLife;
    public float mod_radius = 0f;
    Vector2 mouseOnScreen;

    void Start(){
        mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        GameObject manager = GameObject.Find("Manager");
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.material.SetFloat("_LightStrength", manager.GetComponent<SetIntensity>().intensity);
        //Sonar disparo sfx por tipo de bala
    }

    public void createExplosion(){
        GameObject ins_explosion = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        float x_scale = 1f * mod_radius;
        float y_scale = 1f * mod_radius;
        ins_explosion.transform.localScale += new Vector3(x_scale, y_scale,0f);
    }

    public void reduceBullet(){
        bulletLife -= 1;
    }

    public void increaseRadius(){
        mod_radius = 10f;
    }

    public void decreaseRadius(){
        mod_radius = 0f;
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * -speed;
        float x = transform.position.x;
        float y = transform.position.y;
        if(x >= 21f || x<=-10f || y >= 5f || y<=-5f) Destroy(gameObject);
        if(bulletLife == 0) Destroy(gameObject);
        //if(gameObject.tag.Equals("bullet-he") && x==mouseOnScreen.x && y==mouseOnScreen.y){
        //    Destroy(gameObject);
        //}
    }

    
}
