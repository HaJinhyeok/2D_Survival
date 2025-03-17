using TMPro;
using UnityEngine;

public class DamageTextManager : Singleton<DamageTextManager>
{
    GameObject _informationPanel;
    GameObject _damageText;

    protected override void Initialize()
    {
        _informationPanel = GameObject.Find(Define.InformationPanel);
        _damageText = Resources.Load<GameObject>(Define.DamageTextPath);
    }

    public void CreateDamageText(Vector3 position, int damage)
    {
        if (_informationPanel == null)
        {
            _informationPanel = GameObject.Find(Define.InformationPanel);
        }
        GameObject damageText = Instantiate(_damageText, position, Quaternion.identity, _informationPanel.transform);
        damageText.GetComponent<TMP_Text>().text = damage.ToString();
    }
}
