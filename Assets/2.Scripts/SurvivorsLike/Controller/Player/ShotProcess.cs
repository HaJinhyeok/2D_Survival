using UnityEngine;
using System.Collections.Generic;

public class ShotProcess : MonoBehaviour
{
    public GameObject Shot;
    public List<GameObject> ShotList = new List<GameObject>();

    GameObject _shotPool;

    float _coolTime = 0;

    void Start()
    {
        _shotPool = new GameObject("ShotPool");
    }

    void Update()
    {
        _coolTime += Time.deltaTime;
        if (_coolTime > GameManager.Instance.ShotInfo.Interval)
        {
            _coolTime = 0;
            GetShot();
        }
        
    }

    void GetShot()
    {
        Vector2 direction;
        int shotNum = GameManager.Instance.ShotInfo.ShotNum;
        float angle = 360f / shotNum;

        // pool에서 비활성화인 object 찾아서 우선 활성화
        for (int i = 0; i < ShotList.Count; i++)
        {
            if (!ShotList[i].activeSelf)
            {
                direction = new Vector2(Mathf.Cos(shotNum * angle * Mathf.Deg2Rad), Mathf.Sin(shotNum-- * angle * Mathf.Deg2Rad));
                ShotList[i].SetActive(true);
                ShotList[i].transform.position = transform.position;
                ShotList[i].GetComponent<ShotController>().SetDirection(direction.normalized);
                //ShotList[i].GetComponent<Rigidbody2D>().AddForce(direction.normalized * GameManager.Instance.ShotInfo.Speed);
            }
        }
        // 남은 개수는 새로 생성
        for (int i = 0; i < shotNum;)
        {
            direction = new Vector2(Mathf.Cos(shotNum * angle * Mathf.Deg2Rad), Mathf.Sin(shotNum-- * angle * Mathf.Deg2Rad));
            GameObject shot = Instantiate(Shot);
            shot.transform.parent = _shotPool.transform;
            shot.transform.position = transform.position;
            shot.GetComponent<ShotController>().SetDirection(direction.normalized);
            //shot.GetComponent<Rigidbody2D>().AddForce(direction.normalized * GameManager.Instance.ShotInfo.Speed);
            ShotList.Add(shot);
        }

    }
}
