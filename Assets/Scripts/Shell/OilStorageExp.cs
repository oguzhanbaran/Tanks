using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilStorageExp : MonoBehaviour
{
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
        if (collision.transform.CompareTag("Tank"))
        {
            for (int i = 0; i <= 2; i++)
            {
                Vector3 explosionPosition = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
                explosion[i].gameObject.SetActive(true);
                explosion[i].Play();
                gameObject.GetComponent<Rigidbody>().AddExplosionForce(10000f, explosionPosition, 10f);
                TankMovement.setJump = true;
                TankHealth.mineAmount = 20f;
            }
            StartCoroutine(CoolDown());
            
        }
    }
    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
