using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartFire();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void StartFire()
    {
        gameObject.BroadcastMessage("startFire");
    }

    void StopFire()
    {
        gameObject.BroadcastMessage("stopFire");
    }
}
