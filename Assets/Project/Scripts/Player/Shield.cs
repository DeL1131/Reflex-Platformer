using System;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float _lifeTime = 3;

    public event Action ShieldActivated;
    public event Action ShieldDamaged;

    private void OnEnable()
    {
        StartCoroutine(ShieldLifeTimer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out MagicBall magicBall))
        {
            ShieldDamaged?.Invoke();
        }
    }

    private IEnumerator<WaitForSeconds> ShieldLifeTimer()
    {
        ShieldActivated?.Invoke();
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);
    }
}