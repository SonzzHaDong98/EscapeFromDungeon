using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToMenuStart()
    {
        SceneManager.LoadScene(0);      
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()    
    {
        Application.Quit();
    }
}
