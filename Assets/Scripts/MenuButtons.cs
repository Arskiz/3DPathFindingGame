using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]GameObject settingsCParent;
    [SerializeField] bool settingsOpen = false;

    // Btns
    Button pButton;
    Button cButton;
    Button qButton;

    // Setting Categories
    [SerializeField] GameObject[] sCategories = {  };

    // Setting parent's parent
    [SerializeField] GameObject settingsInterface;

    // Credits object
    [SerializeField] GameObject creditsObject;

    void OnEnable()
    {
        if(sCategories.Length < 1)
        {
            sCategories = new GameObject[] { GameObject.Find("vSettings"), GameObject.Find("dSettings"), GameObject.Find("aSettings") };
        }
        

        settingsCParent.SetActive(false);

        // Initialize buttons
        pButton = GameObject.Find("PlayButton").GetComponent<Button>();
        cButton = GameObject.Find("CreditsButton").GetComponent<Button>();
        qButton = GameObject.Find("QuitButton").GetComponent<Button>();

        // Disable all menu stuff
        for(int i = 0; i < sCategories.Length; i++)
        {
            sCategories[i].SetActive(false);
        }
        settingsInterface.SetActive(false); // Set main parent's visibility OFF aswell
    }

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("Peli");
    }

    public void SettingsButton()
    {
        Toggle(settingsCParent);
    }

    public void CreditsButton()
    {
        settingsInterface.SetActive(false);
        creditsObject.SetActive(true);

        // Initialization for setting creditRoll's pos to start value
        CreditsRoller credits_roller = FindAnyObjectByType<CreditsRoller>();
        RectTransform rollObject = creditsObject.transform.GetChild(0).GetChild(0).GetComponent< RectTransform>();

        // Setting creditRoll's pos to start value to avoid credits starting mid way
        rollObject.anchoredPosition = credits_roller.startPos;

        // Disabling the main-gameobject from the way of credits-object
        transform.parent.gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit(0);
    }

    public void BackButton()
    {
        // NOTHING
    }

    void Toggle(GameObject target)
    {
        bool isEnabled = target.activeSelf;

        // Alt: target = !target;
        switch(isEnabled)
        {
            // Is currently active
            case true:
                target.SetActive(false);
                if (settingsInterface.activeSelf) settingsInterface.SetActive(false);
                break;

            // Is hidden
            case false:
                target.SetActive(true);
                break;
        }
    }

    void SetButtonsState(bool isEnabled)
    {
        // Napeille listahomma
        Button[] buttons = { pButton, cButton, qButton };

        // Alphajutut
        const float disabledAlpha = 0.39f; // 100/255
        const float enabledAlpha = 1.0f;

        // Looppi nappien läpi ettei tarvi hard koodata
        foreach (Button button in buttons)
        {
            button.interactable = !isEnabled;

            // Alphan päivitys
            Image buttonImage = button.gameObject.GetComponent<Image>();
            if (buttonImage != null)
            {
                Color newButtonColor = buttonImage.color;
                Color newTextColor = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
                newButtonColor.a = !isEnabled ? enabledAlpha : disabledAlpha;
                newTextColor.a = !isEnabled ? enabledAlpha : disabledAlpha;
                buttonImage.color = newButtonColor;
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = newTextColor;
            }
        }
    }

    public void CategoryButton(int iID)
    {
        for(int i = 0; i < sCategories.Length; i++)
        {
            if(iID == i)
            {
                SettingsDispatcher(iID);
            }
        }
    }


    void SettingsDispatcher(int settings_id)
    {
        SettingsCategory[] settingsC =
        {
            new SettingsCategory {SETTINGS_ID = 0, SETTINGS_TITLE = "Video", settingsContentOBJ = sCategories[0]},
            new SettingsCategory {SETTINGS_ID = 1, SETTINGS_TITLE = "Display", settingsContentOBJ = sCategories[1]},
            new SettingsCategory {SETTINGS_ID = 2, SETTINGS_TITLE = "Audio", settingsContentOBJ = sCategories[2]},
        };

        for(int i = 0; i < settingsC.Length; i++)
        {
            if (settings_id == settingsC[i].SETTINGS_ID)
                SettingsToggler(settingsC[i]);
        }
    }

    void SettingsToggler(SettingsCategory settingsCategory)
    {
        // Set menus open accoringly
        for(int i = 0; i < sCategories.Length; i++)
        {
            if (sCategories[i] != settingsCategory.settingsContentOBJ)
            {
                sCategories[i].SetActive(false);
            }
            else
            {
                sCategories[i].SetActive(true);
                settingsInterface.SetActive(true);
                // Set menu title text
                SetSettingsTitle(settingsCategory.SETTINGS_TITLE);
            }
        }
    }

    void SetSettingsTitle(string title)
    {
        TextMeshProUGUI t = settingsInterface.transform.GetChild(0).Find("SETTINGS_TITLE").GetComponent<TextMeshProUGUI>();
        t.text = title + " Settings";
    }

    void Update()
    {
        settingsOpen = settingsCParent.activeSelf;

        if (settingsOpen)
            SetButtonsState(true); // Disable
        else
            SetButtonsState(false); // Enable
        
    }

}

[System.Serializable]
class SettingsCategory
{
    public int SETTINGS_ID;
    public string SETTINGS_TITLE;
    public GameObject settingsContentOBJ;
}
