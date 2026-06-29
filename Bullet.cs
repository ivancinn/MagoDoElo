using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 25f;
    public int damage = 5;
    public float lifeTime = 3f; // Tiempo en segundos antes de destruirse sola si no impacta nada

    void Start()
    {
        // Evita que las balas perdidas llenen la memoria del juego
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Se mueve hacia adelante (Z positivo)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si impactó contra un objeto etiquetado como "Enemy"
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Aplica el daño al enemigo
            }

            Destroy(gameObject); // Destruye la bala tras el impacto
        }
    }
}