using System.Collections;
using UnityEngine;

public class PlayerController : BaseController
{
    public GameObject Shot;
    public GameObject Pickaxe;

    bool _isExplosionActivated = false;
    float _explosionRadius = 5f;
    Vector2 _moveDir;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    protected override void Initialize()
    {
        GameManager.Instance.OnMoveDirChanged += (dir) => { _moveDir = dir; };
        StartCoroutine(CoThrowPickaxe());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(_moveDir * 5 * Time.deltaTime);
    }

    void Rotation()
    {
        // 마우스 위치 바라보게
    }

    void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject shot = Instantiate(Shot, transform.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().AddForce((mousePos - currPos).normalized * 300f);
        }
    }

    IEnumerator CoThrowPickaxe()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            Instantiate(Pickaxe, transform.position, Quaternion.identity);
        }
        
    }

    public void StartExplosion()
    {
        if(!_isExplosionActivated)
        {
            _isExplosionActivated = true;
            StartCoroutine(CoLetterExplosion());
        }
    }

    IEnumerator CoLetterExplosion()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Explosion();
            // ObjectManager.Instance.Spawn<LetterController>(transform.position);
            ObjectManager.Instance.ExplosionEffect();
        }
    }

    void Explosion()
    {
        foreach (var item in ObjectManager.Instance.Enemies)
        {
            if ((transform.position - item.transform.position).sqrMagnitude < _explosionRadius * _explosionRadius)
            {
                ObjectManager.Instance.DeSpwan<EnemyController>(item);
                GameManager.Instance.GetScore();
                // item.gameObject.SetActive(false);
            }
        }
    }

    public void EndExplosion()
    {
        if(_isExplosionActivated)
        {
            _isExplosionActivated = false;
            StopCoroutine(CoLetterExplosion());
        }
    }
}
