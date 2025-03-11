using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameOverMenu _panelGameOverMenu;

    public event Action GameEnded;

    private void OnEnable()
    {
        _player.Died += EndGame;
    }

    private void OnDisable()
    {
        _player.Died -= EndGame;
    }

    private void EndGame()
    {
        GameEnded?.Invoke();
        OpenGameOverPanel();
        StartCoroutine(StartRestartGame());
    }

    private void OpenGameOverPanel()
    {
        _panelGameOverMenu.Open();
    }

    private void CLoseGameOverPanel()
    {
        _panelGameOverMenu.Close();
    }

    private IEnumerator StartRestartGame()
    {
        DisableAllObjects();
        yield return new WaitForSeconds(6f);
        RestartGame();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DisableAllObjects()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag("Spike"))
            {
                Destroy(obj);
            }
        }
    }
}