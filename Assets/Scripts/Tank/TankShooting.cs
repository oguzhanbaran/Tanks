using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class TankShooting : MonoBehaviour
{
    //Public
    public GameObject mine;
    public int m_PlayerNumber = 2;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 100f; 
    public float m_MaxChargeTime = 1.5f;
    public float m_ShootCooldownDuration = 0.5f;
    //Default
    float lastThrowDate;
    Vector3 minePosition;
    //Private 
    private int mineCount = 5; //Mine right of tanks
    private string m_DropMineButton;//Mine drop button
    private string m_FireButton; //Fire button        
    private float m_CurrentLaunchForce;//Current bullet power  
    private float m_ChargeSpeed;         
    private bool m_Fired;         
    private bool m_CanShoot;
    private float time2;//Pull bullet cooldown powerup time
    private float mineTime;//Time beetween of two mines
    private float shootTime;//Shoot cooldown time


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        mineTime = Time.time;
        m_CanShoot = true;
        m_FireButton = "Fire" + m_PlayerNumber;
        m_DropMineButton = "DropMine" + m_PlayerNumber;
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
    
    private void finishTime()
    {
        time2 = BulletScript.nowTime;
        if (Time.time-time2>=5f)
        {
            m_ShootCooldownDuration = 0.5f;
        }
        
    }

    private void Update()
    {
        dropMine();
        finishTime();
        m_AimSlider.value = m_MinLaunchForce;
        if (m_CanShoot==true)
        {
            if (m_CurrentLaunchForce >= m_MaxLaunchForce)
            {
                m_CurrentLaunchForce = m_MaxLaunchForce;
                Fire();
            }
            else if (Input.GetButtonDown(m_FireButton))
            {
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
            }
            else if (Input.GetButton(m_FireButton) && !m_Fired)
            {
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                m_AimSlider.value = m_CurrentLaunchForce;
            }
            else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
            {
                Fire();
            }
        }
        
    }
    private void dropMine()
    {
        minePosition = gameObject.transform.position + transform.forward * -1.0f * 2;
        if (Time.time-mineTime>10f)
        {
            if (Input.GetButtonDown(m_DropMineButton) && gameObject.transform.position.y <= 0.2f && mineCount > 0)
            {
                mineTime = Time.time;
                mineCount--;
                Instantiate(mine, minePosition, Quaternion.identity);
            }
        }  
    }

    private void Fire()
    {
         if (m_CanShoot)
         {
            shootTime = Time.time;
            m_Fired = true;
            m_CanShoot = false; 
            Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * m_CurrentLaunchForce*(m_CurrentLaunchForce/2));
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; ;
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play(); 
            m_CurrentLaunchForce = m_MinLaunchForce;
            StartCoroutine(ShootCooldown());
        }
         else
        {
            if (Time.time-shootTime>=0.5f)
            {
                m_CanShoot = true;
            }
        }
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(m_ShootCooldownDuration);
        m_CanShoot = true;
    }
    
}