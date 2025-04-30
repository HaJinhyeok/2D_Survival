using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class RankMain : Singleton<RankMain>
{
    const int port = 3030;
    //readonly string _host = "http://localhost";
    //readonly string _host = "https://3.34.138.149";
    readonly string _host = "https://ec2-3-34-138-149.ap-northeast-2.compute.amazonaws.com/api";
    readonly string _top3Uri = "top3";
    readonly string _updateUri = "update";
    readonly string _loginUri = "login";
    readonly string _registerUri = "register";

    public static int CountPost = 0;

    public void PostLoginData()
    {
        Debug.Log("PostLogin");
        var url = string.Format($"{_host}:{port}/{_loginUri}");

        var req = new Protocols.Packets.req_login();
        req.cmd = 1000;
        req.id = GameManager.Instance.PlayerInfo.PlayerID;
        req.password = GameManager.Instance.PlayerInfo.PlayerPassword;

        var json = JsonConvert.SerializeObject(req);

        StartCoroutine(this.CoPost(url, json, (raw) =>
        {
            Protocols.Packets.res_message res = JsonConvert.DeserializeObject<Protocols.Packets.res_message>(raw);
            Debug.Log($"cmd: {res.cmd}, message: {res.message}");
            switch (res.cmd)
            {
                case 1201:
                    // 아이디 없을 경우
                    UI_PopUp.PopUpAction(Define.Warning_Inappropriate_PlayerID);
                    break;

                case 1202:
                    // 아이디는 존재하나 비밀번호가 틀린 경우
                    UI_PopUp.PopUpAction(Define.Warning_Inappropriate_PlayerPassword);
                    break;

                case 1203:
                    // 로그인 성공
                    UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvGameScene);
                    break;

                default:
                    break;
            }
        }));
    }

    public void PostRegisterData()
    {
        Debug.Log("PostRegister");
        var url = string.Format($"{_host}:{port}/{_registerUri}");

        var req = new Protocols.Packets.req_login();
        req.cmd = 1000;
        req.id = GameManager.Instance.PlayerInfo.PlayerID;
        req.password = GameManager.Instance.PlayerInfo.PlayerPassword;

        var json = JsonConvert.SerializeObject(req);

        StartCoroutine(this.CoPost(url, json, (raw) =>
        {
            Protocols.Packets.res_message res = JsonConvert.DeserializeObject<Protocols.Packets.res_message>(raw);
            Debug.Log($"cmd: {res.cmd}, message: {res.message}");
            switch (res.cmd)
            {
                case 1301:
                    // 신규 유저 등록
                    UI_PopUp.PopUpAction("등록 완료");
                    break;

                case 1302:
                    // 이미 존재하는 유저
                    UI_PopUp.PopUpAction(Define.Warning_Already_Exist_ID);
                    break;

                default:
                    break;
            }
        }));

    }

    public void PostTop3Data(TMP_Text[] TopRankText)
    {
        Debug.Log("PostTop3Data");
        var url = string.Format("{0}:{1}/{2}", _host, port, _top3Uri);

        StartCoroutine(this.CoPostTop3(url, (raw) =>
        {
            Protocols.Packets.res_scores_top3 res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores_top3>(raw);
            for (int i = 0; i < res.result.Length; i++)
            {
                TopRankText[i].text = $"{res.result[i].id} : {res.result[i].score}";
            }
        }));
    }

    public void PostUpdateData()
    {
        Debug.Log("PostGameData");
        var url = string.Format("{0}:{1}/{2}", _host, port, _updateUri);

        var req = new Protocols.Packets.req_scores();
        req.cmd = 1000;
        req.id = GameManager.Instance.PlayerInfo.PlayerID;
        req.score = GameManager.Instance.Score;

        //직렬화  (오브젝트 -> 문자열)
        var json = JsonConvert.SerializeObject(req);

        StartCoroutine(this.CoPost(url, json, (raw) =>
        {
            Protocols.Packets.res_message res = JsonConvert.DeserializeObject<Protocols.Packets.res_message>(raw);
        }));
    }

    private IEnumerator CoPost(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(webRequest.result);
            Debug.Log("네트워크 환경이 안좋아서 통신을 할 수 없습니다.");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }


    private IEnumerator CoPostTop3(string url, System.Action<string> callback)
    {

        var webRequest = new UnityWebRequest(url, "POST");

        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(webRequest.result);
            Debug.Log("네트워크 환경이 안좋아서 통신을 할 수 없습니다.");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }
}