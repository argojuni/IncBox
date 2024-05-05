using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string urlCrimping;
    public string urlIP;

    private void Start()
    {
        AudioManager.Instance.PlayMusic("BgMenu");
    }
    public void GameQuit()
    {
        Application.Quit();
    }

    public void DeletePlayerFerb()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Delete");
    }
    public void OpenURLCrimping()
    {
        Application.OpenURL(urlCrimping);
    }

    public void OpenURLIP()
    {
        Application.OpenURL(urlIP);
    }
}
