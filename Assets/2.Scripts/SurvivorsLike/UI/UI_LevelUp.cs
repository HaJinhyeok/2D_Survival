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
        SetButton(Perks1Button, Define.SpeedUp);
        SetButton(Perks2Button, Define.HpUp);
        SetButton(Perks3Button, Define.AddSword);
    }

    void SetButton(Button button, string perkName)
    {
        button.GetComponentInChildren<TMP_Text>().text = Perks.PerkDictionary[perkName].Name;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Perks.PerkDictionary[perkName].PerkEffect);
        button.onClick.AddListener(DeactivatePanel);

        button.GetComponentInChildren<UI_Panel>().GetComponentInChildren<TMP_Text>().text
            = $": -{Perks.CostDictionary[perkName]}";
    }

    void DeactivatePanel()
    {
        if (GameManager.Instance.IsDone)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            GameManager.Instance.IsPaused = false;
        }
    }
}
