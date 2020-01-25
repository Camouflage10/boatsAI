using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boatControl : MonoBehaviour
{
    //contols- 6 output perseptrons?
    public float sailSize = 0.0f;//up and down
    public float horizontalInput;//left and right/number
    public float sailAngle;//a and d
    float[] outputs;//get outputs from nueral script
    //read- 6 input perseptrons?
    float movementFactor;
    float steerFactor;
    float diffSailWind;//sail-wind angle
    float boatAngle;
    float targetAngle;
    float distanceFromTarget;//track the ship that got the closest to the target in a certain time frame
    float[] inputs;
    //target after they just drive thru the target add points the longer they stay with inthe targets range- move target with clicks/mouse;
    //race add senory rays for distance at 0 45 90

    //

    public float pp;
    public GameObject sail;

    public float windAngle;
    public float speed = 1.0f;
    public float movementThresold = 10.0f;
   public float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        windAngle = (float)Random.rotation.eulerAngles.y;
        //if shipangle+sailangle== windangle-> speed boost;
    }

    // Update is called once per frame
    void Update()
    {
        updateInputs();
        pp = sail.transform.localRotation.eulerAngles.y;
        turnSail();
        windBoost();
        sails();
        movement();
        steer();
    }

    void movement() {
        verticalInput = sailSize;
        if (verticalInput < 0.0f) {
            verticalInput = 0.0f;
        }
        movementFactor = Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThresold);
        transform.Translate(0.0f, 0.0f, movementFactor * speed);
    }
    //rotate instead
    void steer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        steerFactor = Mathf.Lerp(steerFactor, horizontalInput, Time.deltaTime / movementThresold);
        transform.Rotate(0.0f, steerFactor * speed, 0.0f);
    }
    void sails(){
        if (Input.GetKeyDown("up") &&sailSize<1.0f)
        {
            sailSize += 0.1f;
        }
        else if (Input.GetKeyDown("down") && sailSize>0) {
            sailSize -= 0.1f;
        }
    }
    void windBoost() {
        sailAngle = sail.transform.rotation.eulerAngles.y;
        if (Mathf.Abs(sailAngle - windAngle) < 6)
            speed =1.5f;
        else
            speed = 1.0f;
    }
    void turnSail() {
        if (Input.GetKeyDown("d")&&(sail.transform.localRotation.eulerAngles.y <90.0f|| sail.transform.localRotation.eulerAngles.y > 260.0f))
        {
            sail.transform.Rotate(0.0f,10.0f,0.0f);
        }
        else if(Input.GetKeyDown("a")&&(sail.transform.localRotation.eulerAngles.y > 270.0f|| sail.transform.localRotation.eulerAngles.y <100.0f)) {
            sail.transform.Rotate(0.0f, -10.0f, 0.0f);
        }
    }
    void updateInputs() {
        inputs = new float[] { movementFactor, steerFactor, diffSailWind, boatAngle, targetAngle, distanceFromTarget };
    }
}
