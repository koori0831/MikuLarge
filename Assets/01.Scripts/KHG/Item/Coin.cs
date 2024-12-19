using System.Collections;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    private ParticleSystem _coinParticle;

    private bool _particleCool;
    public void Collect()
    {
        CoinCollected();
    }

    private void Awake()
    {
        _coinParticle = GetComponent<ParticleSystem>();
    }

    private void CoinCollected()
    {
        Manager.manager.ResourceManager.Coin++;
        Manager.manager.ResourceUI.SetCoin();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            ParticleEmmit();
        }
    }

    private void ParticleEmmit()
    {
        if (_particleCool) return;
        _coinParticle.Play();
        StartCoroutine(Cool(0.5f));
    }

    private IEnumerator Cool(float time)
    {
        _particleCool = true;
        yield return new WaitForSeconds(time);
        _particleCool = false;
    }
}
