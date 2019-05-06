using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

    public GameObject settingsPanel;
    public GameObject sliderBgm;
    public GameObject sliderEffects;
    public GameObject noBgm;
    public GameObject noEffects;
    GameObject gm;
    GameObject om;
    public float bgmVolume;
    public float effectsVolume;
    public bool bgmActive = true;
    public bool effectsActive = true;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("Manager");
        om = GameObject.Find("OptionsManager");
        settingsPanel.SetActive(false);
       

        if(bgmActive) bgmVolume = sliderBgm.GetComponent<Slider>().value;
        if(effectsActive) effectsVolume = sliderEffects.GetComponent<Slider>().value;
    }

    // Update is called once per frame
    void Update () {

        //if(bgmActive) bgmVolume = sliderBgm.GetComponent<Slider>().value;
        //if(effectsActive) effectsVolume = sliderEffects.GetComponent<Slider>().value;

        if(bgmVolume != 0)
        {
            noBgm.SetActive(false);
        }
        else
        {
            noBgm.SetActive(true);
        }

        if (effectsVolume != 0)
        {
            noEffects.SetActive(false);
        }
        else
        {
            noEffects.SetActive(true);
        }

    }

    public void ChangeEffect()
    {
        effectsVolume = sliderEffects.GetComponent<Slider>().value;
    }

    public void ChangeBgm()
    {
        bgmVolume = sliderBgm.GetComponent<Slider>().value;
    }


    public void ShowPanel()
    {
        if (gm.GetComponent<GameManager>().gameState.Equals("play"))
        {
            settingsPanel.SetActive(true);
            gm.GetComponent<GameManager>().gameState = "pause";
        }
        else if (gm.GetComponent<GameManager>().gameState.Equals("pause"))
        {
            settingsPanel.SetActive(false);
            gm.GetComponent<GameManager>().gameState = "play";
        }

    }

    public void ShowMenu()
    {
        settingsPanel.SetActive(true);
    }

    public void HideMenu()
    {
        settingsPanel.SetActive(false);
    }

    public void NoBgmSound()
    {
        if (bgmVolume != 0)
        {
            bgmVolume = 0;
            bgmActive = false;
        }
        else
        {
            bgmActive = true;
        }
    }

    public void NoEffectsSound()
    {
        if (effectsVolume != 0)
        {
            effectsVolume = 0;
            effectsActive = false;
        }
        else
        {
            effectsActive = true;
        }
    }

}
