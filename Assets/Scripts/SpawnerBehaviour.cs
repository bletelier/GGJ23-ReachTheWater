using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    
    private float max_x, min_x, max_y, min_y, max_z = 0f, min_z = -3f;
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float timeRespawn;
    private float timeStep;
    private Bounds bounds;
    private bool allowRespawn;
    private GameObject mainRoot;
    private int depth;
    private bool biggerLife = false;
    private bool fasterEnemy = false;
    private bool slowerEnemy = false;
    private int stride = 1;

    public void setRespawn(){
        allowRespawn = !allowRespawn;
    }

    public void turnOffBiggerLife(){
        biggerLife = false;
    }

    public void activeBiggerLife(){
        biggerLife = true;
    }

    public void turnOffFasterEnemy(){
        fasterEnemy = false;
    }

    public void activeFasterEnemy(){
        fasterEnemy = true;
    }

    public void turnOffSlowerEnemy(){
        slowerEnemy = false;
    }

    public void activeSlowerEnemy(){
        slowerEnemy = true;
    }

    void Start()
    {
        bounds = gameObject.GetComponent<Renderer>().bounds;
        allowRespawn = true;
        max_x = bounds.max.x;
        min_x = bounds.min.x;
        max_y = bounds.max.y;
        min_y = bounds.min.y;
        mainRoot = GameObject.FindGameObjectWithTag("root");
        depth = mainRoot.GetComponent<RootBehaviour>().getDepth();
    }

    
    void Update()
    {
        timeStep += Time.deltaTime;
        if(allowRespawn && timeStep >= (timeRespawn - 0.21f * depth)){
            timeStep = 0f;
            spawnEnemy();
        }
        depth = mainRoot.GetComponent<RootBehaviour>().getDepth();
    }

    void spawnEnemy(){
        int numberEnemy = Random.Range(0, enemies.Length);
        Vector2 pos = new Vector3(Random.Range(min_x, max_x), Random.Range(min_y, max_y), Random.Range(min_z, max_z));

        GameObject enemy = Instantiate(enemies[numberEnemy], pos, Quaternion.identity);
        if(depth>2) stride = 3;
        enemy.GetComponent<EnemyBehaviour>().life *= (depth*depth*stride);
        enemy.GetComponent<EnemyBehaviour>().Speed += depth*0.1f*((stride+1)/2);
        if(biggerLife){
            enemy.GetComponent<EnemyBehaviour>().life *= 1.2f;
        }
        else if(fasterEnemy){
            enemy.GetComponent<EnemyBehaviour>().Speed *= 1.05f;
        }
        else if(slowerEnemy){
            enemy.GetComponent<EnemyBehaviour>().Speed *= 0.75f;
        }

    }
}
