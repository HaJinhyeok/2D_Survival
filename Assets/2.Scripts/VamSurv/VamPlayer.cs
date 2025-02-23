using UnityEngine;

public class VamPlayer : MonoBehaviour
{
    public GameObject Shot;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        // Fire();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(h, v, 0);
        transform.Translate(movement.normalized * 5 * Time.deltaTime);
    }

    void Rotation()
    {
        // ���콺 ��ġ �ٶ󺸰�
    }

    void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject shot = Instantiate(Shot, transform.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().AddForce((mousePos - currPos).normalized * 300f);
        }
    }
}
