using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PointCloudTutorial: MonoBehaviour {
    public Text text;
    void OnCustomGesture(PointCloudGesture gesture)
    {
        GameManager.GetInstance().HandleGesture(gesture.RecognizedTemplate.name,gesture.MatchScore);
        Debug.Log("Recognized custom gesture: " + gesture.RecognizedTemplate.name +
        ", match scor11e: " + gesture.MatchScore +
        ", match distance: " + gesture.MatchDistance);
    }
}
