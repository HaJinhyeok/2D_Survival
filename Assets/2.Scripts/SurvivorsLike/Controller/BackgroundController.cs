using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject[] Backgrounds = new GameObject[4];

    // 현재 카메라 뷰의 너비와 높이 > ?
    // 백그라운드 하나의 너비와 높이
    float _width, _height;
    Transform _target;

    void Start()
    {
        _width = Backgrounds[0].GetComponent<RectTransform>().sizeDelta.x;
        _height = Backgrounds[0].GetComponent<RectTransform>().sizeDelta.y;
        _target = ObjectManager.Instance.Player.transform;
        
    }

    void Update()
    {
        // 백그라운드 포지션 0(좌상단), 1(우상단), 2(좌하단), 3(우하단)
        // 플레이어 포지션이 좌측 쏠릴 때
        if(_target.position.x)
        {
            // 우선 1과 3을 좌로 translate한 뒤

        }

        // 플레이어 포지션이 우측 쏠릴 때
        else if(_target.position.x)
        {
            // 우선 0과 2를 우로 translate한 뒤

        }

        // 플레이어 포지션이 상측 쏠릴 때
        if(_target.position.y)
        {
            // 우선 2와 3을 위로 translate한 뒤

        }

        // 플레이어 포지션이 하측 쏠릴 때
        else if(_target.position.y)
        {
            // 우선 0과 1을 아래로 translate한 뒤
            SwapBackground(0, 2);
            SwapBackground(1, 3);
        }
    }

    void SwapBackground(int idx1, int idx2)
    {
        GameObject gameObject = Backgrounds[idx1];
        Backgrounds[idx1] = Backgrounds[idx2];
        Backgrounds[idx2] = gameObject;
    }
}
