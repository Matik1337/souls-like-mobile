using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Run(Vector3 direction)
    {
        transform.LookAt(direction);
    }
}
