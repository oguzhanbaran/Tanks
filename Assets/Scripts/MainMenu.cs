using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        Debug.Log("Tıklandı");
        SceneManager.LoadScene(1);
    }
   
    public void Test()
    {

    }
}
