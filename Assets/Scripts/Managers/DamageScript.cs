using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    private int right1 = 5;
    private int right2 = 5;
    public GameObject mine;
    Vector3 minePosition;


   
    private void dropMine()
    {
        minePosition = gameObject.transform.position + transform.forward * -1.0f * 5;
        if (Input.GetButtonDown("DropMine1"))
        {
            Instantiate(mine, minePosition,Quaternion.identity);
        }
        if (Input.GetButtonDown("DropMine2"))
        {
            Instantiate(mine,minePosition,Quaternion.identity);
        }  
    }
    private void Update()
    {
        dropMine();
    }
}
