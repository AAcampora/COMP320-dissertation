using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    private CharacterController controller;

    public GameObject planeObj;

    public GameObject landingGear;

    public float currSpeed = 0.0f;
    public float baseSpeed = 10.0f;
    public float rotSpeedX = 3.0f;
    public float rotSpeedY = 1.5f;

    private Vector3 gravity = Physics.gravity;

    public bool isGrounded;

    public bool isLandingGear;
    
    public enum EPlaneState { FLYING, DEPARTING, LANDING };

    EPlaneState planeState;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        planeState = EPlaneState.DEPARTING;
    }

    private void Update()
    {
        //
        //give player forward velocity

        if(Input.GetKey(KeyCode.Q))
        {
            currSpeed = Mathf.Lerp(currSpeed, baseSpeed, Time.deltaTime * 0.5f);
        }

        if (Input.GetKey(KeyCode.E))
        {
            currSpeed = Mathf.Lerp(currSpeed, 0, Time.deltaTime * 0.5f);
        }


        Vector3 propelPlane = PropelPlane(currSpeed);

        propelPlane = StallingHandler(propelPlane);

        controller.Move(propelPlane * Time.deltaTime);

        var angleVelocityResult = PlaneAngleVelocity();

        currSpeed = angleVelocityResult.currSpeed;
        baseSpeed = angleVelocityResult.baseSpeed;

        isGrounded = GroundDetecor();

        switch (planeState)
        {
            case EPlaneState.FLYING:
                break;
            case EPlaneState.DEPARTING:
                break;
            case EPlaneState.LANDING:
                break;
            default:
                break;
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            if(isLandingGear)
            {
                landingGear.SetActive(true);
                isLandingGear = false;
            }
            else if(!isLandingGear)
            {
                landingGear.SetActive(false);
                isLandingGear = true;
            }
            
        }
    }

    private Vector3 PropelPlane(float speed)
    {

        Vector3 moveVector = transform.forward * speed;

        //get player inputs 
        Vector3 yaw = Input.GetAxis("Horizontal") * transform.right * rotSpeedX * Time.deltaTime;

        Vector3 pitch = Input.GetAxis("Vertical") * transform.up * rotSpeedY * Time.deltaTime;

        //translate this inputs into plane movement
        Vector3 dir = yaw + pitch;

        moveVector += dir;

        transform.rotation = Quaternion.LookRotation(moveVector);
        return moveVector;
    }

    private Vector3 StallingHandler(Vector3 velocity)
    {
        if (controller.isGrounded == false)
        {
            if(currSpeed <= 3.5f)
            {
                //Add our gravity Vecotr
                return velocity += gravity;
            }
            else
            {
                return velocity;
            }
        }
        else
        {
            return velocity;
        }
    }

    private AngleVelocity PlaneAngleVelocity()
    {
        if (transform.rotation.eulerAngles.x >= 5.0f && transform.rotation.eulerAngles.x >= 355.0f)
        {
            return new AngleVelocity
            {
                currSpeed = currSpeed = Mathf.Lerp(currSpeed, baseSpeed, Time.deltaTime * 0.5f),
                baseSpeed = baseSpeed
            };
        }
        else if (transform.eulerAngles.x <= 30.0f)
        {
            //going down
            return new AngleVelocity
            {
                currSpeed = Mathf.Lerp(currSpeed, baseSpeed, Time.deltaTime * 0.5f),
                baseSpeed = Mathf.Lerp(baseSpeed, 30.0f, Time.deltaTime * 1.5f)
            };
        }
        else if (transform.eulerAngles.x >= 320.0f)
        {
            //going up
            return new AngleVelocity
            {
                currSpeed = Mathf.Lerp(currSpeed, baseSpeed, Time.deltaTime * 0.5f),
                baseSpeed = Mathf.Lerp(baseSpeed, 10.0f, Time.deltaTime * 1.0f)
            };
           
            
        }
        else if (transform.rotation.eulerAngles.x <= 280.0f && transform.rotation.eulerAngles.x >= 270.0f)
        {
            //vertical climb stall
            return new AngleVelocity
            {
                currSpeed = Mathf.Lerp(currSpeed, baseSpeed, Time.deltaTime * 0.5f),
                baseSpeed = Mathf.Lerp(baseSpeed, 0.0f, Time.deltaTime * 1.0f)
            };
        }
        else
        {
            return new AngleVelocity
            {
                currSpeed = currSpeed,
                baseSpeed = baseSpeed
            };
        }
    }

    private bool GroundDetecor()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 5.0f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


