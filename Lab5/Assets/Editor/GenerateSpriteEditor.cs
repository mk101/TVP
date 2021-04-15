using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateSprite))]
public class GenerateSpriteEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GenerateSprite gs = target as GenerateSprite;
        if (GUILayout.Button("Generate")) {
            gs.Generate();
        }
    }
}
