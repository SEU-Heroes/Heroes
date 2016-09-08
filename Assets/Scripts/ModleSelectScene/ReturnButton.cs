/*

* 模式选择返回按钮

* @author 何明胜

* @Time 2016-9-3  16:20:00

*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour {

    //创建按钮
    public Button button;

	// Use this for initialization
	void Start () {
        //监听按钮点击事件
        button.onClick.AddListener(ClickEvent);
	}
	
    //按钮点击事件响应
    private void ClickEvent()
    {
        GameManager.GetInstance().ChangeScene("MainScene");
        //此处添加返回接口
    }

}
