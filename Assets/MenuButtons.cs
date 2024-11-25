using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    GameObject main;
    GameObject settings;
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        main = GameObject.Find("Main");
        settings = GameObject.Find("Settings");

        settings.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("Peli");
    }

    public void SettingsButton()
    {
        Switch(main, settings);
    }

    public void CreditsButton()
    {
        Debug.Log("Credits Button");
    }

    public void QuitButton()
    {
        Debug.Log("Quit Button");
    }

    public void BackButton()
    {
        Switch(settings, main);
    }

    void Switch(GameObject from, GameObject to)
    {
        from.SetActive(false);
        to.SetActive(true);
    }

}
