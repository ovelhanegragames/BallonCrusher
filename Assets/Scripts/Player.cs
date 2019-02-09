using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player { 

    string nome;
    int numberMaxCombo;
    float hitsPercent;
    string note;
    int maxLevel;
    int score;
    int position;

    public string Nome
    {
        get
        {
            return nome;
        }

        set
        {
            nome = value;
        }
    }

    public int NumberMaxCombo
    {
        get
        {
            return numberMaxCombo;
        }

        set
        {
            numberMaxCombo = value;
        }
    }

    public float HitsPercent
    {
        get
        {
            return hitsPercent;
        }

        set
        {
            hitsPercent = value;
        }
    }

    public string Note
    {
        get
        {
            return note;
        }

        set
        {
            note = value;
        }
    }

    public int MaxLevel
    {
        get
        {
            return maxLevel;
        }

        set
        {
            maxLevel = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }

    public Player(string nm, int nmc, int ml, int sc)
    {
        this.nome = nm;
        this.numberMaxCombo = nmc;
        this.maxLevel = ml;
        this.score = sc;
    }

    public Player()
    {

    }

}