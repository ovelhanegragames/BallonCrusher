using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour {

    public AudioClip popSound;
    public AudioClip bgm;
    public float bgmVolume;
    public float effectsVolume;
    public bool bgmActive;
    public bool effectsActive;

    public static OptionsManager instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
