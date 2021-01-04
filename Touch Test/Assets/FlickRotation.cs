using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;
using Pixelplacement;

public class FlickRotation : MonoBehaviour
{

    [SerializeField] private Transform cylinder;
    [SerializeField] private int minRow;
    [SerializeField] private int maxRow;
    [SerializeField] private int panelCount;
    [SerializeField] private SpinnerFace panelPrefab;
    [SerializeField] private bool isPaginated = true;

    private PressGesture pressGesture;
    private FlickGesture flickGesture;
    private TransformGesture transformGesture;
    private ReleaseGesture releaseGesture;

    private Vector2 screenSize;

    private bool isFlicked;
    private bool isDragged;

    private Quaternion flickDestination;
    private Quaternion[] flickLocations;
    private int flickIndex = 0;
    private float flickTime = 0;
    private int currentRow = 0;
    private int totalRows = 0;
    private float rotationTime = 0;
    private float rotationAngle = 0;

    private const float RESET_TIME = 0.5f;

    private float dragAngle = 0;

    private SpinnerFace[] panels;

    //Double tap saver
    private Quaternion savedDestination;

    public int CurrentRow => currentRow;

    private float startTouchTime;

    private void Awake()
    {
        //MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        //meshRenderer.enabled = false;
        //screenSize = new Vector2(Screen.width, Screen.height);
        screenSize = new Vector2(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight);
        totalRows = (maxRow - minRow) + 1;
        print($"[FR][AWAKE] Screensize: {screenSize} Total rows: {totalRows}");
        float angle = 360.0f / panelCount;
        float radAngle = Mathf.Deg2Rad * angle * 0.5f;
        Transform panelRect = panelPrefab.transform;
        

        print($"Panel size: {panelRect.localScale.x}_{panelRect.localScale.y} angle: {angle} -> rad {radAngle}");
        float startZ = (panelRect.localScale.y * 0.5f) / Mathf.Tan(radAngle);
        Vector2 startPoint = new Vector2(0, startZ);
        print($"Start point: {startPoint} -> Z: {startZ}");
        float accumulatedAngle = 0.0f;

        panels = new SpinnerFace[panelCount];

        SpinnerFace firstPanel = Instantiate(panelPrefab, cylinder);
        firstPanel.name = $"Panel0";
        firstPanel.transform.localPosition = new Vector3(0, 0, startZ * -1);
        firstPanel.transform.localRotation = Quaternion.Euler(0, 0, 0);
        panels[0] = firstPanel;

        flickLocations = new Quaternion[panels.Length];
        flickLocations[0] = firstPanel.transform.localRotation;

        for (int i = 1; i < panelCount; i++)
        {
            accumulatedAngle += angle * Mathf.Deg2Rad;
            Vector2 position = RotatePoint(startPoint, accumulatedAngle);
            Vector3 actualPosition = new Vector3(0, position.x * -1, position.y * -1);
            
            SpinnerFace panel = Instantiate(panelPrefab, cylinder);
            panel.name = $"Panel{i}";
            panel.transform.localPosition = actualPosition;
            panel.transform.localRotation = Quaternion.Euler(angle * i, 0, 0);
            panels[panelCount - i] = panel;

            flickLocations[i] = panel.transform.localRotation;
            print($"Index: {i} position: {panel.transform.localPosition} angle: {panel.transform.localRotation.eulerAngles}");
        }

        //TODO: Update cylinder position based on radius of panel circle

        flickDestination = flickLocations[0];
        UpdatePanels();
    }


    private Vector2 RotatePoint(Vector2 point, float angle)
    {
        float newX = point.x * Mathf.Cos(angle) - point.y * Mathf.Sin(angle);
        float newY = point.y * Mathf.Cos(angle) + point.x * Mathf.Sin(angle);
        //print($"Rotate Point {point} by angle {angle} = {newX}_{newY}");
        return new Vector2(newX, newY);
    }

    //private void Start()
    //{
    //    float angle = 360.0f / panels.Length;
        //print($"Start panel angle: {angle} ");
        
        //for (int i = 0; i < panels.Length; i++)
        //{
        //    flickLocations[i] = Quaternion.Euler(angle * i, 0, 0);
        //}
        
    //}

    private void UpdatePanels()
    {
        //print($"UPDATE PANELS - flickIndex: {flickIndex} current: {currentRow}  total: {totalRows} ");

        SpinnerFace frontFace = panels[flickIndex]; //.GetComponent<SpinnerFace>();
        frontFace.UpdateNumber(currentRow + minRow);

        //TODO: handle updating half panel count both ways to handle none paginated flick -> some how call update panel during flick??
        int flickUpOne = decreaseIndex(flickIndex, panels.Length);
        SpinnerFace frontUpOneFace = panels[flickUpOne]; //.GetComponent<SpinnerFace>();
        int flickUpOneCurrentRow = decreaseIndex(currentRow, totalRows); //increaseIndex(currentRow, totalRows);
        frontUpOneFace.UpdateNumber(flickUpOneCurrentRow + minRow);

        int flickDownOne = increaseIndex(flickIndex, panels.Length);
        SpinnerFace frontDownOneFace = panels[flickDownOne]; //.GetComponent<SpinnerFace>();
        int flickDownOneCurrentRow = increaseIndex(currentRow, totalRows); //decreaseIndex(currentRow, totalRows);
        frontDownOneFace.UpdateNumber(flickDownOneCurrentRow + minRow);
        //print($"Front face: {frontFace.name} ({flickIndex}) up one: {frontUpOneFace.name} ({flickUpOne}) downOne: {frontDownOneFace.name} ({flickDownOne})");
    }

    private void OnEnable()
    {
        flickGesture = GetComponent<FlickGesture>();
        flickGesture.Flicked += flickedHandler;
        //flickGesture.FlickTime = 0.25f;
        flickGesture.MinDistance = 0.5f;
        flickGesture.MovementThreshold = 0.25f;

        pressGesture = GetComponent<PressGesture>();
        pressGesture.Pressed += pressedHandler;

        releaseGesture = GetComponent<ReleaseGesture>();
        releaseGesture.Released += releaseHandler;

        transformGesture = GetComponent<TransformGesture>();
        transformGesture.Transformed += transformedHandler;
        transformGesture.TransformCompleted += transformedEndedHandler;
        transformGesture.AddFriendlyGesture(flickGesture);
    }

    private void OnDisable()
    {
        flickGesture.Flicked -= flickedHandler;
        pressGesture.Pressed -= pressedHandler;
        releaseGesture.Released -= releaseHandler;
        transformGesture.Transformed -= transformedHandler;
        transformGesture.TransformCompleted -= transformedEndedHandler;
    }

    private void flickedHandler(object sender, System.EventArgs e)
    {
        bool goingUpwards = flickGesture.ScreenFlickVector.y > 0;
        isFlicked = true;
        float screenDivider = 1;
        if (!isPaginated) //Calculate the 
        {
            print($"FLICKED CUBE: {name} -> Vector {flickGesture.ScreenFlickVector} ({flickGesture.ScreenFlickVector.magnitude}) time: {flickGesture.ScreenFlickTime} going upwards: {goingUpwards} ");
            int maxPanelDiff = (int)(panelCount * 0.5f);
            float panelFraction = 1.0f / maxPanelDiff;
            float distancePct = flickGesture.ScreenFlickVector.magnitude / (screenSize.y * 1f);
            float flickPctSpeed = distancePct / flickGesture.ScreenFlickTime;
            print($"Flick distance pct: {distancePct} = speed: {flickPctSpeed} ");
            screenDivider = Mathf.Ceil(flickPctSpeed / 10.0f / panelFraction); // - normalise to screen size                                                
            screenDivider = Mathf.Min(screenDivider, maxPanelDiff);         // - constrain max spin to half panel count
            int panelPct = (int)screenDivider;
            print($"Max diff = {maxPanelDiff} - speed: {flickPctSpeed} - screen div: {screenDivider} - panel pct: {panelPct} ");
        }
        if (goingUpwards)
        {
            for (int i = 0; i < screenDivider; i++)
            {
                flickIndex = increaseIndex(flickIndex, panels.Length);
                currentRow = increaseIndex(currentRow, totalRows);
            }
        }
        else
        {
            for (int i = 0; i < screenDivider; i++)
            {
                flickIndex = decreaseIndex(flickIndex, panels.Length);
                currentRow = decreaseIndex(currentRow, totalRows);
            }
        }
        UpdatePanels();
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
        isDragged = false;
        savedDestination = new Quaternion(flickDestination.x, flickDestination.y, flickDestination.z, flickDestination.w);
        flickDestination = Quaternion.identity;
        print($"Pressed 2 flick: {savedDestination} from {flickDestination}");
        dragAngle = 0.0f;
        startTouchTime = Time.time;
    }

    private void transformedHandler(object sender, System.EventArgs e)
    {
        Vector2 diff = transformGesture.ScreenPosition - transformGesture.PreviousScreenPosition;
        //goingUpwards = diff.y > 0;
        if (transformGesture.State == Gesture.GestureState.Changed)
        {
            isDragged = true;
            float normalisedDiffY = diff.y / screenSize.y;
            float increment = 60.0f * normalisedDiffY;
            cylinder.Rotate(increment, 0, 0);
            dragAngle += increment;

        }
        print("transformed");
    }

    private void transformedEndedHandler(object sender, System.EventArgs e)
    {
        if (!isFlicked)
        {
            CheckRemainder(true);
        }
        print("END transformed");
    }

    private void releaseHandler(object sender, System.EventArgs e)
    {
        print($"released - drag: {isDragged} - flick: {flickDestination}");
        if (!isDragged)
        {
            flickDestination = savedDestination;
            flickTime = 0.0f;
            isFlicked = true;
            //print($"Not dragged - {flickDestination}");
        }
    }

    private void CheckRemainder(bool shouldMove)
    {
        float panelAngle = 360.0f / panels.Length;
        //float remainder = cylinder.rotation.eulerAngles.x % panelAngle;
        //float dividor = cylinder.rotation.eulerAngles.x / panelAngle;
        //bool quaternionFlop = cylinder.rotation.eulerAngles.y != 0f;
        //print($"Check remainder: {remainder} ({dividor}) - current: {cylinder.rotation.eulerAngles.x} = {flickIndex}  going up? {goingUpwards}");

        if (dragAngle > 0)
        {
            if (Math.Abs(dragAngle) > panelAngle * 0.5f)
            {
                flickIndex = increaseIndex(flickIndex, panels.Length);
                currentRow = increaseIndex(currentRow, totalRows);
            }
            print($"Going up -> new flick index {flickIndex}");
        }
        else
        {
            if (Math.Abs(dragAngle) > panelAngle * 0.5f)
            {
                flickIndex = decreaseIndex(flickIndex, panels.Length);
                currentRow = decreaseIndex(currentRow, totalRows);
            }
            print($"Going down -> new flick index {flickIndex}");
        }
        UpdatePanels();

        if (shouldMove)
        {
            print($"CHECK REMAINDER SHOULD MOVE ");
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
            //cylinder.rotation = Quaternion.Lerp(cylinder.rotation, flickDestination, flickTime);
            //print($"Slerp rotation: {cylinder.rotation} = {cylinder.rotation.eulerAngles} time: {flickTime}");
            flickTime += Time.deltaTime;
            if (flickTime > 1)
            {
                isFlicked = false;
                cylinder.rotation = flickDestination;
            }
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
            //print($"Reset spinner - cylinder already at '0' halfway: {halfWay} isOver? {isOverHalfWay}");
            isFlicked = false;
            rotationAngle = 360 * (isOverHalfWay ? 1 : -1);
            rotationTime = RESET_TIME;
        }
        else
        {
            //print("Reset spinner - normal");
            flickIndex = 0;
            flickDestination = flickLocations[flickIndex];
            flickTime = 0;
        }
        currentRow = 0;
        UpdatePanels();
    }

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 30;
        GUI.color = Color.black;
        //GUI.Label(new Rect(10, 10, 500, 40), $"Max diff = {maxPanelDiff} - speed: {flickSpeed} - screen div: {screenDivider} panel: {panelCounter} distance: {flickDistance} time: {flickGestureTime}");
        //GUI.Label(new Rect(10, 10, 500, 40), $"Flick index: {flickIndex} CurrentRow: {currentRow} cylinder: {cylinder.rotation.eulerAngles}", guiStyle);
        //    GUI.Label(new Rect(10, 60, 500, 40), $"Drag: {dragAngle} - flick time: {flickTime} is Flicked: {isFlicked}", guiStyle);
        //    GUI.Label(new Rect(10, 110, 500, 40), $"Destination: {flickDestination.eulerAngles}", guiStyle);
    }

    public void SpinTest()
    {
        Vector3 start = new Vector3(0, 0, 0);
        Vector3 end = new Vector3(360, 0, 0);
        Tween.Value(start, end, TweenUpdateValue, 2.0f, 0.0f);
    }

    private void TweenUpdateValue(Vector3 vector)
    {
        print($"Tween update: {vector}");
    }
}
