using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor( typeof( PointCloudRegognizer ) )]
public class PointCloudRecognizerInspector : GestureRecognizerInspector<PointCloudRegognizer>
{
    protected static GUIContent LABEL_Templates = new GUIContent( "Gesture1 Templates List", "List of gesture templates that will be matched against the user's gesture" );
    protected static GUIContent LABEL_MinDistanceBetweenSamples = new GUIContent( "Sampling Distance", "Minimum distance between two consecutive finger position samples. Smaller means more accurate recording of the gesture, but more samples to process." );
    protected static GUIContent LABEL_MaxMatchDistance = new GUIContent( "Max Match Distance", "Threshold value that controls how accurate the user-generated gesture must be in order to match its corresponding template gesture. The lower the value, the more accurate the user must be." );

    protected override bool ShowRequiredFingerCount
    {
        get { return true; }
    }

    protected override void OnSettingsUI()
    {
        base.OnSettingsUI();
        
        GUILayout.Space( 10 );

        Gesture1.MaxMatchDistance = EditorGUILayout.FloatField( LABEL_MaxMatchDistance, Gesture1.MaxMatchDistance );
        Gesture1.MinDistanceBetweenSamples = EditorGUILayout.FloatField( LABEL_MinDistanceBetweenSamples, Gesture1.MinDistanceBetweenSamples );

        serializedObject.Update();
        if( Gesture1.Templates == null )
        {
            Gesture1.Templates = new List<PointCloudGestureTemplate>();
            EditorUtility.SetDirty( Gesture1 );
        }

        EditorGUILayout.PropertyField( serializedObject.FindProperty( "Templates" ), LABEL_Templates, true );
        serializedObject.ApplyModifiedProperties();
    }

    protected override void ValidateValues()
    {
        base.ValidateValues();
        Gesture1.MinDistanceBetweenSamples = Mathf.Max( 1.0f, Gesture1.MinDistanceBetweenSamples );
        Gesture1.MaxMatchDistance = Mathf.Max( 0.1f, Gesture1.MaxMatchDistance );
    }

    protected override void OnToolbar()
    {
        base.OnToolbar();

        if( GUILayout.Button( "New Gesture1 Template" ) )
        {
            PointCloudGestureTemplate template = FingerGesturesEditorUtils.CreateAsset<PointCloudGestureTemplate>();
            Gesture1.Templates.Add( template );
        }
    }
}
