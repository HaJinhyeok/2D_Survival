using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public abstract class EnemyController : BaseController, IDamageable, IDroppable
{
    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;
    private Material _whiteMaterial;
    private Material _originalMaterial;
    private IEnumerator _coroutine;

    protected Transform _target;
    protected float _atk = 1;
    protected float _speed = 3;
    [SerializeField]
    protected float _hp;
    protected bool _isAttacked;
    protected bool _isGolem;
    protected float _coolTime = 0f;
    protected const float _interval = 0.1f;

    protected override void Initialize()
    {
        _target = ObjectManager.Instance.Player.transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
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

            if (_isGolem)
            {
                _animator.SetTrigger("Die");
            }
            else
            {
                ObjectManager.Instance.DeSpwan(this);
            }

            // 게임 종료
            if (GameManager.Instance.PlayerInfo.CurrentHp <= 0 && GameManager.Instance.IsGameOver == false)
            {
                UI_Game.GameOverAction();
            }
        }
        //else if (collision.gameObject.CompareTag(Define.ShotTag) && !_isAttacked)
        else if (collision.gameObject.CompareTag(Define.ShotTag))
        {
            //_isAttacked = true;
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
            StartCoroutine("CoFlashWhite");
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
        // Exp
        else if (rand >= 100 && rand < 300)
        {
            if (LevelManager.Instance.LevelInfo.Level > 5 && LevelManager.Instance.LevelInfo.Level <= 10)
            {
                // 3 : 1
                if (rand >= 100 && rand < 250)
                {
                    PoolManager.Instance.GetObject<Exp_Lv1>(transform.position);
                }
                else
                {
                    PoolManager.Instance.GetObject<Exp_Lv2>(transform.position);
                }
            }
            else if (LevelManager.Instance.LevelInfo.Level > 10 && LevelManager.Instance.LevelInfo.Level <= 20)
            {
                // 10 : 7 : 3
                if (rand >= 100 && rand < 200)
                {
                    PoolManager.Instance.GetObject<Exp_Lv1>(transform.position);
                }
                else if (rand >= 200 && rand < 270)
                {
                    PoolManager.Instance.GetObject<Exp_Lv2>(transform.position);
                }
                else
                {
                    PoolManager.Instance.GetObject<Exp_Lv3>(transform.position);
                }
            }
            else if (LevelManager.Instance.LevelInfo.Level > 20 && LevelManager.Instance.LevelInfo.Level <= 30)
            {
                // 9 : 7 : 3 : 1
                if (rand >= 100 && rand < 190)
                {
                    PoolManager.Instance.GetObject<Exp_Lv1>(transform.position);
                }
                else if (rand >= 190 && rand < 260)
                {
                    PoolManager.Instance.GetObject<Exp_Lv2>(transform.position);
                }
                else if (rand >= 260 && rand < 290)
                {
                    PoolManager.Instance.GetObject<Exp_Lv3>(transform.position);
                }
                else
                {
                    PoolManager.Instance.GetObject<Exp_Lv4>(transform.position);
                }
            }
            else if (LevelManager.Instance.LevelInfo.Level > 30)
            {
                // 20 : 15 : 10 : 4 : 1
                if (rand >= 100 && rand < 180)
                {
                    PoolManager.Instance.GetObject<Exp_Lv1>(transform.position);
                }
                else if (rand >= 180 && rand < 240)
                {
                    PoolManager.Instance.GetObject<Exp_Lv2>(transform.position);
                }
                else if (rand >= 240 && rand < 280)
                {
                    PoolManager.Instance.GetObject<Exp_Lv3>(transform.position);
                }
                else if (rand >= 280 && rand < 296)
                {
                    PoolManager.Instance.GetObject<Exp_Lv4>(transform.position);
                }
                else
                {
                    PoolManager.Instance.GetObject<Exp_Lv5>(transform.position);
                }
            }
            else
            {
                PoolManager.Instance.GetObject<Exp_Lv1>(transform.position);
            }
        }

        return true;
    }
}
