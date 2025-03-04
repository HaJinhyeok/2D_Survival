using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUp : MonoBehaviour
{
    public Button Perks1Button;
    public Button Perks2Button;
    public Button Perks3Button;
       
    // UI â�� ���� ������ �ٸ� Ư���� ���̵���
    private void OnEnable()
    {
        Perks1Button.GetComponentInChildren<TMP_Text>().text = Define.SpeedUp;
        Perks1Button.onClick.AddListener(SpeedUp);

        Perks2Button.GetComponentInChildren<TMP_Text>().text = Define.HpUp;
        Perks2Button.onClick.AddListener(HpUp);

        Perks3Button.GetComponentInChildren<TMP_Text>().text = Define.AddSword;
        Perks3Button.onClick.AddListener(AddSword);
    }

    void SpeedUp()
    {
        GameManager.Instance.PlayerInfo.Speed += 1;
    }

    void HpUp()
    {
        GameManager.Instance.PlayerInfo.MaxHp += 5;
        GameManager.Instance.PlayerHp += 5;
    }

    // �̹� Sword ������ 4���̸� Ư���� �������� �ʵ��� ����
    void AddSword()
    {
        if (GameManager.Instance.PlayerInfo.SwordNum < 4)
        {
            PoolManager.Instance.GetObject<SwordController>(ObjectManager.Instance.Player.transform.position);
            // ObjectManager.Instance.Spawn<SwordController>(ObjectManager.Instance.Player.transform.position);
            GameManager.Instance.PlayerInfo.SwordNum = Mathf.Min(4, GameManager.Instance.PlayerInfo.SwordNum + 1);
        }
    }
}
