using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPlay : MonoBehaviour {

    public GameObject settingsPanel;
    public GameObject sliderBgm;
    public GameObject sliderEffects;
    public GameObject noBgm;
    public GameObject noEffects;
    public GameObject mainMenu;

    GameObject gm;
    GameObject om;
    GameObject dbrank;

    float bgmVolume;
    float effectsVolume;
    bool bgmActive;
    bool effectsActive;


    // Use this for initialization
    void Start () {
        om = GameObject.Find("OptionsManager");
        gm = GameObject.Find("Manager");
        dbrank = GameObject.Find("Rank");

        settingsPanel.SetActive(false);

        bgmVolume = om.GetComponent<OptionsManager>().bgmVolume;
        effectsVolume = om.GetComponent<OptionsManager>().effectsVolume;
        bgmActive = om.GetComponent<OptionsManager>().bgmActive;
        effectsActive = om.GetComponent<OptionsManager>().effectsActive;

        sliderBgm.GetComponent<Slider>().value = bgmVolume;
        sliderEffects.GetComponent<Slider>().value = effectsVolume;

    }
	
	// Update is called once per frame
	void Update () {

        if (bgmActive)
        {
            noBgm.SetActive(false);
        }
        else
        {
            noBgm.SetActive(true);
        }

        if (effectsActive)
        {
            noEffects.SetActive(false);
        }
        else
        {
            noEffects.SetActive(true);
        }
    }

    public void ShowPanel()
    {
        if (gm.GetComponent<GameManager>().gameState.Equals("play"))
        {
            settingsPanel.SetActive(true);
            gm.GetComponent<GameManager>().gameState = "pause";
            if (bgmVolume == 0) bgmActive = false;
            if (effectsVolume == 0) effectsActive = false;
        }
        else if (gm.GetComponent<GameManager>().gameState.Equals("pause"))
        {
            settingsPanel.SetActive(false);
            gm.GetComponent<GameManager>().gameState = "play";
        }

    }

    public void NoBgmSound()
    {
        if (bgmVolume != 0)
        {
            bgmVolume = 0;
            om.GetComponent<OptionsManager>().bgmVolume = 0;
            sliderBgm.GetComponent<Slider>().value = bgmVolume;
            bgmActive = false;
            om.GetComponent<OptionsManager>().bgmActive = false;
        }
        else
        {
            bgmActive = true;
            om.GetComponent<OptionsManager>().bgmActive = true;
            om.GetComponent<OptionsManager>().bgmVolume = 0.3f;
            bgmVolume = 0.3f;
            sliderBgm.GetComponent<Slider>().value = bgmVolume;
        }
    }

    public void NoEffectsSound()
    {
        if (effectsVolume != 0)
        {
            effectsVolume = 0;
            om.GetComponent<OptionsManager>().effectsVolume = 0;
            sliderEffects.GetComponent<Slider>().value = effectsVolume;
            effectsActive = false;
            om.GetComponent<OptionsManager>().effectsActive = false;
        }
        else
        {
            effectsActive = true;
            om.GetComponent<OptionsManager>().effectsActive = true;
            om.GetComponent<OptionsManager>().effectsVolume = 0.3f;
            effectsVolume = 0.3f;
            sliderEffects.GetComponent<Slider>().value = effectsVolume;
        }
    }

    public void GoToHome()
    {
        //dbrank.GetComponent<DB_Rank>().makeRank = true;
        SceneManager.LoadScene("Menu");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    } 

    public void ChangeEffect()
    {
        if (effectsActive)
        {
            effectsVolume = sliderEffects.GetComponent<Slider>().value;
            om.GetComponent<OptionsManager>().effectsVolume = effectsVolume;
        }
        else
        {
            effectsVolume = sliderEffects.GetComponent<Slider>().value;
            om.GetComponent<OptionsManager>().effectsVolume = effectsVolume;
            effectsActive = true;
            om.GetComponent<OptionsManager>().effectsActive = true;
        }
    }

    public void ChangeBgm()
    {
        if (bgmActive)
        {
            bgmVolume = sliderBgm.GetComponent<Slider>().value;
            om.GetComponent<OptionsManager>().bgmVolume = bgmVolume;
        }
        else
        {
            bgmVolume = sliderBgm.GetComponent<Slider>().value;
            om.GetComponent<OptionsManager>().bgmVolume = bgmVolume;
            bgmActive = true;
            om.GetComponent<OptionsManager>().bgmActive = true;
        }
    }
    public void OpenMenu()
    {
        settingsPanel.SetActive(true);
        mainMenu.SetActive(false);
        if (bgmVolume == 0) bgmActive = false;
        if (effectsVolume == 0) effectsActive = false;
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsPanel.SetActive(false);
    }

}
