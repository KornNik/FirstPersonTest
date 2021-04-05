using UnityEditor;
using UnityEngine;

namespace ExampleTemplate
{
    [CustomEditor(typeof(LoadLevelFromEditor))]
    class LoadLevel : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Загрузить лвл"))
                ((LoadLevelFromEditor)target).Load();
        }
    }
}
