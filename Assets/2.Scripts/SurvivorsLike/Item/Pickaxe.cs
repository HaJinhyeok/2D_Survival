using UnityEngine;

public class Pickaxe : BaseController
{
    protected override void Initialize()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            Destroy(gameObject);
            GameManager.Instance.PlayerInfo.AxeNum = Mathf.Min(GameManager.Instance.PlayerInfo.AxeNum + 1, 4);
        }
    }
}
