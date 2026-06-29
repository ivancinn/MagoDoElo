using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Header("Configuración de Velocidad")]
    // Velocidad a la que se moverá el pasto. 
    // Como el jugador simula ir hacia adelante, el piso va hacia atrás (eje Y de la textura)
    public float scrollSpeed = 0.5f;

    private Renderer rend;

    void Start()
    {
        // Obtenemos el componente Renderer que dibuja el material en el plano
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Calculamos cuánto debe moverse la textura basándonos en el tiempo
        float offset = Time.time * scrollSpeed;

        // Desplazamos la textura en el eje Y (vertical)
        // Usamos "_MainTex" para el material estándar de Unity
        rend.material.SetTextureOffset("_MainTex", new Vector2(0f, offset));
    }
}