using System.Collections;
using UnityEngine;

public class ShotController : BaseController
{
    Vector2 _moveDir;

    protected override void Initialize()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(CoDeactivate());
    }

    private void Update()
    {
        transform.Translate(_moveDir * GameManager.Instance.ShotInfo.Speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 dir)
    {
        _moveDir = dir;
    }

    IEnumerator CoDeactivate()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
