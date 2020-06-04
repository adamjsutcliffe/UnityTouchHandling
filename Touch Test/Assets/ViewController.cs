using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour 
{
    [SerializeField] private float cubeSF = 6f;

	void Start () 
    {
        Camera.main.orthographicSize = 6f / Screen.width * Screen.height / 2.0f;
        print($"Camera orthagraphic size: {Camera.main.orthographicSize}");
	}

    private void Update()
    {
        Camera.main.orthographicSize = cubeSF / Screen.width * Screen.height / 2.0f;
    }

    public void SettingHandler()
    {
        print("SETTINGS");
    }

    public void RandomiseHandler()
    {
        print("RANDOMISE");
    }

    public void PreviewHandler()
    {
        print("PREVIEW");
    }

    public void PlayLoopHandler()
    {
        print("PLAY LOOP");
    }
}
