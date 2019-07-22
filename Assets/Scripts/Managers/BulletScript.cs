using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public static float nowTime;
    void OnCollisionEnter(Collision col)
    {   
        //Debug.Log("Girdi!");
        if (col.transform.CompareTag("Tank"))
        {
            nowTime = Time.time;
            col.gameObject.GetComponent<TankShooting>().m_ShootCooldownDuration = 0.1f;
            Destroy(gameObject);
            SpawnScript.powerUpNumber--;

        }
        else
        {
            Vector3 newPosition = new Vector3(Random.Range(-40, 40), 1, Random.Range(-40, 40));
            transform.position = newPosition;
        }
    }
    
}
