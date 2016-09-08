/*

* 设置界面的返回按钮

* @author 何明胜

* @Time 2016-9-1  9:34:00

*/

using UnityEngine;
using System.Collections;

public class ReturnBtn : MonoBehaviour {

	
    public void OnClick()
    {
       GameManager.GetInstance().ChangeScene("MainScene");
    }

}
