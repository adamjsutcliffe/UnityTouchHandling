using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour 
{
    [SerializeField] private Transform cube;

	void Start () 
    {
        Camera.main.orthographicSize = 6f / Screen.width * Screen.height / 2.0f;
        print($"Camera orthagraphic size: {Camera.main.orthographicSize} cube: {cube.localScale}");
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
