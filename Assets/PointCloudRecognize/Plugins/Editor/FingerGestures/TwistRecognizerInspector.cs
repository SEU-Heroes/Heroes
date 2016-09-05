using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( TwistRecognizer ) )]
public class TwistRecognizerInspector : GestureRecognizerInspector<TwistRecognizer>
{
	protected static GUIContent LABEL_MinDOT = new GUIContent( "Minimum DOT", 
		"Rotation DOT product treshold\n" +
		"Controls how tolerant the twist recognizer is to the two fingers moving in opposite directions.\n" +
		"Setting this to -1 means the fingers have to move in exactly opposite directions to each other\n" +
		"This value should be kept between -1 and 0 excluded." );
	
    protected static GUIContent LABEL_MinRotation = new GUIContent( "Minimum Rotation", "Minimum amount of rotation required to start the rotation gesture (in degrees)" );
    protected static GUIContent LABEL_AllowPivotAroundFinger = new GUIContent( "Allow Pivot Around First Finger", "Allow the user to perform a twist gesture by rotating the second finger around the first one (which must stay motionless)" );
    protected static GUIContent LABEL_Method = new GUIContent( "Method",
        "Specify the twist method to use.\n" +
        "- Standard: two fingers rotating around a mid point\n" +
        "- Pivot: one finger rotates around the other one (which must remain motionless)" );

    protected static GUIContent LABEL_PivotMoveTolerance = new GUIContent( "Pivot Movement Tolerance", "How far the pivot finger can move from its initial position without causing the gesture to fail" );

    protected override bool ShowRequiredFingerCount
    {
        get { return false; }
    }

    protected override void OnSettingsUI()
    {
        base.OnSettingsUI();
        Gesture1.MinDOT = EditorGUILayout.FloatField( LABEL_MinDOT, Gesture1.MinDOT );
        Gesture1.MinRotation = EditorGUILayout.FloatField( LABEL_MinRotation, Gesture1.MinRotation );
        Gesture1.Method = (TwistMethod)EditorGUILayout.EnumPopup( LABEL_Method, Gesture1.Method );

        if( Gesture1.Method == TwistMethod.Pivot )
        {
            EditorGUI.indentLevel++;
            Gesture1.PivotMoveTolerance = DistanceField( LABEL_PivotMoveTolerance, Gesture1.PivotMoveTolerance );
            EditorGUI.indentLevel--;
        }
    }

    protected override void ValidateValues()
    {
        base.ValidateValues();

        Gesture1.MinDOT = Mathf.Clamp( Gesture1.MinDOT, -1.0f, 0 );
        Gesture1.MinRotation = Mathf.Clamp( Gesture1.MinRotation, 0, 360 );
    }
}
