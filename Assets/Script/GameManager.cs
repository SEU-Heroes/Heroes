using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class GameManager{

    //Attr
    public int distanceStander;

    public enum mode {none, player, computer };

    mode nowMode = mode.none;

    static GameManager instance;

    Player player1, player2;

    static public HeroFactory factory;

    GameManager()
    {
        factory = Camera.main.GetComponent<HeroFactory>();
    }

    static public GameManager getInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    public GameObject Instantiate(GameObject go, Vector3 position, Quaternion q)
    {
        return (GameObject)Camera.Instantiate(go, position, q);
    }

    public Player getMainPlayer()
    {
        return player1;
    }

    public Player getOtherPlayer()
    {
        return player2;
    }

    public void setMainPlayer(Player p)
    {
        player1 = p;
    }

    public void setOtherPlayer(Player p)
    {
        player2 = p;
    }

    public void setMode(mode m)
    {
        nowMode = m;
    }
}
