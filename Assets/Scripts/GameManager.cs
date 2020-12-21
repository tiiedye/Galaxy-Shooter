using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    [SerializeField]
    private GameObject _pauseMenu;
    private Animator _pauseAnimator;

    private void Start()
    {
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true) {
            SceneManager.LoadScene(1); // Current Game Scene
        }

        // if esc is pressed, quit application.
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            _pauseMenu.SetActive(true);
            _pauseAnimator.SetBool("IsPaused", true);
            Time.timeScale = 0.0f;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void ClosePause()
    {
        // Hides pause menu and un-pauses game.
        _pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
