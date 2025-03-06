using UnityEngine;

public class Bread : BaseController
{
    protected override void Initialize()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            GameManager.Instance.PlayerInfo.CurrentHp += 20;
            Destroy(gameObject);
        }
    }
}
