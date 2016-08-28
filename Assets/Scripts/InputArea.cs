using UnityEngine;
using System.Collections;

public class InputArea : MonoBehaviour
{
 

    private Vector3 point;

    static InputArea instance;

    

    static public InputArea getInstance()
    {
        if (instance == null)
        {
            instance = new InputArea();
        }
        return instance;
    }

    void Start() 
    {
        Input.multiTouchEnabled = true;//开启多点触碰
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.touchCount > 0)
       {
           Debug.Log("111");
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
          
                point = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
                InputReceiver.getInstance().startInput(point);
               

            }
            else if ((Input.GetTouch(1).phase == TouchPhase.Moved) || (Input.GetTouch(1).phase == TouchPhase.Stationary))
            {
                point = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
                InputReceiver.getInstance().handInput(point);
               
            }
            else if ((Input.GetTouch(1).phase == TouchPhase.Ended) || (Input.GetTouch(1).phase == TouchPhase.Canceled))
            {
                point = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
                InputReceiver.getInstance().finishInput();
            }
        }
    }
}
