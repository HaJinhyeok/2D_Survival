using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Top3 : MonoBehaviour
{
    public TMP_Text[] TopRankText;
    public Button QuitButton;

    private void Awake()
    {
        QuitButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    void OnEnable()
    {
        RankMain.Instance.PostTop3Data();
        for (int i = 0; i < GameManager.Instance.users.Length; i++)
        {
            TopRankText[i].text = $"{GameManager.Instance.users[i].id} : {GameManager.Instance.users[i].score}";
        }
    }
}
