using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour {

    public float comboTimer;
    public float comboTimerRef;
    public bool comboIsActive;
    public float scoreCombo;
    float bonus;
    public int score;
    int comboCount;
    public int comboCountMax = 0;
    public GameObject bonusScore;
    public GameObject comboGui;
    GameObject gm;
    


	// Use this for initialization
	void Start () {
        gm = GameObject.Find("Manager");
        comboGui.SetActive(false);
        bonusScore.SetActive(false);
        comboTimer = comboTimerRef;
	}
	
	// Update is called once per frame
	void Update () {
		if(!comboIsActive && comboCount > 1)
        {
            StartCombo();
            comboGui.SetActive(true);
        }

        if (comboIsActive)
        {
            comboGui.transform.GetChild(0).gameObject.GetComponent<Text>().text = comboCount.ToString();
            comboTimer -= Time.deltaTime;
            if(comboTimer <= 0)
            {
                BonusScoreCombo();
            }
        }
        else
        {

        }

        if (comboCount == 1)
            comboTimer -= Time.deltaTime;
        if (comboTimer <= 0)
        {
            ResetCombo();
        }


    }

    public void AddScoreCombo(int sc)
    {
        if(comboIsActive) scoreCombo += sc;

    }

    public void AddComboCount()
    {
        comboCount++;
        comboGui.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("up");
        if (comboIsActive)
        {
            comboTimer = comboTimerRef;
        }

    }

    public void ResetCombo()
    {
        
        comboIsActive = false;
        comboTimer = comboTimerRef;
        comboGui.SetActive(false);
    }

    public void BonusScoreCombo()
    {
        if(comboCount > 5 && comboCount <= 13)
        {
            bonus = .20f;
        }
        if (comboCount > 13 && comboCount <= 21)
        {
            bonus = .45f;
        }
        if (comboCount > 21 && comboCount <= 30)
        {
            bonus = .75f;
        }
        if(comboCount > 30)
        {
            bonus = 1f;
        }

        //grava numero maximo de combos 
        if (comboCount > comboCountMax) comboCountMax = comboCount;

        score = (Mathf.RoundToInt(scoreCombo *= bonus));
        StartBonusAnimation();

    }

    public void StartCombo()
    {
        comboIsActive = true;
        comboGui.SetActive(true);
    }

    public void StartBonusAnimation()
    {

        bonusScore.SetActive(true);
        bonusScore.GetComponent<Text>().text = "+" + score.ToString();
        comboCount = 0;
        scoreCombo = 0;
        score = 0;

        bonusScore.GetComponent<Animator>().SetTrigger("start");
    }

    public void GiveBonusToPlayer()
    {
        gm.GetComponent<GameManager>().AddScore(score);
        bonusScore.SetActive(false);
        ResetCombo();
    }
}
