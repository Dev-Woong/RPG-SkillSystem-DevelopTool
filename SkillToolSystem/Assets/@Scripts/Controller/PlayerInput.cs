using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 2f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Movement()
    {
        if (Input.GetButton("Horizontal")||Input.GetButton("Vertical"))
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector2 moveDir = new Vector2(h,v);
            transform.Translate(Time.deltaTime*speed*moveDir);
            if (h < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else { transform.localScale = new Vector3(1, 1, 1); }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
