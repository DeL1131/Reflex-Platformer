using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonSettings;
    [SerializeField] private Button _buttonExit;

    private void OnEnable()
    {
        _buttonPlay.onClick.AddListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonPlay));
        _buttonSettings.onClick.AddListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonSettings));
        _buttonExit.onClick.AddListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonExit));
    }

    private void OnDisable()
    {
        _buttonExit.onClick.RemoveListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonExit));
        _buttonPlay.onClick.RemoveListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonPlay));
        _buttonSettings.onClick.RemoveListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonSettings));
    }
}