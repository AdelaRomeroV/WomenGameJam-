using UnityEngine;

public class PuntoDeTiempo : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;

    public PuntoDeTiempo (Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }
}
