using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1); // Loads Single Player Game Scene
    }

    public void LoadCoOp()
    {
        SceneManager.LoadScene(2); // Loads Co-op Game scene
    }
}
