using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( SwipeRecognizer ) )]
public class SwipeRecognizerInspector : GestureRecognizerInspector<SwipeRecognizer>
{
    protected static GUIContent LABEL_MinDistance = new GUIContent( "Min Distance", "Minimum distance the finger must travel in order to produce a valid swipe" );
    protected static GUIContent LABEL_MaxDistance = new GUIContent( "Max Distance", "Finger travel distance beyond which the swipe recognition will fail.\nSetting this to 0 disables the constraint" );
    protected static GUIContent LABEL_MinVelocity = new GUIContent( "Min Velocity", "Minimum speed the finger must maintain in order to produce a valid swipe gesture" );
    protected static GUIContent LABEL_MaxDeviation = new GUIContent( "Max Deviation", "Maximum angle that the swipe direction is allowed to deviate from its initial direction (in degrees)" );

    protected override bool ShowRequiredFingerCount
    {
        get { return true; }
    }

    protected override void OnSettingsUI()
    {
        base.OnSettingsUI();

        Gesture1.MinDistance = DistanceField( LABEL_MinDistance, Gesture1.MinDistance );
        Gesture1.MaxDistance = DistanceField( LABEL_MaxDistance, Gesture1.MaxDistance );
        Gesture1.MinVelocity = DistanceField( LABEL_MinVelocity, Gesture1.MinVelocity, "/s" );
        Gesture1.MaxDeviation = EditorGUILayout.FloatField( LABEL_MaxDeviation, Gesture1.MaxDeviation );
    }

    protected override void ValidateValues()
    {
        base.ValidateValues();
        Gesture1.MinDistance = Mathf.Max( 0.01f, Gesture1.MinDistance );
        Gesture1.MaxDistance = Mathf.Max( 0, Gesture1.MaxDistance );
        Gesture1.MinVelocity = Mathf.Max( 0, Gesture1.MinVelocity );
        Gesture1.MaxDeviation = Mathf.Max( 0, Gesture1.MaxDeviation );
    }
}
