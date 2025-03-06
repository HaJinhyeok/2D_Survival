using UnityEngine;

public class ExpItem : BaseController
{
    protected int _expPoint = 1;

    protected override void Initialize()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            GameManager.Instance.GetExp(_expPoint);
            Destroy(gameObject);
        }
    }
}
