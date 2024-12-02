using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenMenuScript : MonoBehaviour
{
    // Reference to the menu GameObject
    public GameObject menu;

    void Start()
    {
        // Ensure the menu is hidden at the start
        menu.SetActive(false);
        // Ensure the game is running
        Time.timeScale = 1f;
    }

    // Method to toggle the menu and pause/unpause the game
    public void activeMenu()
    {
        // Toggle the menu's visibility
        menu.SetActive(true);

        // Pause the game if the menu is active, otherwise unpause
        if (menu.activeSelf)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

     // Method to restart the game
    public void restartGame()
    {
        // Resume time in case the game is paused
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
