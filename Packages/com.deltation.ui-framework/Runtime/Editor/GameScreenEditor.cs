using DELTation.UI.Screens;
using UnityEditor;
using UnityEngine;

namespace DELTation.UI.Editor
{
    [CustomEditor(typeof(GameScreen))]
    public class GameScreenEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var disabled = EditorApplication.isPlayingOrWillChangePlaymode;
            EditorGUI.BeginDisabledGroup(disabled);

            if (GUILayout.Button("Preview"))
                GameScreenEditorUtility.Preview((GameScreen) target);

            EditorGUI.EndDisabledGroup();
        }
    }
}