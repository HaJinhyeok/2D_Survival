using UnityEngine;

public class Sword : BaseController
{
    protected override void Initialize()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            Destroy(gameObject);
            if (GameManager.Instance.PlayerInfo.SwordNum < 4)
            {
                PoolManager.Instance.GetObject<SwordController>(ObjectManager.Instance.Player.transform.position);
                // ObjectManager.Instance.Spawn<SwordController>(ObjectManager.Instance.Player.transform.position);
                GameManager.Instance.PlayerInfo.SwordNum = Mathf.Min(4, GameManager.Instance.PlayerInfo.SwordNum + 1);
            }            
        }
    }
}