using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    [SerializeField] Button startButton;

    protected override void Awake()
    {
        base.Awake();
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        fader.DOFade(0, .3f).onComplete += () =>
        {
            GameManager.Instance.StartGame();
            gameObject.SetActive(false);
        };
    }    
}
