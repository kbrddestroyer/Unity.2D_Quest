using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraPlayerResolution : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CameraController cameraController = (CameraController) target;
 
        if (GUILayout.Button("Resolve player automatically"))
        {
            cameraController.ResolvePlayerOnScene();
        }
    }
}
#endif

