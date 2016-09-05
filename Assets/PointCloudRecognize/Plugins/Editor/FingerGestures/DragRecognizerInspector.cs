using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( DragRecognizer ) )]
public class DragRecognizerInspector : GestureRecognizerInspector<DragRecognizer>
{
    protected static GUIContent LABEL_MoveTolerance = new GUIContent( "Movement Tolerance", "How far the finger can move from its initial position without starting the drag gesture" );
    protected static GUIContent LABEL_ApplySameDirectionConstraint = new GUIContent( "Apply Same Direction Constraint", "Enable this if you want the gesture to fail when the fingers are not moving in the same direction.\n\nValid for multi-finger drag gestures only (RequiredFingerCount >= 2)." );
    
    protected override bool ShowRequiredFingerCount
    {
        get { return true; }
    }

    protected override void OnSettingsUI()
    {
        base.OnSettingsUI();
        Gesture1.MoveTolerance = DistanceField( LABEL_MoveTolerance, Gesture1.MoveTolerance ); //EditorGUILayout.FloatField( LABEL_MoveTolerance, Gesture1.MoveTolerance );

        GUI.enabled = ( Gesture1.RequiredFingerCount > 1 );
        Gesture1.ApplySameDirectionConstraint = EditorGUILayout.Toggle( LABEL_ApplySameDirectionConstraint, Gesture1.ApplySameDirectionConstraint );
        GUI.enabled = true;
    }

    protected override void ValidateValues()
    {
        base.ValidateValues();
        Gesture1.MoveTolerance = Mathf.Max( 0, Gesture1.MoveTolerance );
    }
}
