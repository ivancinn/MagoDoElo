using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 12f;
    public float xLimit = 10f;

    [Header("Sistema de Disparo")]
    public GameObject bulletPrefab;   // Arrastra aquí el prefab de tu bala
    public Transform firePoint;       // Objeto vacío colocado al frente del jugador
    public float fireRate = 0.2f;     // Tiempo mínimo entre disparos (en segundos)
    private float nextFireTime = 0f;

    [Header("Vida del Jugador")]
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Movimiento Horizontal (A/D o Flechas)
        float moveX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveX, 0f, 0f) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Límites de pantalla
        float clampedX = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Control de disparo continuo manteniendo presionado el botón
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Crea la bala en la posición y rotación del firePoint
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void PlayerTakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Vida restante del jugador: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("El jugador ha sido destruido.");
        Destroy(gameObject);

        // Congela el juego al morir (Pantalla de Game Over)
        Time.timeScale = 0f;
    }

    // Detecta cuando un enemigo entra en el espacio del jugador
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // El jugador recibe daño por el choque
            PlayerTakeDamage(20);

            // Identifica si es un enemigo común para destruirlo al impactar
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null && !enemy.isBoss)
            {
                Destroy(other.gameObject);
            }
        }
    }
}