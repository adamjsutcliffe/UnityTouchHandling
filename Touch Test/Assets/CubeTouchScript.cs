using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using TouchScript.Gestures.TransformGestures;

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
        flickGesture.Flicked += flickedHandler;
        flickGesture.MinDistance = 0.5f;
        flickGesture.MovementThreshold = 0.25f;
        print($"FLICK min: {flickGesture.MinDistance} threshold: {flickGesture.MovementThreshold} time: {flickGesture.FlickTime}");

        transformGesture = GetComponent<TransformGesture>();
        transformGesture.Transformed += transformedHandler;
        transformGesture.TransformCompleted += transformedEndedHandler;
        //transformGesture.AddFriendlyGesture(flickGesture);
    }

    private void OnDisable()
    {
        flickGesture.Flicked -= flickedHandler;
        transformGesture.Transformed -= transformedHandler;
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

    private void transformedHandler(object sender, System.EventArgs e)
    {
        Vector2 diff = transformGesture.ScreenPosition - transformGesture.PreviousScreenPosition;
        //print($"transformed {name} delta: {transformGesture.DeltaPosition} posi: {transformGesture.ScreenPosition} prev: {transformGesture.PreviousScreenPosition} diff: {diff}");

        if (transformGesture.State == Gesture.GestureState.Changed)
        {
            if (!isFlicked)
            {
                print($"transform changed");
                float normalisedDiffY = diff.y / screenSize.y;
                float increment = 90.0f * normalisedDiffY;
                transform.Rotate(increment, 0, 0);
            }
        }
    }

    private void transformedEndedHandler(object sender, System.EventArgs e)
    {
        print($"transform ended - {isFlicked}");
    }

    private IEnumerator RotateCube(bool up)
    {
        currentSide += (up ? 1 : -1);
        int endRotation = 90 * currentSide;

        int counter = 0;

        float currentRotation = transform.rotation.eulerAngles.x;
        float remainder = 90.0f - (currentRotation % 90.0f);
        float newRotation = currentRotation + 90.0f - remainder;
        print($"Rotation = current: {currentRotation} end: {endRotation} remainder: {remainder} new: {newRotation}");

        float increment =  remainder / rotationFrames;
        float spaces = rotationTime / rotationFrames;

        print($"Rotate cube increment: {increment} space: {spaces} transform: {transform.rotation.eulerAngles}  remainder: {remainder}");

        while (counter < rotationFrames)
        {
            transform.Rotate(increment, 0, 0);
            counter += 1;
            yield return new WaitForSeconds(spaces);
        }
        isFlicked = false;
        print($"Rotate cube end transform: {transform.rotation.eulerAngles}");
    }
}
