using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            Time.timeScale = 1f;
            DOTween.KillAll(true);
            SceneManager.LoadScene(sceneName);
        }
    }
}
