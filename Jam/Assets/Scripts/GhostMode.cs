using UnityEngine;

public class GhostMode : MonoBehaviour
{
    public float ghostGravity = 0.1f;
    public float ghostOpacity = 0.5f;
    public float ghostHardness = 0.1f;

    private Rigidbody2D rb;
    private bool isGhostMode = false;
    public Collider2D colliderCheck;
    public Collider2D Collider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ToggleGhostMode();
        }
    }

    private void ToggleGhostMode()
    {
        isGhostMode = !isGhostMode;

        if (isGhostMode)
        {
            // ��������� ������������
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, ghostOpacity);

            // ��������� ������ ����������
            rb.gravityScale = ghostGravity;

            // ��������� ������ ��������
            rb.drag = ghostHardness;
        }
        else
        {
            // �������������� ������� ����������
            GetComponent<Renderer>().material.color = Color.white;
            rb.gravityScale = 1;
            rb.drag = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGhostMode)
        {
            // ���������, �������� �� ������, � ������� ���������� �����, ���������� � ������ ��������
            if (other.gameObject.layer == LayerMask.NameToLayer("PassableObject"))
            {
                Collider.enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isGhostMode)
        {
            Collider.enabled = true;
        }
    }
}