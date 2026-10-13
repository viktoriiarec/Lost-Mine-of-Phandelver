using HeartOfDepths.Runtime;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeartOfDepths.Editor
{
    public static class DemoSceneCreator
    {
        private const string SceneFolder = "Assets/HeartOfDepths/Scenes";
        private const string ScenePath = SceneFolder + "/Prototype.unity";

        [MenuItem("Tools/Heart of Depths/Create Demo Scene")]
        public static void Create()
        {
            EnsureFolder("Assets", "HeartOfDepths");
            EnsureFolder("Assets/HeartOfDepths", "Scenes");

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var root = new GameObject("HeartOfDepthsPrototype");
            root.AddComponent<PrototypeDemo>();

            EditorSceneManager.SaveScene(scene, ScenePath);
            Selection.activeGameObject = root;
            Debug.Log($"Created and opened {ScenePath}. Press Play to test the prototype.");
        }

        private static void EnsureFolder(string parent, string child)
        {
            string path = $"{parent}/{child}";
            if (!AssetDatabase.IsValidFolder(path))
                AssetDatabase.CreateFolder(parent, child);
        }
    }
}
