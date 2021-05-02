using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loader : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Laser_Defender");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
