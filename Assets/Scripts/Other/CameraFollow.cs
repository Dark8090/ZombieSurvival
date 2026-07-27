using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speedFollow = 3f;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * speedFollow);
        }

    }
}
