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
        int FreePerkIdx1 = Random.Range(0, Perks.FreePerkNum);
        int FreePerkIdx2;
        do
        {
            FreePerkIdx2 = Random.Range(0, Perks.FreePerkNum);
        } while (FreePerkIdx2 == FreePerkIdx1);
        SetButton(Perks1Button, Define.PerkNameList[FreePerkIdx1]);
        SetButton(Perks2Button, Define.PerkNameList[FreePerkIdx2]);

        int NonFreePerkIdx = Random.Range(Perks.FreePerkNum, Define.PerkNameList.Length);
        SetButton(Perks3Button, Define.PerkNameList[NonFreePerkIdx]);
    }

    void SetButton(Button button, string perkName)
    {
        button.GetComponentInChildren<TMP_Text>().text = Perks.PerkDictionary[perkName].Name;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Perks.PerkDictionary[perkName].PerkEffect);
        button.onClick.AddListener(DeactivatePanel);

        button.GetComponentInChildren<UI_Panel>().GetComponentInChildren<TMP_Text>().text
            = $": {Perks.CostDictionary[perkName]}";
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
