using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Camera camera;
    private CharacterBase characterBase;
    private void Start()
    {
        camera = Camera.main;
        characterBase = GetComponent<CharacterBase>();
    }

    private void Update()
    {
        PlayerFollowToMouse();
    }

    public Vector3 MouseTracker()
    {
        //Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = 0;
        //return mousePos;
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    public Ray MouseRayTracker()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0f;
        return Camera.main.ScreenPointToRay(mousePoint);
    }

    private void PlayerFollowToMouse()
    {
        Vector3 direction = MouseTracker() - characterBase.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        characterBase.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
