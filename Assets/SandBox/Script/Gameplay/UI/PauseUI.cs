using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isSettingsOpen)
            Character.Instance.SetPause(false);

        base.Update();
    }
}
