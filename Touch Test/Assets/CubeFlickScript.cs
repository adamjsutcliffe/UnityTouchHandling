using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using TouchScript.Gestures.TransformGestures;

public class CubeFlickScript : MonoBehaviour 
{
    [SerializeField] private Transform cube;
    [SerializeField] private int rotationFrames = 10;
    [SerializeField] private float rotationTime = 0.5f;
    [SerializeField] private float decelerationRate = 0.1f;

    private FlickGesture flickGesture;
    private Vector2 screenSize;
    private Vector2 startVector;
    private bool isFlicked;

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
    }

    private void OnDisable()
    {
        flickGesture.Flicked -= flickedHandler;
    }

    private void flickedHandler(object sender, System.EventArgs e)
    {
        print($"FLICKED CUBE: {name} -> {flickGesture.ScreenFlickVector}");
        //if (flickGesture.ScreenFlickVector.y > 0)
        //{
        //isFlicked = true;
        //StartCoroutine(RotateCube(flickGesture.ScreenFlickVector.y > 0));
        //}
        startVector = flickGesture.ScreenFlickVector / 10f;// * flickGesture.ScreenFlickVector.y / screenSize.y;
    }

    private void Update()
    {
        float deltaDecleration = decelerationRate * Time.deltaTime;
        startVector -= new Vector2(startVector.x * deltaDecleration, startVector.y *deltaDecleration);

        cube.Rotate(startVector.y, 0, 0);
    }

    private float DecelerationVector()
    { 
        //deceleration = final velocity - initital velocity / time
        //d = (vf - vi)/t

          
        return 0.0f;
    }

    //private IEnumerator RotateCube(bool up)
    //{
    //    int counter = 0;

    //    float currentRotation = cube.rotation.eulerAngles.x;
    //    float remainder = 90.0f * (up ? 1 : -1); // - (currentRotation % 90.0f);
    //    float newRotation = currentRotation + 90.0f - remainder;
    //    print($"Rotation = current: {currentRotation} remainder: {remainder} new: {newRotation}");

    //    //float currentNineties = currentRotation / 90.0f;
    //    //float outstandingRotation = (90.0f * currentNineties) - currentRotation;
    //    float increment = remainder / rotationFrames;
    //    float spaces = rotationTime / rotationFrames;

    //    print($"Rotate cube increment: {increment} space: {spaces} transform: {cube.rotation.eulerAngles}  remainder: {remainder}");

    //    while (counter < rotationFrames)
    //    {
    //        cube.Rotate(increment, 0, 0);
    //        counter += 1;
    //        yield return new WaitForSeconds(spaces);
    //    }
    //    isFlicked = false;
    //    //print($"Rotate cube end transform: {transform.rotation.eulerAngles}");
    //}
}
