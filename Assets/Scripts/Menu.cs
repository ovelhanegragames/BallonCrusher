using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class Menu : MonoBehaviour {

    public GameObject rankPanel;
    public GameObject rankName;
    public GameObject rankScore;
    public GameObject playerStatistics;

    // Use this for initialization
    void Start () {
        rankPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseApp()
    {
        Application.Quit();
    }

    public void PlayTheGame()
    {
        SceneManager.LoadScene("play");
    }

    public void OpenRank()
    {
        if (LoginManager.LoginData.playIsLogin)
        {
            rankPanel.SetActive(true);
            RequestLeaderboard();
        }
        else
        {
            //TODO
            //Aviso para informar que sem login não acessa ranking
        }
    }

    public void PlayKids()
    {
        SceneManager.LoadScene("Kids");
    }

    public void RequestLeaderboard()
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest();
        request.StatisticName = "Score";
        request.StartPosition = 0;
        request.MaxResultsCount = 5;
        PlayFabClientAPI.GetLeaderboard(request, this.ShowLeaderboard, this.OnError);
    }

    public void ShowLeaderboard(GetLeaderboardResult result)
    {
        GetPlayerPositionOnLeadbord();
        int i = 0;
        foreach (var s in result.Leaderboard)
        {
            Debug.Log("Player: " + s.PlayFabId + " " + "Name: " + s.DisplayName + " " + "Position: " + s.Position + " " + " value: " + s.StatValue);
            rankName.transform.GetChild(i).gameObject.GetComponent<Text>().text = s.DisplayName;
            rankScore.transform.GetChild(i).gameObject.GetComponent<Text>().text = s.StatValue.ToString();
            i++;
        }
    }

    //pega a posiçao do player logado
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
        foreach (var s in result.Leaderboard)
        {
            Debug.Log(s.PlayFabId + "  " + s.Position + "  " + s.StatValue);
            playerStatistics.GetComponent<Text>().text = s.DisplayName;
            playerStatistics.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                "Position:........" + (s.Position + 1) + "\n" +
                "Score:..........." + s.StatValue + "\n";
        }
        //RequestTotalLeaderBoard();
    }

    public void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }

    //private void RequestTotalLeaderBoard()
    //{
    //    GetLeaderboardRequest request = new GetLeaderboardRequest();
    //    request.StatisticName = "Score";
    //    request.StartPosition = 0;
    //    PlayFabClientAPI.GetLeaderboard(request, this.ShowLeaderboardTotal, this.OnError);
    //}

    //public void ShowLeaderboardTotal(GetLeaderboardResult result)
    //{
    //    Debug.LogError(result.Leaderboard.Count);
    //}
}