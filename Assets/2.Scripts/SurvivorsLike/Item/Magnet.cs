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
            AudioManager.Instance.MagnetSound.Play();
            ObjectManager.Instance.Player.StartPullAllItems();
            Destroy(gameObject);
        }
    }
}
