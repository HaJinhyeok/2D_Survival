using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    // ������ ���� �� �����۵��� '����'�ϴ� ��

    Dictionary<System.Type, List<GameObject>> _pooledObject =
        new Dictionary<System.Type, List<GameObject>>();
    Dictionary<System.Type, GameObject> _parentObject =
        new Dictionary<System.Type, GameObject>();

    public T GetObject<T>(Vector3 pos) where T : BaseController
    {
        System.Type type = typeof(T);
        if (type.Equals(typeof(DogController)) || type.Equals(typeof(HoodController)) || type.Equals(typeof(SlimeController)) || type.Equals(typeof(Coin)))
        {
            // ������Ʈ Ǯ�� �ش� Ÿ�� ������Ʈ ��ųʸ��� �����ϸ�
            if (_pooledObject.ContainsKey(type))
            {
                for (int i = 0; i < _pooledObject[type].Count; i++)
                {
                    if (!_pooledObject[type][i].activeSelf)
                    {
                        _pooledObject[type][i].SetActive(true);
                        _pooledObject[type][i].transform.position = pos;
                        return _pooledObject[type][i].GetComponent<T>();
                    }
                }

                var obj = ObjectManager.Instance.Spawn<T>(pos);
                obj.transform.parent = _parentObject[type].transform;
                _pooledObject[type].Add(obj.gameObject);
                return obj;
            }
            // �������� ������
            else
            {
                // parent object ��Ͽ� ������ ����
                if (!_parentObject.ContainsKey(type))
                {
                    GameObject go = new GameObject(type.Name);
                    _parentObject.Add(type, go);
                }
                var obj = ObjectManager.Instance.Spawn<T>(pos);
                obj.transform.parent = _parentObject[type].transform;
                List<GameObject> newList = new List<GameObject>();
                newList.Add(obj.gameObject);
                _pooledObject.Add(type, newList);
                return obj;
            }
        }
        else if (type.Equals(typeof(SwordController)))
        {
            int currentSwordNum = GameManager.Instance.PlayerInfo.SwordNum;
            SwordController swordController;
            if (currentSwordNum >= 4)
                return null;
            if (_pooledObject.ContainsKey(type))
            {
                // �� �� �ҵ��� ���� ����
                float angle = _pooledObject[type][0].GetComponent<SwordController>().Angle;
                float angleDiff = 360f / (currentSwordNum + 1);

                for (int i = 0; i <= currentSwordNum; i++)
                {
                    if (i == currentSwordNum)
                    {
                        swordController = ObjectManager.Instance.Spawn<SwordController>(pos);
                        swordController.transform.parent = _parentObject[type].transform;
                        swordController.Angle = (angle + i * angleDiff) % 360f;
                        _pooledObject[type].Add(swordController.gameObject);
                    }
                    else
                    {
                        // ���� �ҵ�� ���� ����
                        swordController = _pooledObject[type][i].GetComponent<SwordController>();
                        swordController.Angle = (angle + i * angleDiff) % 360f;
                    }
                }

                return _pooledObject[type][currentSwordNum] as T;
            }
            // ù �ҵ� ������ ��
            else
            {
                if (!_parentObject.ContainsKey(type))
                {
                    GameObject go = new GameObject(type.Name);
                    _parentObject.Add(type, go);
                }
                SwordController obj = ObjectManager.Instance.Spawn<SwordController>(pos);
                obj.transform.parent = _parentObject[type].transform;
                List<GameObject> newList = new List<GameObject>();
                newList.Add(obj.gameObject);
                _pooledObject.Add(type, newList);
                return obj as T;
            }
        }

        return null;
    }

    public void PullItems(GameObject origin, float distance, float speed)
    {
        foreach (var item in _pooledObject)
        {
            if(item.Key.Equals(typeof(Coin)))
            {
                foreach(var item2 in item.Value)
                {
                    Vector2 direction = origin.transform.position - item2.transform.position;
                    if (direction.sqrMagnitude < distance)
                    {
                        item2.transform.Translate(direction.normalized * speed * Time.deltaTime);
                    }
                }
            }

        }
    }
}