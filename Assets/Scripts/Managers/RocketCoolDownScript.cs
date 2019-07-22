using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RocketCoolDownScript : MonoBehaviour
{
    public Slider RocketSlider;
    public Color fullTimeColor = Color.green;
    public Color zeroTimeColor = Color.red;
    public Image fillImage;
    float time = 10f;

    void Start()
    {
        RocketSlider.value = time;
        StartCoroutine(CoolDown());
    }
    private void SetHealthUI()
    {
        RocketSlider.value = time;
        fillImage.color = Color.Lerp(zeroTimeColor, fullTimeColor, time / 10);
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
