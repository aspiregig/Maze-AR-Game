using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DragoEyes))]
public class DragoEyesEditor : Editor
{

    private SerializedObject serObj;

    private void OnEnable()
    {
        serObj = new SerializedObject(target);
    }


    public override void OnInspectorGUI()
    {
        serObj.Update();
        DrawDefaultInspector();

        DragoEyes MyDragoEyes = (DragoEyes)target;


        if (GUILayout.Button(new GUIContent("Change", "Change the Eyes color")))
        {
            MyDragoEyes.ChangeColor(MyDragoEyes.EyesMesh);
        }

            serObj.ApplyModifiedProperties();
    }
}
