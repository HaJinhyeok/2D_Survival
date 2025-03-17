using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public abstract class EnemyController : BaseController, IDamageable, IDroppable
{
    private SpriteRenderer _spriteRenderer;
    private Material _whiteMaterial;
    private Material _originalMaterial;
    private IEnumerator _coroutine;

    protected Transform _target;
    protected float _atk = 1;
    protected float _speed = 3;
    [SerializeField]
    protected float _hp;
    [SerializeField]
    protected bool _isAttacked;
    protected float _coolTime = 0f;
    protected const float _interval = 0.1f;

    public string Tag { get; set; } = Define.EnemyTag;

    protected override void Initialize()
    {
        _target = ObjectManager.Instance.Player.transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _whiteMaterial = Resources.Load<Material>(Define.WhiteMaterialPath);
        _originalMaterial = _spriteRenderer.material;
        _coroutine = CoFlashWhite();
    }

    private void OnEnable()
    {
        _spriteRenderer.material = _originalMaterial;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.PlayerTag))
        {
            // 플레이어 체력 감소 및 타격 애니메이션 추가
            ObjectManager.Instance.Player.GetAttack();
            GameManager.Instance.PlayerHp = Mathf.Max(0, GameManager.Instance.PlayerHp - _atk);

            StopCoroutine("CoFlashWhite");
            _spriteRenderer.material = _originalMaterial;
            ObjectManager.Instance.DeSpwan(this);

            // 게임 종료
            if (GameManager.Instance.PlayerInfo.CurrentHp <= 0 && GameManager.Instance.IsGameOver == false)
            {
                UI_Game.GameOverAction();
            }
        }
        else if (collision.gameObject.CompareTag(Define.ShotTag) && !_isAttacked)
        {
            _isAttacked = true;
            GetDamage(GameManager.Instance.PlayerInfo.Atk, collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    protected abstract void ChasePlayer();

    public bool GetDamage(float damage, GameObject damageCauser, Vector2 hitPoint = default)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        DamageTextManager.Instance.CreateDamageText(pos, (int)damage);
        _hp -= damage;
        if (gameObject.activeSelf)
        {
            StartCoroutine(_coroutine);

        }
        if (_hp <= 0)
        {
            DropRandomItem();
            StopCoroutine("CoFlashWhite");
            _spriteRenderer.material = _originalMaterial;
            ObjectManager.Instance.DeSpwan(this);
            GameManager.Instance.GetScore();
        }
        return true;
    }

    public IEnumerator CoFlashWhite()
    {
        _spriteRenderer.material = _whiteMaterial;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.material = _originalMaterial;
        yield return new WaitForSeconds(0.1f);
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
