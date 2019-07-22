using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class TankShooting : MonoBehaviour
{
   
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
    float lastThrowDate;
    //public float time;
    
    
    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;         
    private bool m_CanShoot;
    private float time2;
    private float time1;


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {

        m_CanShoot = true;
        m_FireButton = "Fire" + m_PlayerNumber;

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
        finishTime();
        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (m_CurrentLaunchForce >= m_MaxLaunchForce)// && !m_Fired)
        {
            Debug.Log("Condition1");
            // ... use the max force and launch the shell.
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
        // Otherwise, if the fire button has just started being pressed...
        else if (Input.GetButtonDown(m_FireButton))
        {
            // ... reset the fired flag and reset the launch force.
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

            // Change the clip to the charging clip and start it playing.
            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (Input.GetButton(m_FireButton) && !m_Fired)
        {
            // Increment the launch force and update the slider.
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            m_AimSlider.value = m_CurrentLaunchForce;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
        {
            Fire();        
        }
    }


    private void Fire()
    {
         if (m_CanShoot)
         {
            time1 = Time.time;
            m_Fired = true;
            m_CanShoot = false;
            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * m_CurrentLaunchForce*(m_CurrentLaunchForce/2));
            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; ;

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            // Reset the launch force.  This is a precaution in case of missing button events.
            m_CurrentLaunchForce = m_MinLaunchForce;

            StartCoroutine(ShootCooldown());
        }
         else
        {
            if (Time.time-time1>=0.5f)
            {
                m_CanShoot = true;
            }
        }
    }

    private IEnumerator ShootCooldown()
    {
        
        yield return new WaitForSeconds(m_ShootCooldownDuration);
        Debug.Log("Ates etti");
        m_CanShoot = true;
    }
    
}