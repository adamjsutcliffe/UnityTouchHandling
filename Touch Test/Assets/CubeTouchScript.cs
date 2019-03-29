using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using TouchScript.Gestures.TransformGestures;
using TweenAnimation;

public class CubeTouchScript : MonoBehaviour 
{
    [SerializeField] private int rotationFrames = 10;
    [SerializeField] private float rotationTime = 0.5f;

    private FlickGesture flickGesture;
    private TransformGesture transformGesture;
    private Vector2 screenSize;
    private bool isFlicked;
    private int currentSide = 0;

    private void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        print($"SCREEN SIZE: {screenSize}");
    }

    private void OnEnable()
    {
        flickGesture = GetComponent<FlickGesture>();
        //flickGesture.Flicked += flickedHandler;
        flickGesture.MinDistance = 0.5f;
        flickGesture.MovementThreshold = 0.25f;
        print($"FLICK min: {flickGesture.MinDistance} threshold: {flickGesture.MovementThreshold} time: {flickGesture.FlickTime}");

        transformGesture = GetComponent<TransformGesture>();
        transformGesture.TransformStarted += transformedStartedHandler;
        transformGesture.Transformed += transformedHandler;
        transformGesture.TransformCompleted += transformedEndedHandler;
        //transformGesture.AddFriendlyGesture(flickGesture);
    }

    private void OnDisable()
    {
        flickGesture.Flicked -= flickedHandler;
        transformGesture.TransformStarted -= transformedStartedHandler;
        transformGesture.Transformed -= transformedHandler;
        transformGesture.TransformCompleted -= transformedEndedHandler;
    }

    private void flickedHandler(object sender, System.EventArgs e)
    {
        print($"FLICKED CUBE: {name} -> {flickGesture.ScreenFlickVector}");
        //if (flickGesture.ScreenFlickVector.y > 0)
        //{
            isFlicked = true;
            StartCoroutine(RotateCube(flickGesture.ScreenFlickVector.y > 0));
        //}
    }

    private void transformedStartedHandler(object sender, System.EventArgs e)
    {
        print($"transform started - {isFlicked} => {transform.rotation.eulerAngles}");
    }


    private void transformedHandler(object sender, System.EventArgs e)
    {
        Vector2 diff = transformGesture.ScreenPosition - transformGesture.PreviousScreenPosition;
        //print($"transformed {name} delta: {transformGesture.DeltaPosition} posi: {transformGesture.ScreenPosition} prev: {transformGesture.PreviousScreenPosition} diff: {diff}");

        if (transformGesture.State == Gesture.GestureState.Changed)
        {
            if (!isFlicked)
            {
                //print($"transform changed");
                float normalisedDiffY = diff.y / screenSize.y;
                float increment = 90.0f * normalisedDiffY;
                transform.Rotate(increment, 0, 0);
            }
        }
    }

    private void transformedEndedHandler(object sender, System.EventArgs e)
    {
        print($"transform ended - {isFlicked} => {transform.rotation.eulerAngles}");
    }

    private IEnumerator RotateCube(bool up)
    {
        print($"Rotate cube start transform: {transform.rotation.eulerAngles}");
        currentSide += (up ? 1 : -1);
        int endRotation = 90 * currentSide;

        int counter = 0;

        float currentRotation = transform.rotation.eulerAngles.x;
        float eulerYCheck = transform.rotation.eulerAngles.y;
        float remainder = endRotation - currentRotation;
        print($"Rotation = current: {currentRotation} end: {endRotation} remainder: {remainder} euler Y: {eulerYCheck}");

        float increment = remainder / rotationFrames;
        float spaces = rotationTime / rotationFrames;

        print($"Rotate cube increment: {increment} space: {spaces} frames: {rotationFrames}");
        
        while (counter < rotationFrames)
        {
            //transform.Rotate(increment, 0, 0);
            counter += 1;
            transform.rotation = Quaternion.Euler(currentRotation + (increment * counter), 0, 0);
            yield return new WaitForSeconds(spaces);
        }
        isFlicked = false;
        print($"Rotate cube end transform: {transform.rotation.eulerAngles}");
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 500, 20), $"{transform.rotation.eulerAngles} - ({transform.rotation})");
    }
}
