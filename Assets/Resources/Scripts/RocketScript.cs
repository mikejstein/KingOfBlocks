using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {
    private bool move = false;
    private Vector3 endPosition;
    public GameObject endObject;
    private Vector3 startPosition;
    private float speed = 7.0f;
    private float startTime;
    private float journeyLength;

    public ParticleSystem explosion;
    public AudioSource explosionSound;
    // Use this for initialization
    void Start () {
        endPosition = endObject.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
	    if (move)
        {
            float distCovered = (Time.time - startTime) * speed; //distance = timeElapsed * speed
            float fracJourney = distCovered / journeyLength; //what percentage of the trip you've made
            gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
            gameObject.transform.LookAt(endPosition);
            if ((fracJourney > 0.70) && (explosion.isPlaying != true))
            {

                explosion.Play();
                StartCoroutine(playSound());

            }
            if (gameObject.transform.position == endPosition)
            {
                move = false;
                StopFire();                
                ScoreDisplay.Instance.resetScore();
                Destroy(gameObject);
            }
        }
	}

    IEnumerator playSound()
    {
        yield return new WaitForSeconds(0.5f);
        explosionSound.Play();
    }


    void StartFire()
    {
        gameObject.BroadcastMessage("startFire", SendMessageOptions.RequireReceiver);
    }

    void StopFire()
    {
        gameObject.BroadcastMessage("stopFire", SendMessageOptions.RequireReceiver);
    }

    public void Go(Vector3 destination)
    {
        if (move == false) {
            StartFire();
            startPosition = gameObject.transform.position;
            move = true;
            startTime = Time.time; //what time when i start
            journeyLength = Vector3.Distance(gameObject.transform.position, endPosition); // how long i have to travel 
        }
    }
}
