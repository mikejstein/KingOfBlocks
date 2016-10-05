using UnityEngine;
using System.Collections;

public class FireObject : MonoBehaviour {

   public ParticleSystem regularFire;
    public ParticleSystem magicFire;

	// Use this for initialization

    void Awake()
    {
        stopFire();
    }
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void startFire()
    {
        regularFire.Play();
        magicFire.Play();
    }

    public void stopFire()
    {
        regularFire.Stop();
        magicFire.Stop();
    }
}
