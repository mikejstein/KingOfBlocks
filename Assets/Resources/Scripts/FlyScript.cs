using UnityEngine;
using System.Collections;

public class FlyScript : MonoBehaviour {
    public GameObject head;
    public FireObject fire;

    private bool _isActive;
    public bool isActive {
        get { return _isActive; }
        set
        {
            _isActive = value;
            if (_isActive)
            {
                fire.startFire();
                // DO SOMETHING HERE
            } else
            {
                fire.stopFire();
            }
        }
    }
    // Use this for initialization
    void Start () {
        isActive = false;
	}

    public void toggleActive(bool state)
    {
        if (isActive != state)
        {
           isActive = state;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
