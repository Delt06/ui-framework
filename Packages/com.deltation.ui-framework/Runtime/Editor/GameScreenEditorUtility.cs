using System;
using System.Linq;
using System.Threading.Tasks;
using DELTation.UI.Screens;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static System.Threading.Tasks.Task;
using static System.TimeSpan;
using Object = UnityEngine.Object;

namespace DELTation.UI.Editor
{
    public static class GameScreenEditorUtility
    {
        public static async void Preview([NotNull] GameScreen gameScreen)
        {
            if (gameScreen == null) throw new ArgumentNullException(nameof(gameScreen));

            if (EditorApplication.isPlayingOrWillChangePlaymode)
                throw new InvalidOperationException("Can't preview in play mode.");


            GameObject newPrefab = null;
            string scenePath = null;
            string[] allLoadedScenesPaths = null;
            GameObject openPrefab = null;

            try
            {
                var guid = Guid.NewGuid().ToString();
                var prefabPath = $"Assets/UIFramework_Preview_{guid}.prefab";

                var prefab = gameScreen.gameObject;
                if (!PrefabUtility.IsPartOfPrefabAsset(prefab))
                {
                    newPrefab = PrefabUtility.SaveAsPrefabAsset(gameScreen.gameObject, prefabPath);
                    prefab = newPrefab;
                }

                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    throw new InvalidOperationException("User cancelled.");

                allLoadedScenesPaths = Enumerable.Range(0, EditorSceneManager.loadedSceneCount)
                    .Select(SceneManager.GetSceneAt)
                    .Where(s => s.isLoaded)
                    .Select(s => s.path)
                    .ToArray();

                openPrefab = GetOpenPrefabOrDefault(gameScreen.gameObject);


                scenePath = $"Assets/UIFramework_Preview_{guid}.unity";
                var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

                if (!EditorSceneManager.SaveScene(scene, scenePath))
                    throw new InvalidOperationException("Could not create scene.");

                await EnterPlaymodeAsync();

                EditorSceneManager.LoadSceneInPlayMode(scenePath, new LoadSceneParameters
                    {
                        loadSceneMode = LoadSceneMode.Single,
                        localPhysicsMode = LocalPhysicsMode.None,
                    }
                );

                await Yield();
                var previewSettings = GameScreenPreviewSettings.Instance;

                CreateCamera();
                CreateEventSystem();
                var canvas = CreateCanvas(previewSettings);

                await Delay(FromSeconds(previewSettings.BeginDelay));

                var gameScreenInstance = CreateGameScreenInstance(prefab, canvas);
                gameScreenInstance.Open();
                await WhenAnimationFinishesAsync(gameScreenInstance);
                await Delay(FromSeconds(previewSettings.OpenTime));

                gameScreenInstance.Close();
                await WhenAnimationFinishesAsync(gameScreenInstance);

                await Delay(FromSeconds(previewSettings.EndDelay));

                await EnterEditModeAsync();
            }
            finally
            {
                if (newPrefab != null)
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(newPrefab));
                if (scenePath != null)
                    AssetDatabase.DeleteAsset(scenePath);

                if (allLoadedScenesPaths != null)
                    for (var index = 0; index < allLoadedScenesPaths.Length; index++)
                    {
                        var loadedScenesPath = allLoadedScenesPaths[index];
                        EditorSceneManager.OpenScene(loadedScenesPath,
                            index > 0 ? OpenSceneMode.Additive : OpenSceneMode.Single
                        );
                    }

                if (openPrefab != null)
                    AssetDatabase.OpenAsset(openPrefab);
            }
        }

        [CanBeNull]
        private static GameObject GetOpenPrefabOrDefault(GameObject child)
        {
            var currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (currentPrefabStage == null) return null;
            if (!currentPrefabStage.IsPartOfPrefabContents(child)) return null;

            return AssetDatabase.LoadAssetAtPath<GameObject>(currentPrefabStage.assetPath);
        }

        private static async Task EnterPlaymodeAsync()
        {
            EditorApplication.EnterPlaymode();
            await AwaitPlaymodeStateChange(PlayModeStateChange.EnteredPlayMode);
        }

        private static async Task AwaitPlaymodeStateChange(PlayModeStateChange requiredStateChanged)
        {
            var taskCompletionSource = new TaskCompletionSource<int>();
            // ReSharper disable once ConvertToLocalFunction
            Action<PlayModeStateChange> onStatusChanged = stateChange =>
            {
                if (stateChange != requiredStateChanged) return;
                taskCompletionSource.TrySetResult(0);
            };
            EditorApplication.playModeStateChanged += onStatusChanged;
            await taskCompletionSource.Task;
            EditorApplication.playModeStateChanged -= onStatusChanged;
        }

        private static void CreateCamera()
        {
            var camera = new GameObject("Camera").AddComponent<Camera>();
            camera.backgroundColor = Color.black;
            camera.clearFlags = CameraClearFlags.SolidColor;
        }

        private static void CreateEventSystem()
        {
            var eventSystem = new GameObject("EventSystem").AddComponent<EventSystem>();
            eventSystem.gameObject.AddComponent<StandaloneInputModule>();
        }

        private static Canvas CreateCanvas(GameScreenPreviewSettings previewSettings)
        {
            Canvas canvas;
            if (previewSettings.CanvasPrefab != null)
            {
                canvas = Object.Instantiate(previewSettings.CanvasPrefab);
            }
            else
            {
                canvas = new GameObject("Canvas").AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            return canvas;
        }

        private static GameScreen CreateGameScreenInstance(GameObject prefab, Canvas canvas) =>
            Object.Instantiate(prefab, canvas.transform).GetComponent<GameScreen>();

        private static async Task WhenAnimationFinishesAsync(IScreenListener gameScreen)
        {
            while (gameScreen.ShouldBeAwaited)
            {
                await Yield();
            }
        }

        private static async Task EnterEditModeAsync()
        {
            EditorApplication.ExitPlaymode();
            await AwaitPlaymodeStateChange(PlayModeStateChange.EnteredEditMode);
        }
    }
}