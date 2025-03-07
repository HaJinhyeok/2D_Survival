using UnityEngine;
using System.Collections.Generic;

public class ShotProcess : MonoBehaviour
{
    public GameObject Shot;
    public List<GameObject> ShotList = new List<GameObject>();
    public float Speed = 600f;

    GameObject _shotPool;

    float _interval = 0.7f;
    float _coolTime = 0;
    // 36���� ź �߻��ϴ� ź��
    const int _shotNum = 36;

    void Start()
    {
        _shotPool = new GameObject("ShotPool");
    }

    void Update()
    {
        _coolTime += Time.deltaTime;
        if (_coolTime > _interval)
        {
            _coolTime = 0;
            GetShot();
        }
        
    }

    void GetShot()
    {
        Vector2 direction;
        int shotNum = _shotNum;

        // pool���� ��Ȱ��ȭ�� object ã�Ƽ� �켱 Ȱ��ȭ
        for (int i = 0; i < ShotList.Count; i++)
        {
            if (!ShotList[i].activeSelf)
            {
                direction = new Vector2(Mathf.Cos(shotNum * 10f * Mathf.Deg2Rad), Mathf.Sin(shotNum-- * 10f * Mathf.Deg2Rad));
                ShotList[i].SetActive(true);
                ShotList[i].transform.position = transform.position;
                ShotList[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * Speed);
            }
        }
        // ���� ������ ���� ����
        for (int i = 0; i < shotNum;)
        {
            direction = new Vector2(Mathf.Cos(shotNum * 10f * Mathf.Deg2Rad), Mathf.Sin(shotNum-- * 10f * Mathf.Deg2Rad));
            GameObject shot = Instantiate(Shot);
            shot.transform.parent = _shotPool.transform;
            shot.transform.position = transform.position;
            shot.GetComponent<Rigidbody2D>().AddForce(direction.normalized * Speed);
            ShotList.Add(shot);
        }

    }
}
