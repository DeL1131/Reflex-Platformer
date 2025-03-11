using UnityEngine;

public abstract class ComplexityGame : MonoBehaviour
{
    public float SpawnDelay {  get; protected set; }
    public float MultiShotSpawnDelay { get; protected set; }
    public bool IsMultiShotActive  { get; protected set; }
}