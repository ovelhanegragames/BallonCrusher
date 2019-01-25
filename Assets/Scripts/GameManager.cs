using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<GameObject> listOfBalloons = new List<GameObject>();
    public List<GameObject> listOfGenerators = new List<GameObject>();
    public List<AudioClip> listOfPopSounds = new List<AudioClip>();
    public GameObject scoreLabel;
    public GameObject levelLabel;
    public GameObject coinsLabel;
    public GameObject brick;
    public GameObject gameOver;
    public GameObject tntExplosion;
    int score = 0;
    int coins = 0;
    int endOfTheGame = 16;
    int numberOffBalloons;
    public float levelSpeed = 1;
    public float levelSleep = 0;
    public int level = 0;
    public float sleep = 4;
    bool ready = true;
    public string gameState = "play";

	// Use this for initialization
	void Start () {
        numberOffBalloons = endOfTheGame;
        gameOver.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        scoreLabel.GetComponent<Text>().text = "SCORE: " + score;
        levelLabel.GetComponent<Text>().text = "LEVEL: " + level;
        coinsLabel.GetComponent<Text>().text = "COINS: " + coins;

        if (coins <= 0) coins = 0;

        if (gameState.Equals("play"))
        {
            //verifica final de level e prepara para o proximo level
            if (endOfTheGame <= 0)
            {
                levelSpeed += .2f;
                levelSleep += .15f;
                if(levelSleep < 2)
                {
                    levelSleep = 2;
                }
                level += 1;
                endOfTheGame = (numberOffBalloons + 6);
                numberOffBalloons = endOfTheGame;
            }

            //escolhe um gerador
            if (ready)
            {
                loop:
                int indice = Mathf.RoundToInt(Random.Range(0, listOfGenerators.Count));
                if (listOfGenerators[indice].GetComponent<Generator>().ready)
                {
                    listOfGenerators[indice].SendMessage("MakeBalloon");
                    PopBalloon();
                }
                else
                {
                    goto loop;
                }
                StartCoroutine(SleepTime());

            }
        }
        else if (gameState.Equals("gameover"))
        {
            gameOver.SetActive(true);
        }
	}

    public void AddScore(int sc)
    {
        score += sc;
    }

    public void AddCoins(int c)
    {
        coins += c;
    }

    public void RemoveCoins(int c)
    {
        coins -= c;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void NoCoins()
    {
        coinsLabel.GetComponent<Animator>().SetTrigger("blink");
    }

    public void PopBalloon()
    {
        endOfTheGame -= 1;
    }

    public void RestarLevel()
    {
        SceneManager.LoadScene("play");
    }

    IEnumerator SleepTime()
    {
        ready = false;
        yield return new WaitForSeconds(sleep - levelSleep);
        ready = true;
    }
}
