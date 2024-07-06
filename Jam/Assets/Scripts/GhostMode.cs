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
            // Установка прозрачности
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, ghostOpacity);

            // Установка низкой гравитации
            rb.gravityScale = ghostGravity;

            // Установка низкой твёрдости
            rb.drag = ghostHardness;
        }
        else
        {
            // Восстановление обычных параметров
            GetComponent<Renderer>().material.color = Color.white;
            rb.gravityScale = 1;
            rb.drag = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGhostMode)
        {
            // Проверяем, является ли объект, с которым столкнулся игрок, проходимым в режиме призрака
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