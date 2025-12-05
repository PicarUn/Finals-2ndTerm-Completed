using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public static PlayerLives Instance;

    [Header("Lives Settings")]
    public int totalLives = 3;

    [Header("UI")]
    public Image[] lifeIcons;
    public Sprite heartSprite;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SetupHearts();
    }

    void SetupHearts()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].sprite = heartSprite;
            lifeIcons[i].enabled = (i < totalLives);
        }
    }

    public void LoseLife()
    {
        totalLives--;

        UpdateLifeIcons();

        if (totalLives <= 0)
            GameOver();
    }

    void UpdateLifeIcons()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = (i < totalLives);
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
