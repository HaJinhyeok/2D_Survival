using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUp : MonoBehaviour
{
    public Button Perks1Button;
    public Button Perks2Button;
    public Button Perks3Button;
       
    // UI 창이 열릴 때마다 다른 특전이 보이도록
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

    // 이미 Sword 개수가 4개이면 특전이 등장하지 않도록 설정
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
