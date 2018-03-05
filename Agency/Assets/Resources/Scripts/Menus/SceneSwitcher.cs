using UnityEngine.SceneManagement;

/// <summary>
/// Centralized place to switch scenes and store data
/// </summary>
public static class SceneSwitcher
{
    public static void GotoScene(string newScene)
    {
        string currScene = SceneManager.GetActiveScene().name;
        if (currScene == "ManagementScene")
        {
            switch (newScene)
            {
                case "MainGame":
                    break;
            }
        }
        else if (currScene == "MainGame")
        {
            switch (newScene)
            {
                case "ManagementScene":
                    break;
            }
        }
        else if (currScene == "MainMenu")
        {
            switch (newScene)
            {
                case "ManagementScene":
                    break;
            }
        }
    }
}