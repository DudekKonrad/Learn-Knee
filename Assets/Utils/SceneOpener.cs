using UnityEditor;
using UnityEditor.SceneManagement;

namespace Utils
{
    public static class SceneOpener
    {
        private static void OpenSceneByPath(string scenePath)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }

        [MenuItem("Tools/Scenes/Open MainMenu")]
        public static void OpenMainMenuScene() => OpenSceneByPath("Assets/Scenes/MainMenu.unity");

        [MenuItem("Tools/Scenes/Open Gameplay")]
        public static void OpenGameplayScene() => OpenSceneByPath("Assets/Scenes/Gameplay.unity");
        [MenuItem("Tools/Scenes/Open Quiz")]
        public static void OpenQuizSceneScene() => OpenSceneByPath("Assets/Scenes/Quiz.unity");
    }
}