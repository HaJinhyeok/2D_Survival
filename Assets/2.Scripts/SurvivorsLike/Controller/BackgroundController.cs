using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject[] Backgrounds = new GameObject[4];

    // ���� ī�޶� ���� �ʺ�� ���� > ?
    // ��׶��� �ϳ��� �ʺ�� ����
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
        // ��׶��� ������ 0(�»��), 1(����), 2(���ϴ�), 3(���ϴ�)
        // �÷��̾� �������� ���� �� ��
        if (_target.position.x < Backgrounds[0].transform.position.x)
        {
            float newX = Backgrounds[0].transform.position.x - _width;
            // �켱 1�� 3�� �·� translate�� ��
            Backgrounds[1].transform.position = new Vector2(newX, Backgrounds[1].transform.position.y);
            Backgrounds[3].transform.position = new Vector2(newX, Backgrounds[3].transform.position.y);
            // 0, 2�� 1, 3  �ε����� �迭 ���� ����
            SwapBackground(0, 1);
            SwapBackground(2, 3);
        }

        // �÷��̾� �������� ���� �� ��
        else if (_target.position.x > Backgrounds[1].transform.position.x)
        {
            float newX = Backgrounds[1].transform.position.x + _width;
            // �켱 0�� 2�� ��� translate�� ��
            Backgrounds[0].transform.position = new Vector2(newX, Backgrounds[0].transform.position.y);
            Backgrounds[2].transform.position = new Vector2(newX, Backgrounds[2].transform.position.y);
            // 0, 2�� 1, 3  �ε����� �迭 ���� ����
            SwapBackground(0, 1);
            SwapBackground(2, 3);
        }

        // �÷��̾� �������� ���� �� ��
        if (_target.position.y > Backgrounds[0].transform.position.y)
        {
            float newY = Backgrounds[0].transform.position.y + _height;
            // �켱 2�� 3�� ���� translate�� ��
            Backgrounds[2].transform.position = new Vector2(Backgrounds[2].transform.position.x, newY);
            Backgrounds[3].transform.position = new Vector2(Backgrounds[3].transform.position.x, newY);
            // 0, 1 �� 2, 3 �ε����� �迭 ���� ����
            SwapBackground(0, 2);
            SwapBackground(1, 3);
        }

        // �÷��̾� �������� ���� �� ��
        else if (_target.position.y < Backgrounds[2].transform.position.y)
        {
            float newY = Backgrounds[2].transform.position.y - _height;
            // �켱 0�� 1�� �Ʒ��� translate�� ��
            Backgrounds[0].transform.position = new Vector2(Backgrounds[0].transform.position.x, newY);
            Backgrounds[1].transform.position = new Vector2(Backgrounds[1].transform.position.x, newY);
            // 0, 1 �� 2, 3 �ε����� �迭 ���� ����
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
