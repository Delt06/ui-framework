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

            var domainReloadIsEnabled = !EditorSettings.enterPlayModeOptionsEnabled ||
                                        (EditorSettings.enterPlayModeOptions &
                                         EnterPlayModeOptions.DisableDomainReload) == 0;
            var disabled = EditorApplication.isPlayingOrWillChangePlaymode || domainReloadIsEnabled;

            if (domainReloadIsEnabled)
                EditorGUILayout.HelpBox(
                    "To enable the Preview feature, disable the Reload Domain option in Project Settings/Editor/Enter Play Mode Settings.",
                    MessageType.Warning
                );

            EditorGUI.BeginDisabledGroup(disabled);

            if (GUILayout.Button("Preview"))
                GameScreenEditorUtility.Preview((GameScreen) target);

            EditorGUI.EndDisabledGroup();
        }
    }
}