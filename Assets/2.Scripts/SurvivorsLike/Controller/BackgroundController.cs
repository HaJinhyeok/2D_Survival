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
        _width = Backgrounds[1].transform.position.x;
        _height = Backgrounds[1].transform.position.y;
        _target = ObjectManager.Instance.Player.transform;
        
    }

    void Update()
    {
        // 백그라운드 포지션 0(좌상단), 1(우상단), 2(좌하단), 3(우하단)
        // 플레이어 포지션이 좌측 쏠릴 때
        if (_target.position.x < Backgrounds[0].transform.position.x)
        {
            float newX = Backgrounds[0].transform.position.x - _width;
            // 우선 1과 3을 좌로 translate한 뒤
            Backgrounds[1].transform.position = new Vector2(newX, Backgrounds[1].transform.position.y);
            Backgrounds[3].transform.position = new Vector2(newX, Backgrounds[3].transform.position.y);
            // 0, 2와 1, 3  인덱스의 배열 내용 스왑
            SwapBackground(0, 1);
            SwapBackground(2, 3);
        }

        // 플레이어 포지션이 우측 쏠릴 때
        else if (_target.position.x > Backgrounds[1].transform.position.x)
        {
            float newX = Backgrounds[1].transform.position.x + _width;
            // 우선 0과 2를 우로 translate한 뒤
            Backgrounds[0].transform.position = new Vector2(newX, Backgrounds[0].transform.position.y);
            Backgrounds[2].transform.position = new Vector2(newX, Backgrounds[2].transform.position.y);
            // 0, 2와 1, 3  인덱스의 배열 내용 스왑
            SwapBackground(0, 1);
            SwapBackground(2, 3);
        }

        // 플레이어 포지션이 상측 쏠릴 때
        if (_target.position.y > Backgrounds[0].transform.position.y)
        {
            float newY = Backgrounds[0].transform.position.y + _height;
            // 우선 2와 3을 위로 translate한 뒤
            Backgrounds[2].transform.position = new Vector2(Backgrounds[2].transform.position.x, newY);
            Backgrounds[3].transform.position = new Vector2(Backgrounds[3].transform.position.x, newY);
            // 0, 1 과 2, 3 인덱스의 배열 내용 스왑
            SwapBackground(0, 2);
            SwapBackground(1, 3);
        }

        // 플레이어 포지션이 하측 쏠릴 때
        else if (_target.position.y < Backgrounds[2].transform.position.y)
        {
            float newY = Backgrounds[2].transform.position.y - _height;
            // 우선 0과 1을 아래로 translate한 뒤
            Backgrounds[0].transform.position = new Vector2(Backgrounds[0].transform.position.x, newY);
            Backgrounds[1].transform.position = new Vector2(Backgrounds[1].transform.position.x, newY);
            // 0, 1 과 2, 3 인덱스의 배열 내용 스왑
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
