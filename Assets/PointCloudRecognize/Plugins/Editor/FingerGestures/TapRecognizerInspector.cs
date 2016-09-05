using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( TapRecognizer ) )]
public class TapRecognizerInspector : GestureRecognizerInspector<TapRecognizer>
{
    protected static GUIContent LABEL_RequiredTaps = new GUIContent( "Required Taps", "How many consecutive taps are required to recognize the gesture." );
    protected static GUIContent LABEL_MoveTolerance = new GUIContent( "Movement Tolerance", "How far the finger can move from its initial position without making the gesture fail" );
    protected static GUIContent LABEL_MaxDelayBetweenTaps = new GUIContent( "> Max Delay Between Taps", "The maximum amount of the time that can elapse between two consecutive taps without causing the recognizer to reset.\nSet to 0 to ignore this setting." );
    protected static GUIContent LABEL_MaxDuration = new GUIContent( "Max Duration", "Maximum amount of time the fingers can be held down without failing the gesture.\nSet to 0 for infinite duration." );
    
    protected override bool ShowRequiredFingerCount
    {
        get { return true; }
    }

    protected override void OnSettingsUI()
    {
        base.OnSettingsUI();

        Gesture1.RequiredTaps = EditorGUILayout.IntField( LABEL_RequiredTaps, Gesture1.RequiredTaps );

        GUI.enabled = ( Gesture1.RequiredTaps > 1 );
        EditorGUI.indentLevel++;
        Gesture1.MaxDelayBetweenTaps = EditorGUILayout.FloatField( LABEL_MaxDelayBetweenTaps, Gesture1.MaxDelayBetweenTaps );
        EditorGUI.indentLevel--;
        GUI.enabled = true;
        Gesture1.MoveTolerance = DistanceField( LABEL_MoveTolerance, Gesture1.MoveTolerance );
        
        Gesture1.MaxDuration = EditorGUILayout.FloatField( LABEL_MaxDuration, Gesture1.MaxDuration );       
    }

    protected override void ValidateValues()
    {
        base.ValidateValues();
        Gesture1.RequiredTaps = Mathf.Max( 1, Gesture1.RequiredTaps );
        Gesture1.MoveTolerance = Mathf.Max( 0, Gesture1.MoveTolerance );
        Gesture1.MaxDelayBetweenTaps = Mathf.Max( 0, Gesture1.MaxDelayBetweenTaps );
        Gesture1.MaxDuration = Mathf.Max( 0, Gesture1.MaxDuration );
    }

    protected override void OnNotices()
    {
        string multiTapName = string.Empty;

        if( Gesture1.RequiredFingerCount > 1 )
            multiTapName += "multi-finger, ";

        if( Gesture1.RequiredTaps == 1 )
            multiTapName += "single-tap";
        else if( Gesture1.RequiredTaps == 2 )
            multiTapName += "double-tap";
        else if( Gesture1.RequiredTaps == 3 )
            multiTapName += "triple-tap";
        else
            multiTapName += "multi-tap";

        EditorGUILayout.HelpBox( "Configured as a " + multiTapName + " gesture recognizer", MessageType.Info );

        base.OnNotices();
    }
}
