using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

    public Text text;

    private int leftTime;
    public bool startCountingDown = false;

    void Start()
    {
        leftTime = 60;
        InvokeRepeating("DoCountDown", 0, 1);
    }

    void Update()
    {

    }

    public void DoCountDown()
    {
        if (startCountingDown && leftTime > 0)
        {
            leftTime -= 1;
            text.text = leftTime.ToString();
        }
    }
}
