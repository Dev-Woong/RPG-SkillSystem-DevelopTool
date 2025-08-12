using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    Rigidbody2D _rb;
    public float Speed = 2f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Movement()
    {
        if (Input.GetButton("Horizontal")||Input.GetButton("Vertical"))
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector2 _moveDir = new Vector2(h,v);
            transform.Translate(Time.deltaTime * Speed * _moveDir.normalized);
            if (h < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else { transform.localScale = new Vector3(1, 1, 1); }
        }
    }
    void Update()
    {
        Movement();
    }
}
