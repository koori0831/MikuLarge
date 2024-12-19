using UnityEngine;
using Unity.Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LastShake : Entity
{
    [SerializeField] private CinemachineBasicMultiChannelPerlin _cameraShake;
    [SerializeField] private ChainShot _shot1;
    [SerializeField] private ChainShot2 _shot2;
    [SerializeField] private ChainShot3 _shot3;
    [SerializeField] private ChainShot4 _shot4;
    [SerializeField] private ChainShot5 _shot5;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(5f, 3f);
    private bool _isShakeEnd = false;   
    private float duration = 0.8f;
    private List<IDamageable> damgeAble = new List<IDamageable>();
    [SerializeField] private float _enemyDamge;

    private void Update()
    {
        if (_shot1.isEnd && _shot2.isEnd && _shot3.isEnd && _shot4.isEnd && _shot5.isEnd && !_isShakeEnd)
        {
            StartCoroutine(CameraShake());

        }
    }

    private IEnumerator CameraShake()
    {
        _cameraShake.AmplitudeGain = 6f;
        _cameraShake.FrequencyGain = 6f;
        yield return new WaitForSeconds(1.5f);
        _isShakeEnd = true;
        _cameraShake.AmplitudeGain = 0;
        _cameraShake.FrequencyGain = 0f;
        EnemyDamge();

    }

    private void EnemyDamge()
    {
        foreach(IDamageable enemy in damgeAble)
        {
            Vector2 atkDirection = gameObject.transform.right;
            Vector2 knockBackForce = _knockBackForce;
            knockBackForce.x *= atkDirection.x;
            enemy.ApplyDamage(_enemyDamge, atkDirection, knockBackForce, this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damgeAble.Add(damageable);
            }
        }
    }
}
