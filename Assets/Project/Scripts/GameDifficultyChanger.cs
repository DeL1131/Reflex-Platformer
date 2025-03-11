using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficultyChanger : MonoBehaviour
{
    [SerializeField] private List<float> _complexityLevels = new List<float> { 10f, 20f, 30f };

    private float _timeInGame;
    private int _currentComplexityIndex = 0;
    private bool _hasEventBeenTriggered = false;

    public int GameComplexity { get; private set; } = 0;

    public event Action IncreasingComplexity;
    public event Action ActivateMultiShotSpikes;

    private void Update()
    {
        if (_currentComplexityIndex < _complexityLevels.Count && _timeInGame >= _complexityLevels[_currentComplexityIndex])
        {
            GameComplexity++;
            IncreasingComplexity?.Invoke();
            _currentComplexityIndex++;
        }
        if (_currentComplexityIndex == _complexityLevels.Count && _hasEventBeenTriggered == false)
        {
            ActivateMultiShotSpikes?.Invoke();
            _hasEventBeenTriggered = true;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(GameTimeCounter());
    }

    private IEnumerator<WaitForSeconds> GameTimeCounter()
    {
        float stemTimeInterval = 0.5f;

        WaitForSeconds waitForSeconds = new WaitForSeconds(stemTimeInterval);

        while (true)
        {
            _timeInGame += stemTimeInterval;
            yield return waitForSeconds;
        }
    }
}