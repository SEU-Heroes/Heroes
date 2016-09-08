using UnityEngine;
using System.Collections;

public class Mode_control : MonoBehaviour {
    public string last_scene;//记录本场景的上一个场景
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 返回按钮，返回自己的上一个场景
    /// </summary>
    /// 作者：焦暄雅
    public void ClickReturn()
    {
        Application.LoadLevel("NumberView");
    }

    /// <summary>
    /// 选择自由匹配，进入下一个场景，并传一个参数
    /// </summary>
    /// 作者：焦暄雅
    public void ClickFree()
    {
        Application.LoadLevel("");
    }

    /// <summary>
    /// 选择创建房间，进入下一个场景，并传一个参数
    /// </summary>
    /// 作者：焦暄雅
    public void ClickCreate()
    {
        Application.LoadLevel("RoomView");
    }

    /// <summary>
    /// 选择搜索房间，进入下一个场景，并传一个参数
    /// </summary>
    /// 作者：焦暄雅
    public void ClickFind()
    {
        Application.LoadLevel("");
    }
}
