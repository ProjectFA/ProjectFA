using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PalyerMove : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float speed = 10f;
    public float dash = 5f;

    public float jumpHeight = 3f;
    public float rotSpeed = 3f;

    private Vector3 dir = Vector3.zero;
    private bool ground = false;
    public LayerMask layer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();
        CheckGround();
        if (Input.GetButtonDown("Jump") && ground)
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rigidBody.AddForce(jumpPower, ForceMode.VelocityChange);
        }

        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashPower = this.transform.forward * dash;
            rigidBody.AddForce(dashPower, ForceMode.VelocityChange);
        }
    }
    private void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            if(Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0, 1, 0);
            }
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
        }
        rigidBody.MovePosition(this.gameObject.transform.position + dir * speed * Time.deltaTime);
    }
    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.4f, layer))  // ĳ������ �߳�(transform.position)����  0.2(Vector3.up * 2)��ŭ ���� ��ġ���� �Ʒ���(Vector3.down)���� 0.4(0.4f)��ŭ �������� ��� layer�� ����Ǹ� hit�� ��´ٴ� �ǹ�
        {
            ground = true;
        }
        else
        {
            ground = false;
        }

    }

}


