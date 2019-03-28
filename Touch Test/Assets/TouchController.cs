using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchController : MonoBehaviour  {     // Update is called once per frame     private Touch touch; 
    private void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }
     //private void Update ()      //{     //    for (int i = 0; i < Input.touchCount; i++)     //    {     //        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);     //        Debug.DrawLine(Vector3.zero, touchPosition, Color.red);     //    }     //    //print($"Does update actually work: {Time.time}");     //    if (Input.touchCount > 0)     //    {     //        Touch touch = Input.GetTouch(0);     //        //if (touch)     //        //{      //        //}     //        print($"touch screen position: { touch.position}");     //        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);     //        touchPosition.z = 0f;     //        print($"touch world position: { touchPosition.x}, { touchPosition.y}");     //    }     //}

    public void OnDragDelegate(PointerEventData data)
    {
        Debug.Log("Dragging.");
    }
 }
