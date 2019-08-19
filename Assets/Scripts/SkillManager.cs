using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour {

    public GameObject centerSpot;
    public GameObject rainbowBalloon;
    public GameObject tntBalloon;
    public GameObject rainbowLabel;
    public GameObject tntLabel;
    public GameObject cloneRainbow;
    public GameObject cloneTnt;

    int raindowCost;
    int tntCost;
    int skillSelect;
    public bool skillActive = false;
    public bool dragging = false;

    public GameObject debug;

    GameObject gm;
    GameObject canvas;

    GameObject _clone;

    // Use this for initialization
    void Start () {
        gm = GameObject.Find("Manager");
        canvas = GameObject.Find("Canvas");
        raindowCost = rainbowBalloon.GetComponent<Balloon>().cost;
        tntCost = tntBalloon.GetComponent<Balloon>().cost;
    }
	
	// Update is called once per frame
	void Update () {
        if (gm.GetComponent<GameManager>().isKids)
        {

        }
        else
        {
            rainbowLabel.GetComponent<Text>().text = raindowCost.ToString();
            tntLabel.GetComponent<Text>().text = tntCost.ToString();
        }


        if (dragging)
        {
            _clone.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _clone.transform.position.z);
        }
        else
        {
            //Destroy(_clone.gameObject);
        }

    }

    public void SkillRainbow()
    {
        //se o jogador tiver acumulado moedas utiliza a skill
        if(gm.GetComponent<GameManager>().GetCoins() >= raindowCost && !skillActive)
        {
            Instantiate(rainbowBalloon, centerSpot.transform.position, centerSpot.transform.rotation);
            gm.SendMessage("RemoveCoins", raindowCost);
            raindowCost += 3;
            skillActive = true;
        }
        else
        {
            gm.SendMessage("NoCoins");
        }
        
    }

    public void SkillTnt()
    {
        //se o jogador tiver acumulado moedas utiliza a skill
        if (gm.GetComponent<GameManager>().GetCoins() >= tntCost && !skillActive)
        {
            Instantiate(tntBalloon, centerSpot.transform.position, centerSpot.transform.rotation);
            gm.SendMessage("RemoveCoins", tntCost);
            tntCost += 5;
            skillActive = true;
        }
        else
        {
            gm.SendMessage("NoCoins");
        }
    }

    public void OnDrag(int select)
    {
        skillSelect = select;
        if (!dragging)
        {
            if(select == 0) // skill rainbow
            {
                _clone = Instantiate(cloneRainbow, Input.mousePosition, transform.rotation);
            }
            if(select == 1) // skill tnt
            {
                _clone = Instantiate(cloneTnt, Input.mousePosition, transform.rotation);
            }
            //_clone.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            _clone.transform.SetParent(canvas.transform);
            dragging = true;
        }

    }

    public void OnDrop()
    {
        dragging = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector3.Distance(centerSpot.transform.position,mousePos);
        Destroy(_clone.gameObject);
        debug.GetComponent<Text>().text = distance.ToString();
        if (distance <= 12)
        {
            switch (skillSelect)
            {
                case 0:
                    //se o jogador tiver acumulado moedas utiliza a skill
                    if (gm.GetComponent<GameManager>().GetCoins() >= raindowCost && !skillActive)
                    {
                        Vector3 skillSpot = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Instantiate(rainbowBalloon, new Vector3(skillSpot.x, skillSpot.y, centerSpot.transform.position.z), centerSpot.transform.rotation);
                        gm.SendMessage("RemoveCoins", raindowCost);
                        raindowCost += 3;
                        skillActive = true;
                    }
                    else
                    {
                        gm.SendMessage("NoCoins");
                    }
                    break;
                case 1:
                    //se o jogador tiver acumulado moedas utiliza a skill
                    if (gm.GetComponent<GameManager>().GetCoins() >= tntCost && !skillActive)
                    {
                        Vector3 skillSpot = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Instantiate(tntBalloon, new Vector3(skillSpot.x, skillSpot.y, centerSpot.transform.position.z), centerSpot.transform.rotation);
                        gm.SendMessage("RemoveCoins", tntCost);
                        tntCost += 5;
                        skillActive = true;
                    }
                    else
                    {
                        gm.SendMessage("NoCoins");
                    }
                    break;

            }

        }
        

    }
}
