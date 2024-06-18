using UnityEngine;

public class ExplosionSelfDestruct : MonoBehaviour
{
    public float duration = 2f; // Duration of the explosion effect

    private void Start()
    {
        Destroy(gameObject, duration);
    }
}
