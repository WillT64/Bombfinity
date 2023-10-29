using UnityEngine;

public class CircleController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

           // Récupère la hauteur et la largeur de la caméra
    float cameraHeight = Camera.main.orthographicSize * 2.0f;
    float cameraWidth = cameraHeight * Camera.main.aspect;
    
    // Calcule la position de départ du personnage
    float characterStartY = 0; // Au milieu en Y
    float characterStartX = 0; // Au milieu en X

    transform.position = new Vector3(characterStartX, characterStartY, 0);

    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput, 0);
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Contrôles tactiles pour mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float middle = Screen.width / 2;

            if (touch.position.x > middle && touch.phase == TouchPhase.Stationary)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (touch.position.x < middle && touch.phase == TouchPhase.Stationary)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            
            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}
