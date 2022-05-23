using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace DELTation.UI.Editor
{
    public class GameScreenPreviewSettings : ScriptableObject
    {
        private const string FullPath = "Assets/Editor/Resources/" + ResourcesPath + ".asset";
        private const string ResourcesPath = "UiFramework/Preview Settings";

        [CanBeNull]
        private static GameScreenPreviewSettings _instance;
        [SerializeField] [CanBeNull] private Canvas _canvasPrefab;
        [SerializeField] [Min(0f)] private float _beginDelay = 0.25f;
        [SerializeField] [Min(0f)] private float _openTime = 0.25f;
        [SerializeField] [Min(0f)] private float _endDelay = 0.25f;


        [CanBeNull] public Canvas CanvasPrefab => _canvasPrefab;

        [NotNull]
        public static GameScreenPreviewSettings Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = Resources.Load<GameScreenPreviewSettings>(ResourcesPath);
                if (_instance != null) return _instance;

                EnsureAllFoldersAreCreated(Path.GetDirectoryName(FullPath));

                var newInstance = CreateInstance<GameScreenPreviewSettings>();
                _instance = newInstance;
                AssetDatabase.CreateAsset(_instance, FullPath);
                AssetDatabase.SaveAssetIfDirty(_instance);

                return newInstance;
            }
        }

        public float BeginDelay => _beginDelay;

        public float OpenTime => _openTime;

        public float EndDelay => _endDelay;

        private static void EnsureAllFoldersAreCreated(string path)
        {
            var pathSeparator = Path.DirectorySeparatorChar;
            var pathSeparatorStr = Path.DirectorySeparatorChar.ToString();
            var parts = path.Split(pathSeparator);

            for (var partIndex = 1; partIndex < parts.Length; partIndex++)
            {
                var parentFolder = string.Join(pathSeparatorStr, parts.Take(partIndex));
                var folderName = parts[partIndex];
                if (AssetDatabase.IsValidFolder(parentFolder + pathSeparatorStr + folderName)) continue;

                AssetDatabase.CreateFolder(parentFolder, folderName);
            }
        }
    }
}