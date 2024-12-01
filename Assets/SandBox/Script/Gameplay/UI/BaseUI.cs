using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    [SerializeField] GameObject defaultPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] protected CanvasGroup fader;

    [SerializeField] Button quitButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitSettingsButton;

    protected bool isSettingsOpen;

    protected virtual void Awake()
    {
        isSettingsOpen = false;
        quitButton.onClick.AddListener(GameManager.Instance.QuitGame);
        settingsButton.onClick.AddListener(ShowSettings);
        quitSettingsButton.onClick.AddListener(QuitSettings);
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isSettingsOpen)
        {
            QuitSettings();
        }
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    void ShowSettings()
    {
        settingsPanel.SetActive(true);
        defaultPanel.SetActive(false);
    }

    void QuitSettings()
    {
        settingsPanel.SetActive(false);
        defaultPanel.SetActive(true);
    }
}
