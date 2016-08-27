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


    // Update is called once per frame
    void Update()
    {
       
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                point = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                InputReceiver.getInstance().startInput(point);
                Debug.Log("guiji");
               
            }
            else if ((Input.GetTouch(0).phase == TouchPhase.Moved)||(Input.GetTouch(0).phase==TouchPhase.Stationary))
            {
                point = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                InputReceiver.getInstance().handInput(point);
                Debug.Log("guiji");
            }
            else if ((Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetTouch(0).phase == TouchPhase.Canceled))
            {
                point = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                InputReceiver.getInstance().finishInput();
                Debug.Log("guiji");
            }
    }
}
