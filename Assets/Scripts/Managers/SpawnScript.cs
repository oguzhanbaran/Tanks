using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class SpawnScript : MonoBehaviour
{
    
    public float x=0, y=0;
    public GameObject Health;
    public GameObject Bullet;
    public GameObject Rocket;
    public float SpawnTime = 10f;

    public static int powerUpNumber = 0;
    bool canSpawnHealth;
    
    bool canSpawnBullet;
    bool canSpawnSpeed;
    int rndNumber;
    // Start is called before the first frame update
    void Start()
    {
        rndNumber = Random.Range(0, 3);
        canSpawnHealth = false;
        canSpawnBullet = false;
        canSpawnSpeed = false;   
        x = Random.Range(-40, 40);
        y = Random.Range(-40, 40);
        StartCoroutine(SpawnCool());
        
    }
    void Update()
    {
        if(powerUpNumber < 5)
        {
            SpawnHealth();
            SpawnBullet();
            SpawnSpeed();
            
        }
    }
    private void SpawnSpeed()
    {
        if (canSpawnSpeed)
        {
            powerUpNumber++;
            rndNumber = Random.Range(0, 3);
            x = Random.Range(-40, 40);
            y = Random.Range(-40, 40);
            Transform transformm = Rocket.GetComponent<Transform>();
            transformm.position = new Vector3(x, 1, y);
            canSpawnSpeed = false;
            Instantiate(Rocket, transformm.position, Quaternion.identity);
            StartCoroutine(SpawnCool());

        }
    }
    private void SpawnHealth()
    {
        if (canSpawnHealth)
        {
            powerUpNumber++;
            rndNumber = Random.Range(0, 3);
            x = Random.Range(-40, 40);
            y = Random.Range(-40, 40);
            Transform transformm=Health.GetComponent<Transform>();
            transformm.position = new Vector3(x, 1, y);
            canSpawnHealth = false;
            Instantiate(Health,transformm.position,Quaternion.identity);
            StartCoroutine(SpawnCool());
            
        }
    }
    
    private void SpawnBullet()
    {
        if (canSpawnBullet)
        {
            powerUpNumber++;
            rndNumber = Random.Range(0, 3);
            Transform transformm = Bullet.GetComponent<Transform>();
            transformm.position = new Vector3(x, 1, y);
            canSpawnBullet = false;
            Instantiate(Bullet, transformm.position, Quaternion.identity);
            x = Random.Range(-40, 40);
            y = Random.Range(-40, 40);
            StartCoroutine(SpawnCool());
        }
    }

    private IEnumerator SpawnCool()
    {
            yield return new WaitForSeconds(SpawnTime);
        if (rndNumber == 0)
            canSpawnHealth = true;
        else if (rndNumber == 1)
            canSpawnBullet = true;
        else if (rndNumber == 2)
            canSpawnSpeed = true;
   
    }
    
    
}
