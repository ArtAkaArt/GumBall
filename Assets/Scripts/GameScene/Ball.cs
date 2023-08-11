using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(ColorManager))]
public class Ball : MonoBehaviour
{
    [SerializeField] private AudioSource bounceSound;
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }

    private ColorManager colorManager;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private MenuHandler menu;
    private AudioHub hub;


    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        colorManager = GetComponent<ColorManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitComponents(MenuHandler menu, AudioHub hub)
    {
        this.menu = menu;
        this.hub = hub;
    } 

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].normal.IsEdge())
        {
            hub.PlayGameOver();
            Destroy(gameObject);
            menu.OpenMenu();
            return;
        }
        bounceSound.Play();
        if (other.contacts[0].collider.gameObject.TryGetComponent<ColorManager>(out var manager))
        {
            colorManager.RenderColor(manager.Color);
            colorManager.Color = manager.Color;
        }

        var reflection = Vector2.Reflect(Direction, other.contacts[0].normal);
        Direction = reflection;
        rb.velocity = Vector2.zero;
        rb.AddForce(Direction*Speed, ForceMode2D.Impulse);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (circleCollider.isTrigger)
            circleCollider.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Obstacle"))
            return;
        var otherColor = other.GetComponent<ColorManager>().Color;
        colorManager.RenderColor(otherColor);
        colorManager.Color = otherColor;
    }
}