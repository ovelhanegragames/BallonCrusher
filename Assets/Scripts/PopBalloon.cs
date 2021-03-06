﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBalloon : MonoBehaviour {

    public GameObject pop;
    Vector2 mousePosition;
    GameObject gm;
    GameObject st;
    GameObject wt;
    GameObject om;
    int indice;
    AudioClip popSound;
    float volume;

    Ray ray;
    RaycastHit hit;

    //public GameObject star;

    // Use this for initialization
    void Start () {
        gm = GameObject.Find("Manager");
        st = GameObject.Find("Settings");
        wt = GameObject.Find("Watcher");
        om = GameObject.Find("OptionsManager");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gm.GetComponent<GameManager>().starBlock = false;
        }
        if (gm.GetComponent<GameManager>().gameState.Equals("play"))
        {
            if (Input.GetMouseButton(0) && gm.GetComponent<GameManager>().isKids)
            {
                ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "enemy")
                    { 
                        hit.transform.parent.gameObject.GetComponent<Balloon>().CheckBallon(hit.transform.parent.gameObject);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "enemy")
                    {
                        hit.transform.parent.gameObject.GetComponent<Balloon>().CheckBallon(hit.transform.parent.gameObject);
                    }
                }
                else
                {
                    wt.GetComponent<Combo>().BonusScoreCombo();
                }
            }

        }

        //volume = st.GetComponent<Settings>().effectsVolume;
        volume = om.GetComponent<OptionsManager>().effectsVolume;

	}

    public void PlayPopSound()
    {
        //indice = Mathf.RoundToInt(Random.Range(0, gm.GetComponent<GameManager>().listOfPopSounds.Count));
        //popSound = gm.GetComponent<GameManager>().listOfPopSounds[indice];
        popSound = om.GetComponent<OptionsManager>().popSound;
        GetComponent<AudioSource>().PlayOneShot(popSound,volume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.tag == "balloon")
        //{

        //}
        //else
        //{
        //    wt.SendMessage("BonusScoreCombo");
        //}
    }

}
