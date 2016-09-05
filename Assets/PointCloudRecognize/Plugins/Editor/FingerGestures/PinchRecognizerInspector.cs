using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( PinchRecognizer ) )]
public class PinchRecognizerInspector : GestureRecognizerInspector<PinchRecognizer>
{
	protected static GUIContent LABEL_MinDOT = new GUIContent( "Minimum DOT", 
		"Rotation DOT product treshold\n" +
		"Controls how tolerant the recognizer is to the two fingers moving in opposite directions.\n" +
		"Setting this to -1 means the fingers have to move in exactly opposite directions to each other\n" +
		"This value should be kept between -1 and 0 excluded." );
	
    protected static GUIContent LABEL_MinDistance = new GUIContent( "Minimum Distance", "Minimum gap required between the two fingers in order to begin the pinch gesture recognition" );
	
    protected override bool ShowRequiredFingerCount
    {
        get { return false; }
    }

    protected override void OnSettingsUI()
    {
        base.OnSettingsUI();
        Gesture1.MinDOT = EditorGUILayout.FloatField( LABEL_MinDOT, Gesture1.MinDOT );
        Gesture1.MinDistance = DistanceField( LABEL_MinDistance, Gesture1.MinDistance ); 
    }

    protected override void ValidateValues()
    {
        base.ValidateValues();

        Gesture1.MinDOT = Mathf.Clamp( Gesture1.MinDOT, -1.0f, 1.0f );
        Gesture1.MinDistance = Mathf.Max( 0, Gesture1.MinDistance );
    }
}
