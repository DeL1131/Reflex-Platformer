using System;
using UnityEngine;
using UnityEngine.Audio;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _backGroundPanel;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private Select—omplexityMenu _selectComplexityMenu;
    [SerializeField] private AudioMixerGroup _mixer;

    private Menu _currentMenu;

    private int _easyLevelComplexity = 1;
    private int _mediumLevelComplexity = 2;
    private int _hardLevelComplexity = 3;
    private bool _isGameStarted;

    public event Action<int> OnLevelChange;

    private void Awake()
    {
        _isGameStarted = false;
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (_isGameStarted)
        {
            _backGroundPanel.SetActive(false);
        }
        else
        {
            _backGroundPanel.SetActive(true);
        }
    }

    private void Start()
    {
        OpenMenu(_mainMenu);
    }

    private void OnEnable()
    {
        _mainMenu.OnButtonClicked += OpenSelectedMenu;
        _settingsMenu.OnButtonClicked += OpenSelectedMenu;
        _selectComplexityMenu.OnButtonClicked += OpenSelectedMenu;
    }

    private void OnDisable()
    {
        _mainMenu.OnButtonClicked -= OpenSelectedMenu;
        _settingsMenu.OnButtonClicked -= OpenSelectedMenu;
        _selectComplexityMenu.OnButtonClicked -= OpenSelectedMenu;
    }

    public void OpenSelectedMenu(string selectedMenu)
    {
        switch (selectedMenu)
        {
            case ButtonCommands.Commands.CommandButtonPlay:
                OpenMenu(_selectComplexityMenu);
                break;
            case ButtonCommands.Commands.CommandButtonSettings:
                OpenMenu(_settingsMenu);
                break;
            case ButtonCommands.Commands.CommandButtonCloseSettingsMenu:
                OpenMenu(_mainMenu);
                break;
            case ButtonCommands.Commands.CommandButtonEasyComplexity:
                StartGame(_easyLevelComplexity);
                break;
            case ButtonCommands.Commands.CommandButtonMediumComplexity:
                StartGame(_mediumLevelComplexity);
                break;
            case ButtonCommands.Commands.CommandButtonHardComplexity:
                StartGame(_hardLevelComplexity);
                break;
            case ButtonCommands.Commands.CommandButtonExit:
                CloseGame();
                break;
        }
    }

    public void OpenMenu(Menu menu)
    {
        if (_currentMenu != null)
            _currentMenu.Close();

        _currentMenu = menu;
        _currentMenu.Open();
    }

    private void CloseGame()
    {
        Application.Quit();
    }

    private void StartGame(int levelComplexity)
    {
        Time.timeScale = 1f;
        _isGameStarted = true;
        OnLevelChange?.Invoke(levelComplexity);
        _currentMenu.Close();
    }
}