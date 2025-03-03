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
            if(GameManager.Instance.PlayerInfo.CurrentHp<=0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvMainScene);
            }
        }
    }

    protected abstract void ChasePlayer();

    public bool GetDamage(float damage, GameObject damageCauser, Vector2 hitPoint = default)
    {
        _hp -= damage;
        if(_hp <= 0)
        {
            DropRandomItem();
            ObjectManager.Instance.DeSpwan(this);
        }
        return true;
    }

    public bool DropRandomItem()
    {
        // Nothing: 88%
        // Coin: 5%
        // Sword: 3%
        // Pickaxe: 3%
        // Letter: 1%
        int rand = Random.Range(0, 100);
        if (rand == 0)
        {
            // Letter
            ObjectManager.Instance.Spawn<LetterController>(transform.position);
        }
        else if (rand >= 1 && rand <= 3)
        {
            // Pickaxe
            ObjectManager.Instance.Spawn<PickaxeController>(transform.position);
        }
        else if (rand >= 4 && rand <= 6)
        {
            // Sword
            ObjectManager.Instance.Spawn<SwordController>(transform.position);
        }
        else if (rand >= 7 && rand <= 11)
        {
            // Coin
            ObjectManager.Instance.Spawn<Coin>(transform.position);
        }
        else
        {
            // Nothing
        }

        return true;
    }
}
