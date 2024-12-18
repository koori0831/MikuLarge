using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
public class ChainShot2 : Entity
{
    [SerializeField] private LineRenderer _startLine;
    [SerializeField] private LineRenderer _NextLine;
    [SerializeField] private LineRenderer _NextLine2;
    [SerializeField] private LineRenderer _NextLine3;
    [SerializeField] private LineRenderer _NextLine4;
    private Vector3 endPos = Vector2.zero;
    private Vector3 endPos2 = Vector2.zero;
    private Vector3 endPos3 = Vector2.zero;
    private Vector3 endPos4 = Vector2.zero;
    private Vector3 endPos5 = Vector2.zero;
    [SerializeField] private LayerMask _wathIsWalls;
    [SerializeField] private CinemachineBasicMultiChannelPerlin _cameraShake;

    [SerializeField] private Vector2 _knockBackForce = new Vector2(5f, 3f);

    private List<IDamageable> damgeAble = new List<IDamageable>();
    [SerializeField] private float _enemyDamge;



    public bool isEnd = false;  
    private void Start()
    {
        StartCoroutine(DrawLines());

    }

    private IEnumerator DrawLines()
    {
        yield return new WaitForSeconds(0.5f);

        endPos = EndPosRay().point;
        yield return AnimateLine(_startLine, transform.position, endPos);

        // 두 번째 라인 생성
        LineRenderer nextLineObj = Instantiate(_NextLine, endPos, Quaternion.identity);
        LineRenderer nextLine = nextLineObj.GetComponent<LineRenderer>();
        yield return new WaitForSeconds(0.5f);

        endPos2 = EndPos2().point;
        yield return AnimateLine(nextLine, endPos, endPos2);

        // 세 번째 라인 생성
        LineRenderer nextLine2Obj = Instantiate(_NextLine2, endPos2, Quaternion.identity);
        LineRenderer nextLine2 = nextLine2Obj.GetComponent<LineRenderer>();
        yield return new WaitForSeconds(0.5f);

        endPos3 = EndPos3().point;
        yield return AnimateLine(nextLine2, endPos2, endPos3);

        // 네 번째 라인 생성
        LineRenderer nextLine3Obj = Instantiate(_NextLine3, endPos3, Quaternion.identity);
        LineRenderer nextLine3 = nextLine3Obj.GetComponent<LineRenderer>();
        yield return new WaitForSeconds(0.5f);

        endPos4 = EndPos4().point;
        yield return AnimateLine(nextLine3, endPos3, endPos4);

        // 마지막 라인 생성
        LineRenderer nextLine4Obj = Instantiate(_NextLine4, endPos4, Quaternion.identity);
        LineRenderer nextLine4 = nextLine4Obj.GetComponent<LineRenderer>();
        yield return new WaitForSeconds(0.5f);

        endPos5 = EndPos5().point;
        yield return AnimateLine(nextLine4, endPos4, endPos5);

        isEnd = true;
    }

    IEnumerator AnimateLine(LineRenderer line, Vector3 startPos, Vector3 endPos)
    {
        float duration = 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;


            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);

            // 라인 렌더러에 위치 설정
            line.SetPosition(0, startPos);
            line.SetPosition(1, currentPos);

            yield return null;
        }

        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
        StartCoroutine(CameraShake(0.2f, 0.1f));
    }

    IEnumerator CameraShake(float duration, float magnitude)
    {
        float _amplitudeGain = 0;
        float _frequencyGain = 0;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            _cameraShake.AmplitudeGain = 5f;
            _cameraShake.FrequencyGain = 5f;
            elapsed += Time.deltaTime;
            EnemyDamge();

            yield return null;
        }

        _cameraShake.AmplitudeGain = _amplitudeGain;
        _cameraShake.FrequencyGain = _frequencyGain; 
    }

    private void EnemyDamge()
    {
        foreach (IDamageable enemy in damgeAble)
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
    private void Update()
    {
        EndPosRay();
        EndPos2();
        EndPos3();
        EndPos4();
    }

    public RaycastHit2D EndPosRay()
    {
        return Physics2D.Raycast(gameObject.transform.position, Vector2.up + Vector2.left, Mathf.Infinity, _wathIsWalls);
    }

    public RaycastHit2D EndPos2()
    {
        return Physics2D.Raycast(_NextLine.transform.position, Vector2.right, Mathf.Infinity, _wathIsWalls);
    }

    public RaycastHit2D EndPos3()
    {
        return Physics2D.Raycast(_NextLine.transform.position, Vector2.down + Vector2.left, Mathf.Infinity, _wathIsWalls);
    }

    public RaycastHit2D EndPos4()
    {
        return Physics2D.Raycast(_NextLine.transform.position, Vector2.up + Vector2.right, Mathf.Infinity, _wathIsWalls);
    }

    public RaycastHit2D EndPos5()
    {
        return Physics2D.Raycast(_NextLine.transform.position, Vector2.down + Vector2.left, Mathf.Infinity, _wathIsWalls);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
