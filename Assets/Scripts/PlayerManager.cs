using UnityEngine;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour {
	public SteamVR_TrackedController leftController;
	public SteamVR_TrackedController rightController;
    public SteamVR_Camera VRcamera;
    public FlyScript flyer;

	private bool rightGripped = false;
	private bool leftGripped = false;
    public RocketScript rocket;

    bool resetInAction = false;
    bool justFinishedReset = false;
    public AudioSource gameOverSource;

    CubeBehavior[] cubes;
    int cubeCount;

    /*
	 * Grip left to fly up, grip right to fly down. Definitely need to mix 'on trigger enter'
	 * Walk on ground like normal
	 * Fly in air with d-pads for movement
	 * grab with either hand
	 */

    // Use this for initialization
    void Start () {

        cubes = FindObjectsOfType<CubeBehavior>(); //Get all our cubes
        cubeCount = cubes.Length; //remeber how many there are


        leftController.Gripped += LeftController_Gripped;
		rightController.Gripped += RightController_Gripped;

		leftController.Ungripped += LeftController_UnGripped;
		rightController.Ungripped += RightController_Ungripped;

		leftController.PadClicked += PadClicked;
		rightController.PadClicked += PadClicked;

        FireRocket();

	}


	/*
	 * Grips
	 */
	void RightController_Ungripped (object sender, ClickedEventArgs e)
	{
		rightGripped = false;

	}

	void LeftController_UnGripped (object sender, ClickedEventArgs e)
	{
		leftGripped = false;
	}
		
	void RightController_Gripped (object sender, ClickedEventArgs e)
	{
		rightGripped = true;
	}

	void LeftController_Gripped (object sender, ClickedEventArgs e)
	{
		leftGripped = true;
	}


	/*
	 * Pads
	 */
	void PadClicked (object sender, ClickedEventArgs e)
	{
        FireRocket();
	}

    private void FireRocket()
    {
        Vector3 transform = ScoreDisplay.Instance.currentText.gameObject.transform.position;
        rocket.Go(transform);
    }
		
	
	// Update is called once per frame
	void Update () {
		if (leftGripped && !rightGripped) {
			gameObject.transform.Translate (Vector3.up * Time.deltaTime);
            flyer.toggleActive(true);
		} else if (rightGripped && !leftGripped)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime);
            Vector3 newPos = gameObject.transform.position;
            if (newPos.y < 0f)
            {
                flyer.toggleActive(false);
                gameObject.transform.position = new Vector3(newPos.x, 0, newPos.z);
            }
        }
        if (CubeBehavior.groundCount > 3)
        {
            CubeBehavior.groundCount = 0;
            if (!gameOverSource.isPlaying)
            {
                gameOverSource.Play();
            }
            if (!resetInAction)
            {
                StartCoroutine(ResetGame());
                resetInAction = true;
            } 
        }
        if (justFinishedReset)
        {
            justFinishedReset = false;
            foreach (CubeBehavior cube in cubes) //release all the cubes
            {
                cube.setSound(true);

            }
        }
    }


    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1.5f);
        int resetCount = cubeCount;

        Action resetDone = () => // Action that will run after a cube has been repositioned
        {
            resetCount--;
            if (resetCount == 0) //if all my cubes are in place
            { 
                resetInAction = false; //i'm done resetting
                foreach (CubeBehavior cube in cubes) //release all the cubes
                {
                    cube.Released(); //cubes are released
                }
                justFinishedReset = true; //and the rest has just finished

            }
        };

        foreach (CubeBehavior cube in cubes) //begin the replacing process
        {
            cube.reset(resetDone); //make sure to pass in the action as a callback
        }
    }






}
