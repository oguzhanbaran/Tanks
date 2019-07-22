using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BulletCoolDownScript : MonoBehaviour
{
    public Slider BulletSlider;
    public Color fullTimeColor = Color.green;
    public Color zeroTimeColor = Color.red;
    public Image fillImage;
    float time = 10f;
    
    void Start()
    {
        BulletSlider.value = time;
        StartCoroutine(CoolDown());
    }
    private void SetHealthUI()
    {
        BulletSlider.value = time;
        fillImage.color = Color.Lerp(zeroTimeColor, fullTimeColor, time/10);
    }
    void Update()
    {
          
    }
    private IEnumerator CoolDown()
    {
        while (time>=0.0f)
        {
            time -= Time.deltaTime;
            SetHealthUI();
            yield return null;
        }
        Destroy(gameObject);
        SpawnScript.powerUpNumber--;
    }
}
