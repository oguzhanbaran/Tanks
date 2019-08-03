using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilStorageExp : MonoBehaviour
{
    public float mineDamage = 10f;
    public ParticleSystem[] explosion = new ParticleSystem[2];
    public void Awake()
    {
        for (int i = 0; i <= 2; i++)
        {
            explosion[i].gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        if (collision.transform.CompareTag("Tank"))
        {
            for (int i = 0; i <= 2; i++)
            {  
                explosion[i].gameObject.SetActive(true);
                explosion[i].Play();
                
            }
            collision.gameObject.GetComponent<TankMovement>().onJump();
            
            collision.gameObject.GetComponent<TankHealth>().TakeDamage(mineDamage);
            StartCoroutine(CoolDown());
            
        }
    }
    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
