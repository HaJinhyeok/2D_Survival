using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;
using UnityEditor.Experimental.GraphView;
using System.Threading.Tasks;

public class RankMain : Singleton<RankMain>
{
    string host = "http://localhost";
    int port = 3030;
    string top3Uri = "top3";
    //string idUri= "scores/Jinhyeok";
    string postUri = "register";

    void Awake()
    {
        //this.btnGetId.onClick.AddListener(() =>
        //{
        //    var url = string.Format("{0}:{1}/{2}", host, port, idUri);
        //    Debug.Log(url);

        //    StartCoroutine(this.GetId(url, (raw) =>
        //    {

        //        var res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores_id>(raw);
        //        Debug.LogFormat("{0}, {1}", res.result.id, res.result.score);

        //    }));
        //});

        //GameManager.Instance.OnGameOver ??= PostGameData;
        //GameManager.Instance.OnScoreBoard ??= PostTop3Data;
    }


    public void PostTop3Data()
    {
        var url = string.Format("{0}:{1}/{2}", host, port, top3Uri);
        Debug.Log(url);

        StartCoroutine(this.GetTop3(url, (raw) =>
        {
            Protocols.Packets.res_scores_top3 res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores_top3>(raw);
            Debug.LogFormat("{0}, {1}", res.cmd, res.message);
            GameManager.Instance.users = res.result;
            foreach (var user in res.result)
            {
                Debug.LogFormat("{0} : {1}", user.id, user.score);
            }

        }));
    }
    public void PostGameData()
    {
        var url = string.Format("{0}:{1}/{2}", host, port, postUri);
        Debug.Log(url); //http://localhost:3030/scores

        var req = new Protocols.Packets.req_scores();
        req.cmd = 1000; //(int)Protocols.eType.POST_SCORE;
        req.id = GameManager.Instance.PlayerInfo.PlayerName;
        req.score = GameManager.Instance.Score;

        //직렬화  (오브젝트 -> 문자열)
        var json = JsonConvert.SerializeObject(req);
        Debug.Log(json);
        //{"id":"hong@nate.com","score":100,"cmd":1000}

        StartCoroutine(this.PostScore(url, json, (raw) =>
        {
            Protocols.Packets.res_scores res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores>(raw);
            Debug.LogFormat("Hello1 {0}, {1}", res.cmd, res.message);
        }));
    }

    private IEnumerator GetTop3(string url, System.Action<string> callback)
    {

        var webRequest = new UnityWebRequest(url, "POST");

        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(webRequest.result);
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator GetId(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        Debug.Log("--->" + webRequest.downloadHandler.text);

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator PostScore(string url, string json, System.Action<string> callback)
    {
        // url 주소에 POST 방식으로 요청
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json); //직렬화 (문자열 -> 바이트 배열)

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(webRequest.result);
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            // Debug.LogFormat("Hello 123\n{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }

}