using UnityEngine;
using System.Collections;
/*
 * v1.0  创建时间  2016-9-4   
 * 功能  房间界面返回按钮事件
 * */

public class RoomReturnButton : MonoBehaviour
{

    /// <summary>
    /// 返回按钮
    /// 返回局域网模式下的匹配模式选择界面
    /// 作者：龚健康
    /// </summary>
    public void OnClick()
    {
       // GameManager.GetInstance().ChangeScene("NetWorkView");
        Application.LoadLevel("NetWorkView");
    }
}
