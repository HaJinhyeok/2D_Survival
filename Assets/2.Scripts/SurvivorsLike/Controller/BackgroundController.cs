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
        _width = Backgrounds[0].GetComponent<RectTransform>().sizeDelta.x;
        _height = Backgrounds[0].GetComponent<RectTransform>().sizeDelta.y;
        _target = ObjectManager.Instance.Player.transform;
        
    }

    void Update()
    {
        // ��׶��� ������ 0(�»��), 1(����), 2(���ϴ�), 3(���ϴ�)
        // �÷��̾� �������� ���� �� ��
        if(_target.position.x)
        {
            // �켱 1�� 3�� �·� translate�� ��

        }

        // �÷��̾� �������� ���� �� ��
        else if(_target.position.x)
        {
            // �켱 0�� 2�� ��� translate�� ��

        }

        // �÷��̾� �������� ���� �� ��
        if(_target.position.y)
        {
            // �켱 2�� 3�� ���� translate�� ��

        }

        // �÷��̾� �������� ���� �� ��
        else if(_target.position.y)
        {
            // �켱 0�� 1�� �Ʒ��� translate�� ��
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
