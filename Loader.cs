using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{

    public enum Scene
    {
        MainMenu,
        Game,
        Load
    }

    private static Scene targetScene;

    public static void Load(Scene targetSceneName)
    {
        Loader.targetScene = targetSceneName;
        SceneManager.LoadScene(targetSceneName.ToString());
    }
}
