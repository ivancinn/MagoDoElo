using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Estadísticas")]
    public int maxHealth = 10;
    public float moveSpeed = 5f;
    public int passDamage = 10; // Daño que recibes si el enemigo logra llegar a tu posición

    [Header("Configuración de Jefe")]
    public bool isBoss = false;
    public float bossStopZPosition = 12f;

    private int currentHealth;
    private PlayerController player; // Variable para recordar al jugador

    void Start()
    {
        currentHealth = maxHealth;

        // Busca el script del jugador en la escena automáticamente al aparecer
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        // 1. Movimiento
        if (isBoss && transform.position.z <= bossStopZPosition)
        {
            // El jefe se detiene en su posición de combate
            transform.position = new Vector3(transform.position.x, transform.position.y, bossStopZPosition);
        }
        else
        {
            // Movimiento normal hacia abajo (mantenemos Space.World por si los rotaste)
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
        }

        // 2. Lógica de desaparición y daño al llegar al jugador (Solo enemigos comunes)
        if (!isBoss)
        {
            // Verificamos que el jugador siga vivo y si el enemigo llegó a su altura (eje Z)
            if (player != null && transform.position.z <= player.transform.position.z)
            {
                // El enemigo te alcanzó/pasó: te hace daño
                player.PlayerTakeDamage(passDamage);

                // Efecto visual/sonido opcional aquí antes de destruirse
                Debug.Log("¡Un enemigo llegó a tu posición y te hizo daño!");

                // Desaparece
                Destroy(gameObject);
            }
            // Respaldo: Si el jugador ya murió, destruye a los enemigos que salgan mucho de la pantalla
            else if (transform.position.z < -15f)
            {
                Destroy(gameObject);
            }
        }
    }

    // Método para recibir daño de las balas
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isBoss)
        {
            // Congela el juego al ganar
            Time.timeScale = 0f;
        }

        Destroy(gameObject);
    }
}