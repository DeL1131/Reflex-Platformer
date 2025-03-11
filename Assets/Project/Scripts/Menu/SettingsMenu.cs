using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    [SerializeField] private Button _buttonCloseSettingsMenu;

    private void OnEnable()
    {
        _buttonCloseSettingsMenu.onClick.AddListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonCloseSettingsMenu));
    }

    private void OnDisable()
    {
        _buttonCloseSettingsMenu.onClick.RemoveListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonCloseSettingsMenu));
    }
}