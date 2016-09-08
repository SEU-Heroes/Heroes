using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeButton : MonoBehaviour
{

    public Text TimeShowText;
    public string[] times = { "60s", "90s", "120s", "无限" };
    public int _Timenum = 0;
    public int _TimeNumber = 60;
    static public TimeButton _instance;

    void Start()
    {
        _instance = this;
    }

    public void LeftClick()
    {
        _Timenum = (_Timenum + 3) % 4;
        TimeShowText.text = times[_Timenum];
        if(_Timenum==0){
            _TimeNumber = 60;
        }
        else if(_Timenum==1){
        _TimeNumber=90;
        }
        else if(_Timenum==2){
            _TimeNumber = 120;
        }
        else{
        _TimeNumber=-1;
        }

    }
    public void RightClick()
    {
        _Timenum = (_Timenum + 1) % 4;
        TimeShowText.text = times[_Timenum];
        if (_Timenum == 0)
        {
            _TimeNumber = 60;
        }
        else if (_Timenum == 1)
        {
            _TimeNumber = 90;
        }
        else if (_Timenum == 2)
        {
            _TimeNumber = 120;
        }
        else
        {
            _TimeNumber = -1;
        }
    }
}
