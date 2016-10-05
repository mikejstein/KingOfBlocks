using UnityEngine;
using System.Collections;

public class FlyScript : MonoBehaviour {
    public GameObject head;
    public FireObject fire;
    public float pof;
    private Quaternion fireRotation;
    private Vector3 fireDistance;


    private bool _isActive;
    public bool isActive {
        get { return _isActive; }
        set
        {
            _isActive = value;
            if (_isActive)
            {
                fire.startFire();
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

    private void setPosition(Vector3 cameraPosition)
    {
        Vector3 positionOffset = new Vector3(cameraPosition.x, cameraPosition.y - pof, cameraPosition.z);
        gameObject.transform.position = positionOffset;
    }
	
	// Update is called once per frame
	void Update () {
        setPosition(head.transform.position);
	}



}
