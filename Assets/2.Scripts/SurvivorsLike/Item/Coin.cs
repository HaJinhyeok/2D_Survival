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
            AudioManager.Instance.CoinSound.Play();
            GameManager.Instance.Money += 5;
            ObjectManager.Instance.DeSpwan(this);
        }
    }
}
