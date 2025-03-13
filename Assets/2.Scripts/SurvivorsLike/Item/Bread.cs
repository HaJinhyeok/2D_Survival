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
            GameManager.Instance.PlayerHp =
                Mathf.Min(GameManager.Instance.PlayerInfo.MaxHp, GameManager.Instance.PlayerHp + 20);
            Destroy(gameObject);
        }
    }
}
