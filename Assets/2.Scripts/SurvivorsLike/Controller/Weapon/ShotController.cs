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

    // update�� ���� ����
        
    IEnumerator CoDeactivate()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
