using UnityEngine;
using System.Collections;

public class FlyScript : MonoBehaviour {
    public GameObject head;
    public GameObject rig;
    public ParticleSystem smoke;

    private bool _isActive;
    public bool isActive {
        get { return _isActive; }
        set
        {
            _isActive = value;
            if (_isActive)
            {
                smoke.Play();
                // DO SOMETHING HERE
            } else
            {
                smoke.Stop();
            }
        }
    }
    // Use this for initialization
    void Start () {
        isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 headLocation = head.transform.position;
        Vector3 rigLocation = head.transform.position;
        gameObject.transform.position = new Vector3(headLocation.x, rigLocation.y - 2, headLocation.z);
        if ((rigLocation.y > 0) && (!isActive))
        {
            Debug.Log("MAKE ACTGIVE");
            isActive = true;
        } else if ((rigLocation.y == 0) && (isActive))
        {
            Debug.Log("KILL");
            isActive = false;
        }
	}
}
