using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallSpawner : MonoBehaviour
{
    private const int CommandTopScreenEdge = 1;
    private const int CommandLeftScreenEdge = 2;
    private const int CommandRightScreenEdge = 3;

    private const KeyCode SpawnCubeKey = KeyCode.Q;

    [SerializeField] private MagicBall _magicBoolPrefab;
    [SerializeField] private Player _target;
    [SerializeField] private float _spawnDelay;

    private CustomObjectPool<MagicBall> _poolMagicBalls;
    private Coroutine _coroutineSpawn;
    private Coroutine _coroutineMultiShotSpawn;

    private bool _isMultiShotActive = false;
    private bool _isCoroutineActive = false;
    private float _randomXPoint;
    private float _randomYPoint;
    private float _multiShotSpawnDelay = 3f;

    private void Start()
    {
        _poolMagicBalls = new CustomObjectPool<MagicBall>(_magicBoolPrefab);
    }


    private void FixedUpdate()
    {
        if (_coroutineSpawn == null)
        {
            _isCoroutineActive = true;
            _coroutineSpawn = StartCoroutine(CountDelay());
        }
        else if (_isCoroutineActive == false && _coroutineSpawn != null)
        {
            StopCoroutine(_coroutineSpawn);
        }

        if (_isMultiShotActive && _coroutineMultiShotSpawn == null)
        {
            ActivateMultiShotSpikes();
        }
    }

    public void SetSpawnerSettings(ComplexityGame complexityGame)
    {
        _spawnDelay = complexityGame.SpawnDelay;
        _multiShotSpawnDelay = complexityGame.MultiShotSpawnDelay;
        _isMultiShotActive = complexityGame.IsMultiShotActive;
    }

    private void SpawnObject()
    {
        MagicBall newSpike = _poolMagicBalls.Get();
        newSpike.OnMagicBallDeactivated += ReturnToPool;
        newSpike.transform.position = GetRandomPosition();

        Vector3 direction = _target.transform.position - newSpike.transform.position;

        newSpike.transform.right = direction.normalized;
    }

    private Vector2 GetRandomPosition()
    {
        int side = UnityEngine.Random.Range(1, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case CommandTopScreenEdge:
                spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0f, 1f), 1f, 10f));
                break;
            case CommandLeftScreenEdge:
                spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0f, UnityEngine.Random.Range(0f, 1f), 10f));
                break;
            case CommandRightScreenEdge:
                spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(1f, UnityEngine.Random.Range(0f, 1f), 10f));
                break;
        }

        spawnPosition.z = 0;

        return spawnPosition;
    }

    private IEnumerator CountDelay()
    {
        while (_isCoroutineActive && _target != null && _target.gameObject.activeSelf)
        {
            SpawnObject();
            WaitForSeconds waitForSeconds = new WaitForSeconds(_spawnDelay);
            yield return waitForSeconds;
        }
    }

    private void ReturnToPool(MagicBall spike)
    {
        spike.OnMagicBallDeactivated -= ReturnToPool;
        _poolMagicBalls.ReturnToPool(spike);
    }

    private void MultiplySpawn()
    {
        Vector2 randomPosition = GetRandomPosition();
        float topScreenPosition = 8;
        float countSpikeInMultiplyShot = 30;
        float angleStep = 360f / countSpikeInMultiplyShot;

        while (randomPosition.y < topScreenPosition)
        {
            randomPosition = GetRandomPosition();
        }

        List<MagicBall> listSpikes = new List<MagicBall>();

        for (int i = 0; i < countSpikeInMultiplyShot; i++)
        {
            MagicBall newSpike = _poolMagicBalls.Get();
            newSpike.OnMagicBallDeactivated += ReturnToPool;
            newSpike.transform.position = randomPosition;

            float angle = i * angleStep;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            newSpike.transform.rotation = Quaternion.Euler(0, 0, angle + 90f);

            listSpikes.Add(newSpike);
        }
    }

    private void ActivateMultiShotSpikes()
    {
        _coroutineMultiShotSpawn = StartCoroutine(MultiShotSpawnDelay(_multiShotSpawnDelay));
    }

    private IEnumerator<WaitForSeconds> MultiShotSpawnDelay(float multiShotSpawnDelay)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(multiShotSpawnDelay);

        while (_target != null)
        {
            yield return waitForSeconds;
            MultiplySpawn();
        }
    }
}