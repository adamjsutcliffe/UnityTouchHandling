using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ScrollManager : MonoBehaviour 
{
    [SerializeField] private RectTransform ScrollingViewPort;
    [SerializeField] private RectTransform ScrollCenter;
    [SerializeField] private GameObject scrollItemPrefab;

    [SerializeField] private int StartValue;
    [SerializeField] private int EndValue;

    private GameObject[] scrollObjects;
    private UIVerticalScroller VerticalScroller;

    private void Awake()
    {
        SetupObjects();
        VerticalScroller = new UIVerticalScroller(ScrollingViewPort, scrollObjects, ScrollCenter);
        VerticalScroller.Start();
    }

    private void SetupObjects()
    {
        int count = EndValue - StartValue;
        int[] numbers = new int[count];
        scrollObjects = new GameObject[numbers.Length];
        for (int i = 0; i < count; i++)
        {
            numbers[i] = i + StartValue;
            GameObject clone = (GameObject)Instantiate(scrollItemPrefab, new Vector3(0, i * 80, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            clone.transform.SetParent(ScrollingViewPort, false);
            clone.transform.localScale = new Vector3(1, 1, 1);
            clone.GetComponentInChildren<Text>().text = "" + numbers[i];
            clone.name = "BPM_" + numbers[i];
            clone.AddComponent<CanvasGroup>();
            scrollObjects[i] = clone;
        }
    }

    private void Update()
    {
        VerticalScroller.Update();
    }
}
