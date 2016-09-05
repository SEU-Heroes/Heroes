using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//轨迹输入
class InputReceiver
{
    public enum dir { up, down, left, right, upAright, upAleft, downAleft, downAright };
    public enum joyDir { up, down, left, right, none };

    bool isInputing = false;

    List<dir> input;

    Vector3 startPoint, endPoint, lastDirection, nowDirection, oneDirection;

    static InputReceiver instance;

    int stayCount = 0;

    const int maxStayCount = 5;

    InputReceiver()
    {
        input = new List<dir>();
    }

    static public InputReceiver getInstance()
    {
        if (instance == null)
        {
            instance = new InputReceiver();
        }
        return instance;
    }

}