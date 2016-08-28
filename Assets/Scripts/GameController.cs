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
        if (_instance == null)
            _instance = new GameController();
        return _instance;
    }

    public int roundNumber;

	// Use this for initialization
	void Awake () 
    {
        roundNumber = 1;
	}

}
