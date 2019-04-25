using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

    
    public float speed;
    public int hp;
    public int score;
    public int coins;
    public bool ready = false;
    bool active = true;
    public bool isRainbow;// explode todos os balões da cor escolhida
    public bool isBomb;// bomba que atrapalha o jogador
    public bool isTnt; // skill
    public bool isNormal;
    bool colorControl = false;
    int hpAux;
    public int cost;
    GameObject gm;
    GameObject mc;
    GameObject wt;
    Animator anim;
    Vector2 movement;

   


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        gm = GameObject.Find("Manager");
        mc = GameObject.Find("Main Camera");
        wt = GameObject.Find("Watcher");
	}
	
	// Update is called once per frame
	void Update () {

        if (isTnt || isBomb)
        {

        }
        else
        {
            if (hp == 3)
            {
                GetComponent<SpriteRenderer>().color = new Color32(0, 184, 255, 255);
            }
            else if (hp == 2)
            {
                GetComponent<SpriteRenderer>().color = new Color32(9, 255, 0, 255);
            }
            else if (hp == 1)
            {
                GetComponent<SpriteRenderer>().color = new Color32(255, 0, 237, 255);
            }
        }
        
        if (isRainbow)
        {
            if (gm.GetComponent<GameManager>().gameState.Equals("play"))
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                if (!colorControl)
                {
                    StartCoroutine(ChangeColor());
                }
                if (hp <= 0)
                {
                    speed = 0;
                    DestroySameColor();
                    PopBalloon();
                   

                }
            }
        }
        else if (isTnt || isBomb)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            if (hp <= 0)
            {
                speed = 0;
                PopBalloon();
            }
        }
        else
        {
            if (gm.GetComponent<GameManager>().gameState.Equals("play"))
            {
                //velocidade incrementa conforme level
                transform.Translate(Vector2.up * speed * gm.GetComponent<GameManager>().levelSpeed * Time.deltaTime);

                if (hp <= 0)
                {
                    speed = 0;
                    PopBalloon();
                   
                }
            }
        }
    }

    public void PopBalloon()
    {
        anim.SetTrigger("pop");
        
    }

    public void DestroyBalloon()
    {
        if (active)
        {
            gm.SendMessage("AddScore", score);
            if(wt.GetComponent<Combo>().comboIsActive) gm.SendMessage("AddCoins", coins);
            active = false;
            wt.SendMessage("AddScoreCombo", score);
        }
        mc.SendMessage("PlayPopSound");
        Destroy(this.gameObject);
    }

    void DestroySameColor()
    {
        GameObject[] balloons = GameObject.FindGameObjectsWithTag("balloon");
        foreach(GameObject bl in balloons)
        {
            if(bl.GetComponent<Balloon>().hp == hpAux)
            {
                bl.GetComponent<Balloon>().hp = 0;
            }
        }
    }

    void TntExplosion()
    {
        //TO DO
        //explosao dos blocos
        Instantiate(gm.GetComponent<GameManager>().tntExplosion, transform.position, transform.rotation);
        StartCoroutine(TntTime());
    }

    void BombExplosion()
    {
        //TO DO
        //exposao e criaçao dos blocos
        Instantiate(gm.GetComponent<GameManager>().brick, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRainbow)
        {
            if (collision.gameObject.tag == "pop" && ready && collision.gameObject.GetComponent<Hit>().isActive)
            {
                Destroy(collision.gameObject);
                DestroySameColor();
                hp = 0;
                gm.GetComponent<SkillManager>().skillActive = false;
            }
        }
        else if (isTnt)
        {
            if (collision.gameObject.tag == "pop" && ready && collision.gameObject.GetComponent<Hit>().isActive)
            {
                Destroy(collision.gameObject);
                TntExplosion();
                hp = 0;
                gm.GetComponent<SkillManager>().skillActive = false;
            }
            if (collision.gameObject.tag == "brick" && ready)
            {
                TntExplosion();
                hp = 0;
                gm.GetComponent<SkillManager>().skillActive = false;
            }
        }
        else if (isBomb)
        {
            if (collision.gameObject.tag == "pop" && ready && collision.gameObject.GetComponent<Hit>().isActive)
            {
                Destroy(collision.gameObject);
                BombExplosion();
                hp = 0;
            }
        }
        else if (isNormal)
        {
            if (collision.gameObject.tag == "pop" && ready && collision.gameObject.GetComponent<Hit>().isActive)
            {
                GameObject hit = GameObject.FindGameObjectWithTag("pop");
                Instantiate(gm.GetComponent<GameManager>().star, hit.transform.position, hit.transform.rotation);
                Destroy(collision.gameObject);
                hp -= 1;
                if (hp == 0) wt.SendMessage("AddComboCount");
            }

            if ((collision.gameObject.tag == "wall" || collision.gameObject.tag == "brick") && ready)
            {
                hp = 0;
                if (active)
                {
                    //Instantiate(gm.GetComponent<GameManager>().brick, transform.position, transform.rotation);
                    Instantiate(gm.GetComponent<GameManager>().brick, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), transform.rotation);
                    active = false;
                }
            }
        }
        else
        {

        }
    }

    void OnBecameInvisible()
    {
        // perde uma chance quando bala sai da tela
        if (active)
        {
            active = false;
        }

        DestroyBalloon();
    }

    void OnBecameVisible()
    {
        ready = true;
    }

    IEnumerator ChangeColor()
    {
        hpAux = hp;
        colorControl = true;
        yield return new WaitForSeconds(1f);
        hp += 1;
        if (hp > 3) hp = 1;
        colorControl = false;
    }

    IEnumerator TntTime()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject obj = GameObject.FindGameObjectWithTag("tnt");
        Destroy(obj.gameObject);
    }

    
}
