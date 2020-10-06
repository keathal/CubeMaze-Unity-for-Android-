using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour {

    bool enable;
    Gyroscope gyro;
	// Use this for initialization
	void Start () {
        enable = enableGyro();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    bool enableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        else
            return false;
    }
}
