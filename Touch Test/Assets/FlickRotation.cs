using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class FlickRotation : MonoBehaviour
{

    [SerializeField] private Transform cylinder;
    [SerializeField] private Transform[] panels;
    [SerializeField] private int minRow;
    [SerializeField] private int maxRow;

    private PressGesture pressGesture;
    private FlickGesture flickGesture;
    private TransformGesture transformGesture;

    private Vector2 screenSize;

    private bool isFlicked;
    private bool goingUpwards;


    private Quaternion flickDestination;
    private Quaternion[] flickLocations;
    private int flickIndex = 0;
    private float flickTime = 0;
    private int currentRow = 0;
    private int totalRows = 0;
    private float rotationTime = 0;
    private float rotationAngle = 0;

    private const float RESET_TIME = 0.5f;

    private void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        print($"SCREEN SIZE: {screenSize}");
        totalRows = maxRow - minRow;
    }

    private void Start()
    {
        float angle = 360.0f / panels.Length;
        print($"Start panel angle: {angle} ");
        flickLocations = new Quaternion[panels.Length];
        for (int i = 0; i < panels.Length; i++)
        {
            flickLocations[i] = Quaternion.Euler(angle * i, 0, 0);
            print($"Click location {i}: {flickLocations[i]} = {flickLocations[i].eulerAngles}");
        }
        flickDestination = flickLocations[0];
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
        goingUpwards = flickGesture.ScreenFlickVector.y > 0;
        isFlicked = true;
        print($"FLICKED CUBE: {name} -> {flickGesture.ScreenFlickVector} time: {flickGesture.ScreenFlickTime} going upwards: {goingUpwards}");
        if (goingUpwards)
        {
            flickIndex = increaseIndex(flickIndex, panels.Length);
            currentRow = increaseIndex(currentRow, totalRows);
        }
        else
        {
            flickIndex = decreaseIndex(flickIndex, panels.Length);
            currentRow = decreaseIndex(currentRow, totalRows);
        }
        flickDestination = flickLocations[flickIndex];
        flickTime = 0;
    }

    private int increaseIndex(int value, int maxValue)
    {
        if (value < maxValue - 1)
        {
            value += 1;
        }
        else
        {
            value = 0;
        }
        return value;
    }
    private int decreaseIndex(int value, int maxValue)
    {
        if (value > 0)
        {
            value -= 1;
        }
        else
        {
            value = maxValue - 1;
        }
        return value;
    }

    private void pressedHandler(object sender, System.EventArgs e)
    {
        isFlicked = false;
        flickDestination = Quaternion.identity;
    }


    private void transformedHandler(object sender, System.EventArgs e)
    {
        Vector2 diff = transformGesture.ScreenPosition - transformGesture.PreviousScreenPosition;
        goingUpwards = diff.y > 0;
        if (transformGesture.State == Gesture.GestureState.Changed)
        {
            //if (!isFlicked)
            {
                float normalisedDiffY = diff.y / screenSize.y;
                float increment = 60.0f * normalisedDiffY;
                cylinder.Rotate(increment, 0, 0);
            }
        }
    }

    private void transformedEndedHandler(object sender, System.EventArgs e)
    {
        if (!isFlicked)
        {
            CheckRemainder(true);
        }
    }

    private void CheckRemainder(bool shouldMove)
    {
        float panelAngle = (360.0f / panels.Length);
        float remainder = cylinder.rotation.eulerAngles.x % panelAngle;
        float dividor = cylinder.rotation.eulerAngles.x / panelAngle;
        bool quaternionFlop = cylinder.rotation.eulerAngles.y != 0f;
        print($"Check remainder: {remainder} ({dividor}) - current: {cylinder.rotation.eulerAngles.x} = {flickIndex}  going up? {goingUpwards}");

        if (goingUpwards)
        {
            if (remainder > panelAngle * 0.5f)
            {
                flickIndex = increaseIndex(flickIndex, panels.Length);
                currentRow = increaseIndex(currentRow, totalRows);
            }
            print($"Going up -> new flick index {flickIndex}");
        }
        else
        {
            if (remainder < panelAngle * 0.5f)
            {
                flickIndex = decreaseIndex(flickIndex, panels.Length);
                currentRow = decreaseIndex(currentRow, totalRows);
            }
            print($"Going down -> new flick index {flickIndex}");
        }

        if (shouldMove)
        {
            isFlicked = true;
            flickDestination = flickLocations[flickIndex];
            flickTime = 0;
        }
    }

    private void FixedUpdate()
    {
        if (isFlicked)
        {
            cylinder.rotation = Quaternion.Slerp(cylinder.rotation, flickDestination, flickTime);
            flickTime += Time.deltaTime;
        }
        else if (rotationTime > 0)
        {
            float moveIncrement = rotationAngle * Time.deltaTime / RESET_TIME;
            cylinder.Rotate(moveIncrement, 0, 0);
            rotationTime -= Time.deltaTime;
            if (rotationTime <= 0) //reset
            {
                cylinder.transform.rotation = Quaternion.identity;
            }
        }
    }

    public void ResetSpinnerHandler()
    {
        if (flickIndex == 0 && currentRow != 0)
        {
            
            int halfWay = (maxRow - minRow) / 2;
            bool isOverHalfWay = currentRow > halfWay;
            print($"Reset spinner - cylinder already at '0' halfway: {halfWay} isOver? {isOverHalfWay}");
            isFlicked = false;
            rotationAngle = 720 * (isOverHalfWay ? 1 : -1);
            rotationTime = RESET_TIME;
        } else
        {
            print("Reset spinner - normal");
            flickIndex = 0;
            flickDestination = flickLocations[flickIndex];
            flickTime = 0;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 500, 20), $"Flick index: {flickIndex} CurrentRow: {currentRow} cylinder: {cylinder.rotation.eulerAngles}");
    }
}
