using UnityEngine;

public class KeepWorldRotation : MonoBehaviour
{
    private Quaternion worldRot;

    private void Awake()
    {
        worldRot = transform.rotation;
    }
    private void LateUpdate()
    {
        transform.rotation = worldRot;
    }

}
