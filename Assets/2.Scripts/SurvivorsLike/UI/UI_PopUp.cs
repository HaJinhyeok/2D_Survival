using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_PopUp : MonoBehaviour
{

    public TMP_Text PopUpText;

    public static Action<string> PopUpAction;

    private void Awake()
    {
        gameObject.SetActive(false);
        PopUpAction += PopUpWarning;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        PopUpText.color = Color.red;
        StartCoroutine(CoVanishPopUp());
    }

    void PopUpWarning(string message)
    {
        PopUpText.text = message;
        gameObject.SetActive(true);
    }

    IEnumerator CoVanishPopUp()
    {
        Color color = PopUpText.color;
        color.a -= 0.1f;
        PopUpText.color = color;
        if (color.a <= 0)
        {
            gameObject.SetActive(false);
            yield break;
        }
        yield return new WaitForSecondsRealtime(0.2f);
        StartCoroutine(CoVanishPopUp());
    }
}
