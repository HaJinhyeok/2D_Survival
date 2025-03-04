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
        if (collision.gameObject.CompareTag(Define.PlayerTag) && ObjectManager.Instance.Player.IsBlinking == false)
        {
            // 플레이어 체력 감소 및 타격 애니메이션 추가
            ObjectManager.Instance.Player.IsBlinking = true;
            StartCoroutine(ObjectManager.Instance.Player.GetAttack(Define.BlinkCount));
            ObjectManager.Instance.Player.IsBlinking = false;

            gameObject.SetActive(false);
            GameManager.Instance.PlayerHp -= _atk;
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
        if (rand == 0)
        {
            // Letter
            ObjectManager.Instance.Spawn<LetterController>(transform.position);
        }
        else if (rand >= 11 && rand <= 12)
        {
            // Pickaxe
            ObjectManager.Instance.Spawn<Pickaxe>(transform.position);
        }
        else if (rand >= 21 && rand <= 23)
        {
            // Sword
            ObjectManager.Instance.Spawn<Sword>(transform.position);
        }
        else if (rand >= 31 && rand <= 35)
        {
            // Coin
            // ObjectManager.Instance.Spawn<Coin>(transform.position);
            PoolManager.Instance.GetObject<Coin>(transform.position);
        }

        return true;
    }
}
