using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectСomplexityMenu : Menu
{
    [SerializeField] private Button _buttonEasyComplexity;
    [SerializeField] private Button _buttonMediumComplexity;
    [SerializeField] private Button _buttonHardComplexity;

    public event Action GameStarted;

    private void OnEnable()
    {
        _buttonEasyComplexity.onClick.AddListener(InvokeGameStarted);
        _buttonMediumComplexity.onClick.AddListener(InvokeGameStarted);
        _buttonHardComplexity.onClick.AddListener(InvokeGameStarted);
        _buttonEasyComplexity.onClick.AddListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonEasyComplexity));
        _buttonMediumComplexity.onClick.AddListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonMediumComplexity));
        _buttonHardComplexity.onClick.AddListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonHardComplexity));
    }

    private void OnDisable()
    {
        _buttonEasyComplexity.onClick.RemoveListener(InvokeGameStarted);
        _buttonMediumComplexity.onClick.RemoveListener(InvokeGameStarted);
        _buttonHardComplexity.onClick.RemoveListener(InvokeGameStarted);
        _buttonEasyComplexity.onClick.RemoveListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonEasyComplexity));
        _buttonMediumComplexity.onClick.RemoveListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonMediumComplexity));
        _buttonHardComplexity.onClick.RemoveListener(() => InvokeOnButtonClicked(ButtonCommands.Commands.CommandButtonHardComplexity));
    }

    private void InvokeGameStarted()
    {
        GameStarted?.Invoke();
    }
}