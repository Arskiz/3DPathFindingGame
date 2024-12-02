using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingDispatcher : MonoBehaviour
{
    [Header("Sliders")]
    public List<Slider> settingSlider = new List<Slider>();
    // Start is called before the first frame update
    void Start()
    {
        // Add sliders here manually
        settingSlider.Add(GameObject.Find("VolSlider").GetComponent<Slider>()); // Audio slider -> Volume

        GatherData();
    }

    void GatherData()
    {
        foreach (var slider in settingSlider)
        {
            string oName = slider.name;
            slider.value = PlayerPrefs.GetFloat(oName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update slider data
        for (int i = 0; i < settingSlider.Count; i++)
        {
            int index = i;
            string oName = settingSlider[index].name; // Object name to add in the PlayerPrefs for later value obtaining
            float sliderValue = settingSlider[index].value;

            // Save to PlayerPrefs:
            PlayerPrefs.SetFloat(oName, sliderValue);
        }

        // Update slider value text
        foreach (var slider in settingSlider)
        {
            float amount = slider.value * 100;
            TextMeshProUGUI valueTextObj = slider.transform.parent.GetChild(0).Find("Amount").GetComponent<TextMeshProUGUI>();

            // Set text
            valueTextObj.text = $"[{amount.ToString("F0")}]";
        }
    }
}
