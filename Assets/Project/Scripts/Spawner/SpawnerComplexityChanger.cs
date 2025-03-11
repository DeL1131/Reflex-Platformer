using UnityEngine;

[RequireComponent(typeof(EasyComplexity))]
[RequireComponent(typeof(MediumComplexity))]
[RequireComponent(typeof(HardComplexity))]

public class SpawnerComplexityChanger : MonoBehaviour
{
    private const int CommandEasyComplexity = 1;
    private const int CommandMediumComplexity = 2;
    private const int CommandHardComplexity = 3;

    [SerializeField] public GameMenu _gameMenu;
    [SerializeField] private MagicBallSpawner _spawner;

    private EasyComplexity _easyComplexity;
    private MediumComplexity _mediumComplexity;
    private HardComplexity _hardComplexity;

    private int _levelComplexity;

    private void Start()
    {
        _gameMenu = GetComponent<GameMenu>();
        _easyComplexity = GetComponent<EasyComplexity>();
        _mediumComplexity = GetComponent<MediumComplexity>();
        _hardComplexity = GetComponent<HardComplexity>();
    }

    private void OnEnable()
    {
        _gameMenu.OnLevelChange += SetLevelComplexity;
    }

    private void SetLevelComplexity(int levelComplexity)
    {
        switch (levelComplexity)
        {
            case CommandEasyComplexity:
                _spawner.SetSpawnerSettings(_easyComplexity);
                break;
            case CommandMediumComplexity:
                _spawner.SetSpawnerSettings(_mediumComplexity);
                break;
            case CommandHardComplexity:
                _spawner.SetSpawnerSettings(_hardComplexity);
                break;
        }
    }
}
