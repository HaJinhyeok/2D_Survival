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
        int PerkIdx1 = Random.Range(0, Define.PerkNameList.Length);
        int PerkIdx2 = Random.Range(0, Define.PerkNameList.Length);
        int PerkIdx3 = Random.Range(0, Define.PerkNameList.Length);
        while (PerkIdx2 == PerkIdx1)
        {
            PerkIdx2 = Random.Range(0, Define.PerkNameList.Length);
        }
        while (PerkIdx3 == PerkIdx1 || PerkIdx3 == PerkIdx2)
        {
            PerkIdx3 = Random.Range(0, Define.PerkNameList.Length);
        }
        SetButton(Perks1Button, Define.PerkNameList[PerkIdx1]);
        SetButton(Perks2Button, Define.PerkNameList[PerkIdx2]);
        SetButton(Perks3Button, Define.PerkNameList[PerkIdx3]);
    }

    void SetButton(Button button, string perkName)
    {
        button.GetComponentInChildren<TMP_Text>().text = Perks.PerkDictionary[perkName].Name;
        button.GetComponentsInChildren<Image>()[1].sprite =
            Resources.Load<Sprite>(Perks.PerkDictionary[perkName].ImagePath);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Perks.PerkDictionary[perkName].PerkEffect);
        button.onClick.AddListener(DeactivatePanel);
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
