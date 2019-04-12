using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySpin : MonoBehaviour {

    private Rigidbody rigidbody;
    private float amount = 50f;
	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody>();
	}

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Vertical") * amount * Time.deltaTime;
        if (h > 0)
        {
            print($"H: {h} - input: {Input.GetAxis("Vertical")}");
        }
        rigidbody.AddTorque(transform.up * h);

    }
}
