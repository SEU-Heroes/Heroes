using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    // 单例模式
    private static GameController _instance;
    private GameController()
    {
        roundNumber = 1;
    }

    public static GameController GetInstance()
    {
        return _instance;
    }

    public int roundNumber;

	// Use this for initialization
	void Awake () 
    {
        _instance = this;
        roundNumber = 1;
	}

}
