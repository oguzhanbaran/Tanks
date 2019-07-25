using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSc : MonoBehaviour
{
    
    float x, z;
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Tank"))
        {

            if (col.gameObject.GetComponent<TankHealth>().m_CurrentHealth<100)
            {

                col.gameObject.GetComponent<TankHealth>().Heal(30);
                Destroy(gameObject);
                SpawnScript.powerUpNumber--;
                
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Building"))
        {
            Destroy(gameObject);
            Vector3 newPosition = new Vector3(Random.Range(-40, 40), 1, Random.Range(-40, 40));
            transform.position = newPosition;
        }
        
    }


}
