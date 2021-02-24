using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPlane : MonoBehaviour
{
    Quaternion rotation;
    Quaternion rotationMin = Quaternion.Euler(new Vector3(0f, 0f, -50f));
    Quaternion rotationMax = Quaternion.Euler(new Vector3(0f, 0f, 50f));


    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A) && rotation.z < rotationMax.z)
        {
            Debug.Log("triggered left");
            rotation.z += Quaternion.Euler(new Vector3(0f, 0f, 10f * Time.deltaTime)).z;
        }

        if (Input.GetKey(KeyCode.D) &&  rotation.z > rotationMin.z)
        {
            Debug.Log("triggered right");
            rotation.z -= Quaternion.Euler(new Vector3(0f, 0f, 10f * Time.deltaTime)).z;
        }

        transform.rotation = rotation;
    }
    private void Barrel(float direction)
    {
        direction = (direction > 180) ? direction - 360 : direction;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
            transform.eulerAngles.y,
            Mathf.Lerp(transform.eulerAngles.z, direction, Time.deltaTime * 1.0f));
    }
}
