using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] private BulletBehaviour[] bullet;
    public Transform launchBullet;
    int id_bullet = 0;
    public int global_dmg = 1;
    public float fireRate = 1e-2f;
    private float nextFire = 0.0f;
    private bool allowShot;
    private bool doubleShot = false;
    private bool bigger_radius = false;
    private bool slowerShots = false;
    public List<AudioClip> shoots;

    public Image uiB1;
    public Image uiB2;

    public List<Sprite> b1;
    public List<Sprite> b2;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        allowShot = true;
        audioSource = audioSource.GetComponent<AudioSource>();
        uiB1.sprite = b1[1];
        uiB2.sprite = b2[0];
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(-b.y,-b.x) * Mathf.Rad2Deg;
    }

    public void turnOffDoubleShot(){
        doubleShot = false;
    }

    public void activeDoubleShot(){
        doubleShot = true;
    }

    public void turnOffBiggerRadius(){
        bigger_radius = false;
    }

    public void activeBiggerRadius(){
        bigger_radius = true;
    }

    public void turnOffSlowerShots(){
        slowerShots = false;
    }

    public void activeSlowerShots(){
        slowerShots = true;
    }

    public void IncreaseDamage(){
        print("Damage Increased");
        if(global_dmg < 10) global_dmg += 1;
    }

    public void IncreaseRate(){
        print("Rate Increased");
        if(fireRate > 5e-3f) fireRate /=1.5f;
    }

    public void DecreaseRate(){
        print("Rate decreased");
        print(fireRate);
        if(fireRate < 5e-2f) fireRate *=1.5f;
        print(fireRate);
    }
    public void DecreaseDamage(){
        print("Damage decreased");
        print(global_dmg);
        if(global_dmg > 2) global_dmg -= 1;
        print(global_dmg);
    }

    public void canShot(){
        allowShot = !allowShot;
    }

    void Update()
    {
        
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        //transform.LookAt(mouseOnScreen);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f + angle));
        //transform.eulerAngles = new Vector3(0f, 0f,-45f -transform.eulerAngles.x);

        if(Input.GetKeyDown("1")) {
            id_bullet = 0;
            uiB1.sprite = b1[1];
            uiB2.sprite = b2[0];
        }
        if(Input.GetKeyDown("2")){
            id_bullet = 1 ;
            uiB1.sprite = b1[0];
            uiB2.sprite = b2[1];
        }

        if(allowShot && Input.GetButton("Fire1") && Time.time > nextFire){
            nextFire = Time.time + fireRate;
            BulletBehaviour bullet_fire = Instantiate(bullet[id_bullet], launchBullet.position, Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.eulerAngles.z + 90.0f)));
            audioSource.PlayOneShot(shoots[id_bullet]);
            //bullet_fire.gameObject.GetComponent<Rigidbody2D>().AddForce(bullet_fire.transform.up * -1f);
            bullet_fire.GetComponent<BulletBehaviour>().dmg_mtp = global_dmg;
            if(doubleShot){
                BulletBehaviour dbullet_fire = Instantiate(bullet[id_bullet], launchBullet.position + (new Vector3(0f,0.5f,0f)), Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.eulerAngles.z + 90.0f)));
                //dbullet_fire.gameObject.GetComponent<Rigidbody2D>().AddForce(dbullet_fire.transform.up * -1f);
                dbullet_fire.GetComponent<BulletBehaviour>().dmg_mtp = global_dmg;
                if(slowerShots){
                    dbullet_fire.GetComponent<BulletBehaviour>().speed *= 0.9f;
                }
            }
            else if(bigger_radius){
                if(id_bullet == 0){
                    bullet_fire.GetComponent<BulletBehaviour>().increaseRadius();
                }
            }
            if(slowerShots){
                bullet_fire.GetComponent<BulletBehaviour>().speed *= 0.9f;
            }
            
        }

       
        
        

    }
}
