using UnityEngine;

public class GolemShotController : BaseController
{
    Vector2 _moveDir;
    float _atk = 10f;

    protected override void Initialize()
    {
        
    }

    private void OnEnable()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y) + _moveDir * 7f * Time.deltaTime;
        transform.position = newPos;
    }

    public void SetDirection(Vector2 dir)
    {
        _moveDir = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            ObjectManager.Instance.Player.GetAttack();
            GameManager.Instance.PlayerHp = Mathf.Max(0, GameManager.Instance.PlayerHp - _atk);
            Destroy(gameObject);

            // 게임 종료
            if (GameManager.Instance.PlayerInfo.CurrentHp <= 0 && GameManager.Instance.IsGameOver == false)
            {
                UI_Game.GameOverAction();
            }
        }
    }
}
