using UnityEngine;

public class Magnet : BaseController
{
    protected override void Initialize()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            ObjectManager.Instance.Player.StartPullAllItems();
            Destroy(gameObject);
        }
    }
}
