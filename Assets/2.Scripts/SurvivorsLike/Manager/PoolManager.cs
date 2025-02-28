using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    // 스폰된 몬스터 및 아이템들을 '관리'하는 곳

    Dictionary<System.Type, List<GameObject>> _pooledObject =
        new Dictionary<System.Type, List<GameObject>>();
    Dictionary<System.Type, GameObject> _parentObject=
        new Dictionary<System.Type, GameObject>();

    public T GetObject<T>(Vector3 pos) where T : BaseController
    {
        System.Type type = typeof(T);
        if(type.Equals(typeof(EnemyController)))
        {
            // 오브젝트 풀에 해당 타입 오브젝트 딕셔너리가 존재하면
            if(_pooledObject.ContainsKey(type))
            {
                for(int i = 0; i < _pooledObject[type].Count;i++)
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
            // 존재하지 않으면
            else
            {
                // parent object 목록에 없으면 생성
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

        return null;
    }
}
