using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    private CharacterController controller;

    public GameObject planeObj;

    public GameObject landingGear;

    public int rings = 10;

    public float currSpeed = 0.0f;
    public float baseSpeed = 10.0f;
    public float rotSpeedX = 3.0f;
    public float rotSpeedY = 1.5f;

    Vector3 pitch;

    Vector3 moveVector;

    private Vector3 gravity = Physics.gravity;

    Vector3 propelPlane;

    public bool isGrounded;

    public bool isAboutToCollide;

    public bool isLandingGear = true;

    public bool isHelperOn;
    
    public enum EPlaneState { FLYING, DEPARTING, LANDING };

    public EPlaneState planeState;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        planeState = EPlaneState.DEPARTING;
        currSpeed = 0;
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

        isGrounded = GroundDetecor();

        switch (planeState)
        {
             
            case EPlaneState.FLYING:
                isLandingGear = false;
                propelPlane = PropelPlane(currSpeed);
                propelPlane = StallingHandler(propelPlane);

                controller.Move(propelPlane * Time.deltaTime);

                var angleVelocityResult = PlaneAngleVelocity();

                currSpeed = angleVelocityResult.currSpeed;
                baseSpeed = angleVelocityResult.baseSpeed;
                break;

            case EPlaneState.DEPARTING:

                isLandingGear = true;
                if (Input.GetKey(KeyCode.Q))
                {
                    currSpeed = Mathf.Lerp(currSpeed, 10.0f, Time.deltaTime / 10.0f);
                }
                propelPlane = PropelPlane(currSpeed);
                controller.Move(propelPlane * Time.deltaTime);

                if (currSpeed >= 9.0f)
                {
                    planeState = EPlaneState.FLYING;
                }
                break;

            case EPlaneState.LANDING:
                break;
            default:
                break;
        }

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

        StallingHandler();
        GroundHelper();
        TurnHelper();
    }

    private Vector3 PropelPlane(float speed)
    {

        moveVector = transform.forward * speed;

        //get player inputs 
        Vector3 yaw = Input.GetAxis("Horizontal") * transform.right * rotSpeedX * Time.deltaTime;
        
        if(isAboutToCollide)
        {
            pitch = new Vector3(0, 0, 0);
        }
        else
        {
            pitch = Input.GetAxis("Vertical") * transform.up * rotSpeedY * Time.deltaTime;
        }
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
            if(!isAboutToCollide)
            {
                //going down
                return new AngleVelocity
                {
                    currSpeed = Mathf.Lerp(currSpeed, baseSpeed, Time.deltaTime * 0.5f),
                    baseSpeed = Mathf.Lerp(baseSpeed, 30.0f, Time.deltaTime * 1.5f)
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

    private void GroundHelper()
    {
        if (isHelperOn)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, moveVector, out hit, 500.0f))
            {
                Debug.DrawRay(transform.position, moveVector * 500.0f);
                if (hit.collider.tag == "floor")
                {
                    if(hit.distance <= 100)
                    {
                        isAboutToCollide = true;   
                    }
                }
               
            }
            if (isAboutToCollide)
            {
                currSpeed = Mathf.Lerp(currSpeed, baseSpeed, Time.deltaTime * 5);
                baseSpeed = Mathf.Lerp(baseSpeed, 10.0f, Time.deltaTime * 7);
                transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, -120, Time.deltaTime / 40f), transform.eulerAngles.y, transform.eulerAngles.z);

                Debug.Log(transform.rotation.eulerAngles);

                if (transform.rotation.eulerAngles.x < 5.0f)
                {
                    isAboutToCollide = false;
                }
            }
        }
    }
    private void StallingHandler()
    {
        if (isHelperOn)
        {
            if (transform.eulerAngles.x <= 320 && transform.eulerAngles.x >315.0f && Input.GetKey(KeyCode.W))
            {
                rotSpeedY = 0.0f;
            }
            else
            {
                rotSpeedY = 3.0f;
            }
            if (currSpeed < 5.0f)
            {
                currSpeed = Mathf.Clamp(currSpeed, 5.0f, 10.0f);
            }
        }
    }

    private void TurnHelper()
    {
        if(isHelperOn)
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {       
                currSpeed -= 0.01f;
            }
        }
    }
}


