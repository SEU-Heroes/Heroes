using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//摇杆输入
public class JoyInput: MonoBehaviour
{

    List<InputReceiver.joyDir> JInput;

    static JoyInput instance;



    JoyInput() 
    {
        JInput = new List<InputReceiver.joyDir>();
    }


    static public JoyInput getInstance()
    {
        if (instance == null)
        {
            instance = new JoyInput();
        }
        return instance;
    }

    //脚本启用时触发，注册事件
    public void OnEnable()
    {
        EasyJoystick.On_JoystickMove += new EasyJoystick.JoystickMoveHandler(JoyMove);
        EasyJoystick.On_JoystickMoveEnd += new EasyJoystick.JoystickMoveEndHandler(JoyMoveEnd);
    }


    //移动摇杆结束
    void JoyMoveEnd(MovingJoystick move)
    {

        //Movejoystick 当前移动的摇杆名称,和自己命名的摇杆名称对应
        if (move.joystickName == "Movejoystick")
        {
            JInput.Clear();


            GameManager.GetInstance().GetPlayer(GameManager.GetInstance()._controlPlayer).HandDirection(InputReceiver.joyDir.none);
        }
    }

    //移动摇杆中
    void JoyMove(MovingJoystick move)
    {
      //  Debug.Log("111");
        if (move.joystickName != "Movejoystick")
        {
            return;
        }
        //获取摇杆重心偏移坐标
        float joyPositionX = move.joystickAxis.x;
        float joyPositionY = move.joystickAxis.y;
        getDirection(joyPositionX, joyPositionY);
      //  Debug.Log(joyPositionX);
      //  Debug.Log(joyPositionY);
    }

    //移动摇杆中的方向判定
    void getDirection(float x, float y)
    {
        Vector2 oneDirection = new Vector2(1, 0);
        double last1 = x;
        double last2 = System.Math.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        double angleCos = last1 / last2;
        if ((Mathf.Cos(Mathf.PI / 4)) <= angleCos)
        {
            GameManager.GetInstance().GetPlayer(GameManager.GetInstance()._controlPlayer).HandDirection(InputReceiver.joyDir.right);
            
        }
        else if ((Mathf.Cos((Mathf.PI * 3) / 4) <= angleCos) && (angleCos < Mathf.Cos(Mathf.PI / 4)) && (y >= 0))
        {
            GameManager.GetInstance().GetPlayer(GameManager.GetInstance()._controlPlayer).HandDirection(InputReceiver.joyDir.up);
            
        }
        else if ((angleCos < Mathf.Cos((Mathf.PI * 5) / 4)) && (angleCos >= Mathf.Cos(Mathf.PI)))
        {
            GameManager.GetInstance().GetPlayer(GameManager.GetInstance()._controlPlayer).HandDirection(InputReceiver.joyDir.left);
          
        }
        else if ((Mathf.Cos((Mathf.PI * 5) / 4) <= angleCos) && (angleCos < Mathf.Cos((Mathf.PI * 7) / 4)) && y < 0)
        {
            GameManager.GetInstance().GetPlayer(GameManager.GetInstance()._controlPlayer).HandDirection(InputReceiver.joyDir.down);
           
        }
        else
        {
            GameManager.GetInstance().GetPlayer(GameManager.GetInstance()._controlPlayer).HandDirection(InputReceiver.joyDir.none);
           
        }
    }
}

