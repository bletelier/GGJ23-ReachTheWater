using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class RootBehaviour : MonoBehaviour
{
    bool nextPhase = false;
    private int charge;
    private int limitCharge = 25;
    public GameObject waterOfVictory;
    private string[] buffs = {"double-shot","bigger-radius-explosion","slower-enemies"};
    private string[] debuffs = {"high-hp","faster-enemies","slower-shots"};
    private int id_buff = -1;
    private int id_debuff = -1;
    private GameObject spawner;
    private GameObject player;
    private string[] tags = {"enemy-l", "enemy-h", "enemy-n", "drop-fr", "drop-dmg", "bullet-he", "bullet-ap", "drop-dmg-ng", "drop-rate-ng"};
    private int depth;
    private bool lastLevel = false;
    public Image root_power;
    public Image root_win;
    public Transform bg;
    private float[] fA = {0.0f, 0.2f, 0.3f,0.2f,0.15f};
    private float[] tA = {0.0f, 26.5f, 38.0f,21.5f,9.0f};
    public SetIntensity si;
    public Light2D ligth;

    public List<AudioClip> musicPerLevel;
    public AudioSource audioSource;

    public List<AudioClip> fxs;

    public AudioSource fxAudioSource;

    bool ended = false;



    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("spawner");
        player = GameObject.FindGameObjectWithTag("Player");
        charge = 0;
        audioSource = audioSource.GetComponent<AudioSource>();
        fxAudioSource = audioSource.GetComponent<AudioSource>();
        root_power.fillAmount = 0;
        depth = 1;
    }

    public void playFx(int bullet) {
        fxAudioSource.PlayOneShot(fxs[bullet]);
    }

    public void increaseCharge(){
        charge += 1;
        root_power.fillAmount = (charge/(1.0f*limitCharge));
        print("Charge increase "+root_power.fillAmount);
    }

    void changePhase(){
        // destroy Instances
        
        spawner.GetComponent<SpawnerBehaviour>().setRespawn();
        player.GetComponent<PlayerBehaviour>().canShot();
        charge = 0;
        root_power.fillAmount = 0;
        limitCharge += 25;
        audioSource.Stop();
        if(!ended)
            audioSource.clip = musicPerLevel[depth];
        depth += 1;
        
        
    }

    public int getDepth(){
        return depth;
    }

    void clearScreen(){
        foreach(string tag in tags){
            GameObject[] garbage = GameObject.FindGameObjectsWithTag(tag);
            foreach(GameObject x in garbage) GameObject.Destroy(x);
        }
    }

    void chooseBuffDebuff(){

        player.GetComponent<PlayerBehaviour>().turnOffDoubleShot();
        player.GetComponent<PlayerBehaviour>().turnOffBiggerRadius();
        player.GetComponent<PlayerBehaviour>().turnOffSlowerShots();

        spawner.GetComponent<SpawnerBehaviour>().turnOffBiggerLife();
        spawner.GetComponent<SpawnerBehaviour>().turnOffFasterEnemy();
        spawner.GetComponent<SpawnerBehaviour>().turnOffSlowerEnemy();

        id_buff = Random.Range(0,buffs.Length-1);
        id_debuff = Random.Range(0,debuffs.Length-1);

        if(buffs[id_buff].Equals("double-shot")) player.GetComponent<PlayerBehaviour>().activeDoubleShot();
        if(buffs[id_buff].Equals("bigger-radius-explosion")) player.GetComponent<PlayerBehaviour>().activeBiggerRadius();
        if(buffs[id_buff].Equals("slower-enemies")) spawner.GetComponent<SpawnerBehaviour>().activeSlowerEnemy();
        

        if(debuffs[id_debuff].Equals("high-hp")) spawner.GetComponent<SpawnerBehaviour>().activeBiggerLife();
        if(debuffs[id_debuff].Equals("faster-enemies")) spawner.GetComponent<SpawnerBehaviour>().activeFasterEnemy();
        if(debuffs[id_debuff].Equals("slower-shots")) player.GetComponent<PlayerBehaviour>().activeSlowerShots();

        print("Buff choosen: "+buffs[id_buff]+", Debuff choosen: "+debuffs[id_debuff]);
    }

    private IEnumerator buffWait(float grow, float tr){
        float acm = 0.0f;
        float am = grow/200.0f;
        float tm = tr/200.0f;
        float im = (33.33f/200.0f)/100.0f;
        Material mt = player.GetComponent<SpriteRenderer>().material;
        for (int i = 0; i < 200;++i)
        {
            acm += im;
            root_win.fillAmount += am;
            bg.position += new Vector3(0,tm,0);
            mt.SetFloat("_LightStrength",si.intensity - acm);
            ligth.intensity = (si.intensity - acm);
            yield return new WaitForSeconds(0.01f);
        }
        if(lastLevel){
            print("WIN\n");
            SceneManager.LoadScene("VictoryScreen");
            //Time.timeScale = 0;
        }
        else {
            si.nextIntensity();
            yield return new WaitForSeconds(0.5f);
            chooseBuffDebuff();
            yield return new WaitForSeconds(2.5f);
            print("Buff & Debuff Chosen\n");
            activateNewPhase();
            audioSource.Play();
        }
    }

    void getBuffAndDebuffs(){
        StartCoroutine(buffWait(fA[depth-1], tA[depth-1]));

    }

    void activateNewPhase(){
        print("To new phase\n");
        gameObject.transform.position = new Vector3(-2.9f, 3.8f, 0.0f);
        gameObject.transform.localScale = new Vector3(5.3f, 5.3f, 1.0f);
        spawner.GetComponent<SpawnerBehaviour>().setRespawn();
        player.GetComponent<PlayerBehaviour>().canShot();
        if(depth == 4){
            lastLevel = true;
            //Instantiate(waterOfVictory, new Vector3(-2.6f, -4.5f, 0f), Quaternion.identity);
        }
        print("Depth level "+depth);
    }

    
    void Update()
    {
        
        if(charge==limitCharge) nextPhase = true;

        if(nextPhase && !ended){
            nextPhase = false;
            if(lastLevel) ended = true;
            changePhase();
            clearScreen();
            getBuffAndDebuffs();
            
        }

    }
}
