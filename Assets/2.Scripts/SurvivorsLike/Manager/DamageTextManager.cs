using TMPro;
using UnityEngine;

public class DamageTextManager : Singleton<DamageTextManager>
{
    Canvas _canvas;
    GameObject _informationPanel;
    GameObject _damageText;

    protected override void Initialize()
    {
        //_canvas = GameObject.Find(Define.GameUIName).GetComponent<Canvas>();
        _informationPanel = GameObject.Find(Define.InformationPanel);
        _damageText = Resources.Load<GameObject>(Define.DamageTextPath);
    }

    public void CreateDamageText(Vector3 position, int damage)
    {
        //if (_canvas == null)
        //{
        //    _canvas = GameObject.Find(Define.GameUIName).GetComponent<Canvas>();
        //}
        if (_informationPanel == null)
        {
            _informationPanel = GameObject.Find(Define.InformationPanel);
        }
        GameObject damageText = Instantiate(_damageText, position, Quaternion.identity, _informationPanel.transform);
        damageText.GetComponent<TMP_Text>().text = damage.ToString();
    }
}
