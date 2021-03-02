using UnityEngine;

public class BarrelPlane : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Barrel(-60.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Barrel(60.0f);
        }
        else
        {
            Barrel(0.0f);
        }
    }
    private Vector3 Barrel(float direction)
    {
        return transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle(transform.eulerAngles.z,direction, Time.deltaTime));
    }
}