using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_Rank : MonoBehaviour {

    public static DB_Rank instance = null;
    //public List<Player> playerList = new List<Player>();
    List<string> listOfNames = new List<string>();
    List<string> listOfScore = new List<string>();
    List<string> listOfCombo = new List<string>();
    List<string> listOfLevel = new List<string>();
    public Player[] playerList = new Player[6];
    public GameObject panelRank;
    public GameObject rankName;
    public GameObject rankScore;

    public bool makeRank = true;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(this);

        listOfNames.Add("primeiro");
        listOfNames.Add("segundo");
        listOfNames.Add("terceiro");
        listOfNames.Add("quarto");
        listOfNames.Add("quinto");

        listOfScore.Add("p.score");
        listOfScore.Add("s.score");
        listOfScore.Add("t.score");
        listOfScore.Add("q.score");
        listOfScore.Add("qq.scoreo");

        listOfCombo.Add("p.combo");
        listOfCombo.Add("s.combo");
        listOfCombo.Add("t.combo");
        listOfCombo.Add("q.combo");
        listOfCombo.Add("qq.combo");

        listOfLevel.Add("p.level");
        listOfLevel.Add("s.level");
        listOfLevel.Add("t.level");
        listOfLevel.Add("q.level");
        listOfLevel.Add("qq.level");

        LoadList();


    }

    // Use this for initialization
    void Start () {
        BuildRankPanel();
    }

    // Update is called once per frame
    void Update () {
        if (makeRank)
        {
            rankName = GameObject.Find("Name");
            rankScore = GameObject.Find("Score");
            BuildRankPanel();
        }
	}

    public void BuildRankPanel()
    {
        for(int i = 0; i < rankName.transform.childCount; i++)
        {
            rankName.transform.GetChild(i).gameObject.GetComponent<Text>().text = playerList[i].Nome;
            rankScore.transform.GetChild(i).gameObject.GetComponent<Text>().text = playerList[i].Score.ToString();
        }
        makeRank = false;

    }

    public Player ArrangePlayerRank()
    {
        Player pl = new Player();

        //ordena maior score
        for (int i = 1; i < playerList.Length; i++)
        {
            for (int j = 0; j < playerList.Length - i; j++)
            {
                if (playerList[j].Score < playerList[j + 1].Score)
                {
                    pl = playerList[j];
                    playerList[j] = playerList[j + 1];
                    playerList[j + 1] = pl;
                }
            }
        }

        //atribui a posição
       for (int i = 1; i < playerList.Length; i++)
        {
            playerList[i-1].Position = i;
        }

        return null;

    }

    public bool SaveList()
    {
        for(int i = 0; i < listOfNames.Count; i++)
        {
            if (i >= playerList.Length) return true;
            PlayerPrefs.SetString(listOfNames[i],playerList[i].Nome);
            PlayerPrefs.SetInt(listOfScore[i], playerList[i].Score);
            PlayerPrefs.SetInt(listOfCombo[i], playerList[i].NumberMaxCombo);
            PlayerPrefs.SetInt(listOfLevel[i], playerList[i].MaxLevel);
        }
        return true;
    }

    public bool LoadList()
    {
        for(int i = 0; i < listOfNames.Count; i++)
        {
            playerList[i] = new Player(PlayerPrefs.GetString(listOfNames[i]), PlayerPrefs.GetInt(listOfCombo[i]), PlayerPrefs.GetInt(listOfLevel[i]), PlayerPrefs.GetInt(listOfScore[i]));
            playerList[i].Position = i + 1;
        }
        //atribui a posição
        for (int i = 1; i < playerList.Length; i++)
        {
            playerList[i - 1].Position = i;
        }

        /*for (int i = 0; i < playerList.Length - 1; i++)
        {
            print(playerList[i].Position.ToString());
            print(playerList[i].Nome);
            print(playerList[i].Score.ToString());
        }*/
        return true;
    }
}
