using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] MainMenuUI mainMenuUI;

    public event Action OnStartGame;

    public bool IsGameStarted {  get; private set; }

    private void Start()
    {
        mainMenuUI.SetVisible(true);
    }

    public void StartGame()
    {
        IsGameStarted = true;
        OnStartGame?.Invoke();
    }

    public void QuitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
