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
            // �÷��̾� ü�� ���� �� Ÿ�� �ִϸ��̼� �߰�
            ObjectManager.Instance.Player.GetAttack();

            gameObject.SetActive(false);
            GameManager.Instance.PlayerHp -= _atk;
            // ���� ����
            if (GameManager.Instance.PlayerInfo.CurrentHp <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvMainScene);
            }
        }
        else if (collision.gameObject.CompareTag(Define.ShotTag))
        {
            GetDamage(3, collision.gameObject);
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
        // Letter: 1%
        // Pickaxe: 2%
        // Sword: 3%
        // Coin: 5%
        // Nothing: else
        int rand = Random.Range(0, 100);

        if (rand >= 31 && rand <= 35)
        {
            // Coin
            // ObjectManager.Instance.Spawn<Coin>(transform.position);
            PoolManager.Instance.GetObject<Coin>(transform.position);
        }

        return true;
    }
}
