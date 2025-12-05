using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;
    public GameObject image;

    public GameObject blocker;

    private bool isPaused = false;

    void SetBlocker(bool state)
    {
        if (blocker != null)
            blocker.SetActive(state);
    }

    public void OpenPanel()
    {
        if (panel == null) return;

        panel.SetActive(true);
        SetBlocker(true);

        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ClosePanel()
    {
        if (panel == null) return;

        panel.SetActive(false);

        isPaused = image != null && image.activeSelf;
        if (!isPaused) Time.timeScale = 1f;

        SetBlocker(isPaused); 
    }

    public void OpenImage()
    {
        if (image == null) return;

        bool isActive = image.activeSelf;
        image.SetActive(!isActive);

        if (image.activeSelf && panel != null)
            panel.SetActive(false);

        isPaused = image.activeSelf || (panel != null && panel.activeSelf);
        Time.timeScale = isPaused ? 0f : 1f;

        SetBlocker(isPaused);
    }

    public void CloseImage()
    {
        if (image == null) return;

        image.SetActive(false);

        isPaused = panel != null && panel.activeSelf;
        if (!isPaused) Time.timeScale = 1f;

        SetBlocker(isPaused);
    }

    public void TogglePanel()
    {
        if (panel == null) return;

        bool isActive = panel.activeSelf;
        panel.SetActive(!isActive);

        isPaused = !isActive || (image != null && image.activeSelf);
        Time.timeScale = isPaused ? 0f : 1f;

        SetBlocker(isPaused);
    }
}
