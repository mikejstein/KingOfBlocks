using UnityEngine;
using System.Collections;

public class FlyScript : MonoBehaviour {
    public GameObject head;
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
