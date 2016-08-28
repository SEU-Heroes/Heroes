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

    public bool startInput(Vector3 point)
    {
        endPoint = point;

        if (isInputing)
        {
            input.Clear();
            return false;
        }
        else
        {
            isInputing = true;
            return true;
        }
    }

    public bool finishInput()
    {
        if (isInputing)
        {
          
           GameManager.getInstance().getMainPlayer().handInput(input);
            isInputing = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void handInput(Vector3 point)
    {
        startPoint = endPoint;
        endPoint = point;
        nowDirection = endPoint - startPoint;
        if (!lastDirection.Equals(new Vector3(0, 0, 0)))
        {
            if (!checkDirection())
            {
                input.Add(getDirection());
            }
        }
        if (nowDirection.Equals(new Vector3(0, 0, 0)))
        {
            stayCount++;
            if (stayCount > maxStayCount)
            {
                stayCount = 0;
                handStay();
            }
        }
        else
        {
            lastDirection = nowDirection;
        }
    }

    void handStay()
    {
       GameManager.getInstance().getMainPlayer().touchStay(input);
    }

    bool checkDirection()
    {
       double last1 = nowDirection.x * lastDirection.x + nowDirection.y * lastDirection.y + nowDirection.z * lastDirection.z;
       double last2 = System.Math.Sqrt(Math.Pow(nowDirection.x, 2) + Math.Pow(nowDirection.y, 2) + Math.Pow(nowDirection.z, 2))
                        * System.Math.Sqrt(Math.Pow(lastDirection.x, 2) + Math.Pow(lastDirection.y, 2) + Math.Pow(lastDirection.z, 2));
        if ((last1 / last2) > Math.Cos(22.5))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    dir getDirection()
    {
        oneDirection = new Vector3(1, 0, 0);
     double last1 = nowDirection.x * oneDirection.x + nowDirection.y * oneDirection.y + nowDirection.z * oneDirection.z;
      double last2 = System.Math.Sqrt(Math.Pow(nowDirection.x, 2) + Math.Pow(nowDirection.y, 2) + Math.Pow(nowDirection.z, 2))
                        * System.Math.Sqrt(Math.Pow(oneDirection.x, 2) + Math.Pow(oneDirection.y, 2) + Math.Pow(oneDirection.z, 2));
        if ((last1 / last2)>=Math.Cos(Math.PI/8)) 
        {
           
            return dir.right;

        }
        else if (((last1 / last2)>=Math.Cos((Math.PI*3)/8)) && ((last1 / last2) < Math.Cos(Math.PI/8))&&(nowDirection.y>=0))
        {
           
            return dir.upAright;
        }
        else if (((last1 / last2)>=Math.Cos((Math.PI*5)/8))&& ((last1 / last2) < Math.Cos((Math.PI*3)/8))&&(nowDirection.y>=0))
        {
            
            return dir.up;
        }
        else if (((last1 / last2) >=Math.Cos((Math.PI*7)/8)) && ((last1 / last2) < Math.Cos((Math.PI*5)/8))&&(nowDirection.y<=0))
        {
          
            return dir.upAleft;
        }
        else if (((last1 / last2) >=Math.Cos(Math.PI)) && ((last1 / last2) < Math.Cos((Math.PI*7)/8)))
        {
          
            return dir.left;
        }
        else if (((last1 / last2) >=Math.Cos((Math.PI*9)/8)) && ((last1 / last2) < Math.Cos((Math.PI*11)/8))&&(nowDirection.y<=0))
        {
           
            return dir.downAleft;
        }
        else if (((last1 / last2) >=Math.Cos((Math.PI*11)/8)) && ((last1 / last2) < Math.Cos((Math.PI*13)/8))&&(nowDirection.y<=0))
        {
           
            return dir.down;
        }
        else
        {
            
            return dir.downAright;
        }
    }
}