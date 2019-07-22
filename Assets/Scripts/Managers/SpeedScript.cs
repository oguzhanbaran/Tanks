using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedScript : MonoBehaviour
{
    public static float nowTime;
    void OnCollisionEnter(Collision col)
    {    
        
        if (col.transform.CompareTag("Tank"))
        {
            nowTime = Time.time;
            col.gameObject.GetComponent<TankMovement>().m_Speed = 30f;
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
