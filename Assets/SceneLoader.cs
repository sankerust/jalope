using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadGame() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

public void QuitGame() {
  // if doesnt quit - might have to do with timescale set to 0
  Application.Quit();
}
}
