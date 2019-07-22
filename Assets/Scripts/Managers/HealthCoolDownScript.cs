using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCoolDownScript : MonoBehaviour
{
    public Slider HealthSlider;
    public Color fullTimeColor = Color.green;
    public Color zeroTimeColor = Color.red;
    public Image fillImage;
    float time = 10f;

    void Start()
    {
        HealthSlider.value = time;
        StartCoroutine(CoolDown());
    }
    private void SetHealthUI()
    {
        HealthSlider.value = time;
        fillImage.color = Color.Lerp(zeroTimeColor, fullTimeColor, time / 10);
    }
    void Update()
    {

    }
    private IEnumerator CoolDown()
    {
        while (time >= 0.0f)
        {
            time -= Time.deltaTime;
            SetHealthUI();
            yield return null;
        }
        Destroy(gameObject);
        SpawnScript.powerUpNumber--;
    }
}
