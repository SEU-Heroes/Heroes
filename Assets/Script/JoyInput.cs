using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//摇杆输入
public class JoyInput: MonoBehaviour
{
    public enum joyDir { up, down, left, right, none };

    List<joyDir> JInput;

    static JoyInput instance;



    JoyInput() 
    {
        JInput = new List<joyDir>();
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
    void Update() 
    { 
    
    }

    //移动摇杆结束
    void JoyMoveEnd(MovingJoystick move)
    {

        //Movejoystick 当前移动的摇杆名称,和自己命名的摇杆名称对应
        if (move.joystickName == "Movejoystick")
        {
            JInput.Clear();
            JInput.Add(joyDir.none);

          // GameManager.getInstance().getMainPlayer().getDirection(JInput);
        }
    }
    //移动摇杆中
    void JoyMove(MovingJoystick move)
    {
        Debug.Log("yaogan");
        if (move.joystickName != "Movejoystick")
        {
            return;
        }
        //获取摇杆重心偏移坐标
        float joyPositionX = move.joystickAxis.x;
        float joyPositionY = move.joystickAxis.y;
        Debug.Log("yaogan");
        //JInput.Add(getDirection(joyPositionX, joyPositionY));
       // GameManager.getInstance().getMainPlayer().SetBool("normalAttack",true);
    // GameManager.getInstance().getMainPlayer().getDirection(JInput);

    }
    //移动摇杆中的方向判定
    joyDir getDirection(float x, float y)
    {
        Vector2 oneDirection = new Vector2(1, 0);
        double last1 = x;
        double last2 = System.Math.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        double angleCos = last1 / last2;
        if ((Mathf.Cos(Mathf.PI / 4)) <= angleCos)
        {
            return joyDir.right;
        }
        else if ((Mathf.Cos((Mathf.PI * 3) / 4) <= angleCos) && (angleCos < Mathf.Cos(Mathf.PI / 4)) && (y >= 0))
        {
            return joyDir.up;
        }
        else if ((angleCos < Mathf.Cos((Mathf.PI * 5) / 4)) && (angleCos >= Mathf.Cos(Mathf.PI)))
        {
            return joyDir.left;
        }
        else if ((Mathf.Cos((Mathf.PI * 5) / 4) <= angleCos) && (angleCos < Mathf.Cos((Mathf.PI * 7) / 4)) && y < 0)
        {
            return joyDir.down;
        }
        else
        {
            return joyDir.none;
        }
    }
}

