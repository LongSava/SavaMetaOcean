using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    None,
    Loading,
    Ocean,
    Titanic
}

public class SceneController : Singleton<SceneController>
{
    public SceneType SceneFrom = SceneType.None;
    public SceneType SceneTo = SceneType.Loading;

    public void LoadScene(string name)
    {
        switch (name)
        {
            case "Ocean":
                LoadScene(SceneType.Ocean);
                break;
            case "Titanic":
                LoadScene(SceneType.Ocean);
                break;
        }
    }

    public void LoadScene(SceneType sceneType)
    {
        SceneFrom = SceneTo;
        SceneTo = sceneType;
        SceneManager.LoadScene(sceneType.ToString());
    }
}
