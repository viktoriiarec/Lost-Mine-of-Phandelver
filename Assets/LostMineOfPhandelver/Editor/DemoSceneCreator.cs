using LostMineOfPhandelver.Runtime;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LostMineOfPhandelver.Editor
{
    public static class DemoSceneCreator
    {
        private const string RootFolder = "Assets/LostMineOfPhandelver";
        private const string SceneFolder = RootFolder + "/Scenes";
        private const string ScenePath = SceneFolder + "/Prototype.unity";

        [MenuItem("Tools/Lost Mine of Phandelver/Create Demo Scene")]
        public static void Create()
        {
            EnsureFolder("Assets", "LostMineOfPhandelver");
            EnsureFolder(RootFolder, "Scenes");

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var root = new GameObject("LostMineOfPhandelverPrototype");
            root.AddComponent<GoblinAmbushDemo>();

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
