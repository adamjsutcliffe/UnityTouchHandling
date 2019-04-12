using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class FlickRotation : MonoBehaviour {

    [SerializeField] private Transform cylinder;
    [SerializeField] private float amount = 50f;
    [SerializeField] private float minSpeed = 0.3f;

    private Vector2 startVector;
    private Vector2 dragVector;

    private PressGesture pressGesture;
    private FlickGesture flickGesture;
    private TransformGesture transformGesture;

    private Vector2 screenSize;
    private int panelCount = 12;
    private Rigidbody cylinderBody;

    private bool isFlicked;
    private bool isMoving;
    private bool goingUpwards;

    private void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        print($"SCREEN SIZE: {screenSize}");
    }

    private void Start()
    {
        cylinderBody = cylinder.GetComponent<Rigidbody>();
        //TODO generate x number of panels
        float angle = 360.0f / panelCount;
        print($"Start panel angle: {angle}");
    }

    private void OnEnable()
    {
        flickGesture = GetComponent<FlickGesture>();
        flickGesture.Flicked += flickedHandler;
        flickGesture.MinDistance = 0.5f;
        flickGesture.MovementThreshold = 0.25f;

        pressGesture = GetComponent<PressGesture>();
        pressGesture.Pressed += pressedHandler;

        transformGesture = GetComponent<TransformGesture>();
        transformGesture.Transformed += transformedHandler;
        transformGesture.TransformCompleted += transformedEndedHandler;
        transformGesture.AddFriendlyGesture(flickGesture);
    }

    private void OnDisable()
    {
        flickGesture.Flicked -= flickedHandler;
        pressGesture.Pressed -= pressedHandler;
        transformGesture.Transformed -= transformedHandler;
        transformGesture.TransformCompleted -= transformedEndedHandler;
    }

    private void flickedHandler(object sender, System.EventArgs e)
    {
        startVector = flickGesture.ScreenFlickVector / 10f;
        goingUpwards = startVector.y > 0;
        isFlicked = true;

        print($"FLICKED CUBE: {name} -> {flickGesture.ScreenFlickVector} going upwards: {goingUpwards}");
    }

    private void pressedHandler(object sender, System.EventArgs e)
    {
        print($"pressed - {isFlicked} => {transform.rotation.eulerAngles}");
        cylinderBody.angularVelocity = Vector3.zero;
        isFlicked = false;
    }


    private void transformedHandler(object sender, System.EventArgs e)
    {
        Vector2 diff = transformGesture.ScreenPosition - transformGesture.PreviousScreenPosition;
        //print($"transformed {name} delta: {transformGesture.DeltaPosition} posi: {transformGesture.ScreenPosition} prev: {transformGesture.PreviousScreenPosition} diff: {diff}");

        if (transformGesture.State == Gesture.GestureState.Changed)
        {
            //if (!isFlicked)
            {
                print($"transform changed");
                float normalisedDiffY = diff.y / screenSize.y;
                float increment = 90.0f * normalisedDiffY;
                //print($"transform changed - {increment} -  {normalisedDiffY}");
                //dragVector = new Vector2(0, increment);
                cylinder.Rotate(0, -increment, 0);
            }
        }
    }

    private void transformedEndedHandler(object sender, System.EventArgs e)
    {
        print($"transform ended - {isFlicked} => {cylinder.rotation.eulerAngles}");
        CheckRemainder();
    }

    private void CheckRemainder()
    {
        float remainder = cylinder.rotation.eulerAngles.x % 90.0f;
        print($"Check remainder: {remainder}");
    }

    private void FixedUpdate()
    {
        float accumSpeed = Mathf.Abs(cylinderBody.angularVelocity.x); // + cylinderBody.angularVelocity.y + cylinderBody.angularVelocity.z;
        //print($"spped check X: {accumSpeed}");
        //if (cylinderBody.angularVelocity != Vector3.zero)
        //{

        //    print($"Cylinder velo: {cylinderBody.angularVelocity} X: {accumSpeed} Moving: {isMoving}");
        //}
        if (accumSpeed < minSpeed && isMoving && isFlicked)
        {
            isMoving = false;
            isFlicked = false;
            cylinderBody.angularVelocity = Vector3.zero;
            print("STOP FLICK");
            CheckRemainder();
        }

        if (startVector != Vector2.zero)
        {
            float h = startVector.y * amount * Time.deltaTime;
            cylinderBody.AddTorque(transform.right * h);
            startVector = Vector2.zero;
            isMoving = true;
            print($"Add torque: {transform.right * h} is moving: {isMoving}");
        }
    }
}
