using UnityEngine;

public abstract class EnemyController : BaseController, IDamageable, IDroppable
{
    protected Transform _target;
    protected float _atk = 1;
    protected float _speed = 3;
    protected float _hp = 3;

    public string Tag { get; set; } = Define.EnemyTag;

    protected override void Initialize()
    {
        _target = ObjectManager.Instance.Player.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.PlayerTag))
        {
            // 플레이어 체력 감소 및 타격 애니메이션 추가
            ObjectManager.Instance.Player.GetAttack();
            GameManager.Instance.PlayerHp = Mathf.Max(0, GameManager.Instance.PlayerHp - _atk);
            gameObject.SetActive(false);

            // 게임 종료
            if (GameManager.Instance.PlayerInfo.CurrentHp <= 0 && GameManager.Instance.IsGameOver == false)
            {
                UI_Game.GameOverAction();
            }
        }
        else if (collision.gameObject.CompareTag(Define.ShotTag))
        {
            GetDamage(GameManager.Instance.PlayerInfo.Atk, collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    protected abstract void ChasePlayer();

    public bool GetDamage(float damage, GameObject damageCauser, Vector2 hitPoint = default)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            DropRandomItem();
            ObjectManager.Instance.DeSpwan(this);
            GameManager.Instance.GetScore();
        }
        return true;
    }

    public bool DropRandomItem()
    {
        // Magnet: 0.1%
        // Bread: 0.2%
        // Coin: 1%
        // Exp: 20%
        int rand = Random.Range(0, 1000);

        if (rand == 500)
        {
            // Magnet
            ObjectManager.Instance.Spawn<Magnet>(transform.position);
        }
        else if (rand == 50 || rand == 51)
        {
            // Bread
            ObjectManager.Instance.Spawn<Bread>(transform.position);
        }
        else if (rand >= 30 && rand < 40)
        {
            // Coin
            PoolManager.Instance.GetObject<Coin>(transform.position);
        }
        else if (rand >= 100 && rand < 300)
        {
            // Exp1
            PoolManager.Instance.GetObject<Exp_Lv1>(transform.position);
        }
        else if (rand >= 300 && rand < 350)
        {
            if (LevelManager.Instance.LevelInfo.Level > 5)
            {
                PoolManager.Instance.GetObject<Exp_Lv2>(transform.position);
            }
        }
        else if (rand >= 350 && rand < 370)
        {
            if (LevelManager.Instance.LevelInfo.Level > 10)
            {
                PoolManager.Instance.GetObject<Exp_Lv3>(transform.position);
            }
        }

        return true;
    }
}
