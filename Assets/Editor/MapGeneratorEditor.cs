using System.Collections;
using System.Collections.Generic;
using UnityEditor;


[CustomEditor (typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.isActiveAndEnabled)
            {
                mapGen.GenerateMap();
            }
        }

        if (EditorGUILayout.LinkButton("Generate"))
        {
            mapGen.GenerateMap();
        }

    }
}


