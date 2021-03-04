using UnityEngine;

public class minimapHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform plane;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = plane.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, plane.eulerAngles.y, 0f);
    }
}
