using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour {

    public GameObject leftSpot;
    public GameObject centerSpot;
    public GameObject rightSpot;
    public GameObject rainbowBalloon;
    public GameObject tntBalloon;
    public GameObject rainbowLabel;
    public GameObject tntLabel;
    int raindowCost;
    int tntCost;

    GameObject gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("Manager");
        raindowCost = rainbowBalloon.GetComponent<Balloon>().cost;
        tntCost = tntBalloon.GetComponent<Balloon>().cost;
    }
	
	// Update is called once per frame
	void Update () {
        rainbowLabel.GetComponent<Text>().text = raindowCost.ToString();
        tntLabel.GetComponent<Text>().text = tntCost.ToString();

    }

    public void SkillRainbow()
    {
        //se o jogador tiver acumulado moedas utiliza a skill
        if(gm.GetComponent<GameManager>().GetCoins() >= raindowCost)
        {
            Instantiate(rainbowBalloon, centerSpot.transform.position, centerSpot.transform.rotation);
            gm.SendMessage("RemoveCoins", raindowCost);
            raindowCost += 3;
        }
        else
        {
            gm.SendMessage("NoCoins");
        }
        
    }

    public void SkillTnt()
    {
        //se o jogador tiver acumulado moedas utiliza a skill
        if (gm.GetComponent<GameManager>().GetCoins() >= tntCost)
        {
            Instantiate(tntBalloon, centerSpot.transform.position, centerSpot.transform.rotation);
            gm.SendMessage("RemoveCoins", tntCost);
            tntCost += 5;
        }
        else
        {
            gm.SendMessage("NoCoins");
        }
    }
}
