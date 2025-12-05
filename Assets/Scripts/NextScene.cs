using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Almanac");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        DOTween.KillAll(true);
        SceneManager.LoadScene("Main Menu");
    }
}