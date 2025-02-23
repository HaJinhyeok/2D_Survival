using UnityEngine;
using System.Collections.Generic;

public class VamShotProcess : MonoBehaviour
{
    public GameObject Shot;
    public List<GameObject> ShotList = new List<GameObject>();

    GameObject _shotPool;

    float _interval = 0.05f;
    float _coolTime = 0;

    void Start()
    {
        _shotPool = new GameObject("ShotPool");
    }

    void Update()
    {
        _coolTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (_coolTime > _interval)
            {
                _coolTime = 0;
                GetShot();
            }
        }
    }

    void GetShot()
    {
        Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < ShotList.Count; i++)
        {
            if (!ShotList[i].activeSelf)
            {
                ShotList[i].SetActive(true);
                ShotList[i].transform.position = transform.position;
                ShotList[i].GetComponent<Rigidbody2D>().AddForce((mousePos - currPos).normalized * 300);
                return;
            }
        }

        GameObject shot = Instantiate(Shot);
        shot.transform.parent = _shotPool.transform;
        shot.transform.position = transform.position;
        shot.GetComponent<Rigidbody2D>().AddForce((mousePos - currPos).normalized * 300);
        ShotList.Add(shot);
    }
}
