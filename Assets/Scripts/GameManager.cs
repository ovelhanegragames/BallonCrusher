using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : MonoBehaviour {

    public List<GameObject> listOfBalloons = new List<GameObject>();
    public List<GameObject> listOfGenerators = new List<GameObject>();
    public GameObject playerStatistics;
    public GameObject scoreLabel;
    public GameObject levelLabel;
    public GameObject coinsLabel;
    public GameObject brick;
    public GameObject gameOver;
    public GameObject tntExplosion;
    public GameObject bombBalloon;
    public GameObject star;
    public int score = 0;
    int coins = 0;
    public int endOfTheGame = 16;
    int numberOffBalloons;
    public float levelSpeed = 1;
    public float levelSleep = 0;
    public int level = 0;
    public float sleep = 4;
    bool ready = true;
    public string gameState = "play";
    int bombBalloonCount = 0;
    public int bombBalloonRef;
    public bool isKids;
    public bool starBlock = false;

	// Use this for initialization
	void Start () {
        gameOver.transform.GetChild(2).gameObject.SetActive(false);
        gameOver.transform.GetChild(4).gameObject.SetActive(false);
        playerStatistics.SetActive(false);
        numberOffBalloons = endOfTheGame;
        if (isKids)
        {

        }
        else
        {
            gameOver.SetActive(false);
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (isKids)
        {

        }
        else
        {
            scoreLabel.GetComponent<Text>().text = "SCORE: " + score;
            levelLabel.GetComponent<Text>().text = "LEVEL: " + level;
            coinsLabel.GetComponent<Text>().text = "COINS: " + coins;
        }


        if (coins <= 0) coins = 0;

        if (gameState.Equals("play"))
        {
            //verifica final de level e prepara para o proximo level
            if (endOfTheGame <= 0)
            {
                LevelUp();
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
                    if (isKids)
                    {

                    }
                    else
                    {
                        MakeBombBalloon();
                    }
                    
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
            gameState = "waiting";
            gameOver.SetActive(true);
            SendScoreToRanking();
        }
	}

    private void LevelUp()
    {
        level += 1;
        levelSpeed += .35f;
        if (level > 1)
        {
            levelSleep += .2f;
        }

        if (levelSleep > 2)
        {
            levelSleep = 2;
        }


        endOfTheGame = (numberOffBalloons + 5);
        numberOffBalloons = endOfTheGame;
    }
    //seleciona um gerador para criar um bombBalloon a cada @14 baloes gerados.
    void MakeBombBalloon()
    {
        bombBalloonCount++;
        if(bombBalloonCount >= bombBalloonRef)
        {
            bombBalloonCount = 0;
            GameObject gn = listOfGenerators[Mathf.RoundToInt(Random.Range(0, listOfGenerators.Count))];
            gn.GetComponent<Generator>().ready = true;
            Instantiate(bombBalloon, gn.transform.position, gn.transform.rotation);
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

    public void SendScoreToRanking()
    {
        if (LoginManager.LoginData.playIsLogin)
        {
            List<StatisticUpdate> statistic = new List<StatisticUpdate>();
            statistic.Add(new StatisticUpdate()
            {
                StatisticName = "Score",
                Value = score
            });

            UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest();
            request.Statistics = statistic;
            PlayFabClientAPI.UpdatePlayerStatistics(request, this.DataUpdateSuccess, this.OnError);
        }
        else
        {
            ShowLocalStatistics();
        }
    }

    private void DataUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Success");
        GetPlayerPositionOnLeadbord();
    }

    public void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }

    public void GetPlayerPositionOnLeadbord()
    {
        GetLeaderboardAroundPlayerRequest request = new GetLeaderboardAroundPlayerRequest();
        request.PlayFabId = LoginManager.LoginData.playfabId;
        request.StatisticName = "Score";
        request.MaxResultsCount = 1;
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, this.OnPlayerLeaderBoardResult, this.OnError);
    }

    public void OnPlayerLeaderBoardResult(GetLeaderboardAroundPlayerResult result)
    {
        playerStatistics.SetActive(true);
        foreach (var s in result.Leaderboard)
        {
            playerStatistics.GetComponent<Text>().text = s.DisplayName;
            playerStatistics.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                "Position:........" + (s.Position + 1) + "\n" +
                "Score:..........." + s.StatValue + "\n" +
                "Level:..........." + level;
        }
        gameOver.transform.GetChild(2).gameObject.SetActive(true);
        gameOver.transform.GetChild(4).gameObject.SetActive(true);
    }

    private void ShowLocalStatistics()
    {
        playerStatistics.GetComponent<Text>().text = "";
        playerStatistics.transform.GetChild(0).gameObject.GetComponent<Text>().text =
            "Score:..........." + score + "\n" +
            "Level:..........." + level;

        gameOver.transform.GetChild(2).gameObject.SetActive(true);
        gameOver.transform.GetChild(4).gameObject.SetActive(true);
    }
}
