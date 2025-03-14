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
        RankMain.Instance.PostTop3Data(TopRankText);
    }
}
