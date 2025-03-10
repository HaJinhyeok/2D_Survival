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
            if (GameManager.Instance.PlayerInfo.CurrentHp <= 0)
            {
                RankMain.Instance.PostGameData();
                UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvResultScene);
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
        // Bread: 0.1%
        // Coin: 1%
        // Exp: 20%
        int rand = Random.Range(0, 1000);

        if (rand >= 30 && rand < 40)
        {
            // Coin
            PoolManager.Instance.GetObject<Coin>(transform.position);
        }
        else if (rand == 50)
        {
            ObjectManager.Instance.Spawn<Bread>(transform.position);
        }
        else if (rand >= 100 && rand < 300)
        {
            PoolManager.Instance.GetObject<ExpItem>(transform.position);
        }

        return true;
    }
}
