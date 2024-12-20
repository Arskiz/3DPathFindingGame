using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    GameObject pMenu;
    mLockType mLockTypes;
    GameObject pauseMenuBG;

    struct mLockType
    {
        public CursorLockMode lockMode;
        public CursorLockMode unlockMode;

        public mLockType(CursorLockMode lMode, CursorLockMode uLockMode)
        {
            lockMode = lMode;
            unlockMode = uLockMode;
        }
    }

    private void Start()
    {
        pMenu = GameObject.Find("PauseMenu");

        mLockTypes = new mLockType(CursorLockMode.Locked, CursorLockMode.None);
        pauseMenuBG = GameObject.Find("PauseMenuBG");
    }

    public void PauseGame()
    {
        paused = true;
    }

    public void ContinueGame()
    {
        paused = false;
    }

    void ToggleVisibility()
    {
        paused = !paused;
        
    }


    public void TitleScreen()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    void CursorH()
    {
        switch(paused)
        {
            case true:
                    Cursor.lockState = mLockTypes.unlockMode;
                break;

            case false:
                    Cursor.lockState = mLockTypes.lockMode;
                break;

        }
    }

    private void Update()
    {
        switch (paused)
        {
            case true: Time.timeScale = 0.0f; break;
            case false: Time.timeScale = 1.0f; break;
        }
        pMenu.SetActive(paused);
        pauseMenuBG.SetActive(paused);
        CursorH();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleVisibility();
        }
    }
}
