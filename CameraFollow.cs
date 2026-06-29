using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Arrastra a tu jugador aquí desde el Inspector
    public Vector3 offset = new Vector3(0f, 15f, -5f); // Ajusta la altura (Y) y la distancia (Z)

    void LateUpdate()
    {
        if (target != null)
        {
            // Sigue al jugador manteniendo la distancia configurada
            transform.position = target.position + offset;
        }
    }
}