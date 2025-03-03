using UnityEngine;

public class Coin : BaseController
{
    protected override void Initialize()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            GameManager.Instance.Money += 5;
            Destroy(gameObject);
        }
    }
}
