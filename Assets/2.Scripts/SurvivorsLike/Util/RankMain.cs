using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;

public class RankMain : MonoBehaviour
{
    string host= "http://localhost";
    int port= 3030;
    //string top3Uri= "scores/top3";
    //string idUri= "scores/Jinhyeok";
    string postUri="register";

    void Start()
    {
        //this.btnGetTop3.onClick.AddListener(() =>
        //{
        //    var url = string.Format("{0}:{1}/{2}", host, port, top3Uri);
        //    Debug.Log(url);

        //    StartCoroutine(this.GetTop3(url, (raw) =>
        //    {
        //        var res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores_top3>(raw);
        //        Debug.LogFormat("{0}, {1}", res.cmd, res.result.Length);
        //        foreach (var user in res.result)
        //        {
        //            Debug.LogFormat("{0} : {1}", user.id, user.score);
        //        }
        //    }));


        //});

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

        GameManager.Instance.OnGameOver += PostGameData;

    }

    void PostGameData()
    {
        var url = string.Format("{0}:{1}/{2}", host, port, postUri);
        Debug.Log(url); //http://localhost:3030/scores

        var req = new Protocols.Packets.req_scores();
        req.cmd = 1000; //(int)Protocols.eType.POST_SCORE;
        req.id = GameManager.Instance.PlayerInfo.PlayerName;
        req.score = GameManager.Instance.Score;

        //����ȭ  (������Ʈ -> ���ڿ�)
        var json = JsonConvert.SerializeObject(req);
        Debug.Log(json);
        //{"id":"hong@nate.com","score":100,"cmd":1000}

        StartCoroutine(this.PostScore(url, json, (raw) => {
            Protocols.Packets.res_scores res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores>(raw);
            Debug.LogFormat("{0}, {1}", res.cmd, res.message);
        }));
    }

    private IEnumerator GetTop3(string url, System.Action<string> callback)
    {

        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("��Ʈ��ũ ȯ���� �����Ƽ� ����� �Ҽ� �����ϴ�.");
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
            Debug.Log("��Ʈ��ũ ȯ���� �����Ƽ� ����� �Ҽ� �����ϴ�.");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator PostScore(string url, string json, System.Action<string> callback)
    {
        // url �ּҿ� POST ������� ��û
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json); //����ȭ (���ڿ� -> ����Ʈ �迭)

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(webRequest.result);
            Debug.Log("��Ʈ��ũ ȯ���� �����Ƽ� ����� �Ҽ� �����ϴ�.");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }

}