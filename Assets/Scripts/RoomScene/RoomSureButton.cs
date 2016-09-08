using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
 * v1.0  创建时间 2016-9-4
 * 功能  确定按钮的点击事件以及房间号的显示
 * */
public class RoomSureButton : MonoBehaviour {
    Text text;
   private int _RoomNumber=0;
   void Update() { 
       //调用接口，将服务器创建的房间号赋值给_RoomNumber;
       //以_RoomNumber为实参，调用RoomNumberShow函数
       RoomNumberShow(_RoomNumber);
       //调用接口当房主进入时与玩家进入时加载资源
       
   }
    public void RoomSureClick() {
        //调用接口，判断房间是否有两个玩家，如果没有两个玩家，则点击确定无响应
        //若房间中有两个玩家，则点击确定能进入角色选择界面
        //GameManager.GetInstance().ChangeScene("HeroSelctView");
        Application.LoadLevel("HeroSelctView");
    }
    public void RoomNumberShow(int number) {
        text.text = number.ToString();
    }
}
