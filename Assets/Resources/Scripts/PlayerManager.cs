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
    public static bool spawnOpen = true;
    private int spawnCount = 1;
    /*
	 */



    // Use this for initialization
    void Start () {

        CubeBehavior.spawnHitDelegate += SpawnCubes;
        UpdateCubes();

        leftController.Gripped += LeftController_Gripped;
		rightController.Gripped += RightController_Gripped;

		leftController.Ungripped += LeftController_UnGripped;
		rightController.Ungripped += RightController_Ungripped;

		leftController.PadClicked += PadClicked;
		rightController.PadClicked += PadClicked;
  
	}

    private void UpdateCubes()
    {
        cubes = FindObjectsOfType<CubeBehavior>(); //Get all our cubes
        cubeCount = cubes.Length; //remember how many there are
        foreach (CubeBehavior cube in cubes) {
            cube.setInitial(); // update their initial position in case of explosion
        }
    }


    private void spawnLayer(Quaternion orient)
    {
        CubeBehavior dummy = cubes[0]; // get a base cube
        Transform cubeParent = dummy.transform.parent; // get the parent of the cube (a grid of three)
        Transform stack = cubeParent.parent; // Get the parent of the grid (the whole stack)
        Vector3 currentParentPosition = stack.position; // get the current position of the stack
        Vector3 addHeight = new Vector3(0, dummy.transform.lossyScale.y, 0); // raise the stack up by the height of a block

        stack.position = currentParentPosition + addHeight;

        Vector3 newRowPosition = new Vector3(stack.position.x, 0, stack.position.z);
        Transform newRow = (Transform)Instantiate(cubeParent, newRowPosition, orient);

        newRow.SetParent(stack);

        //add the new cubes to 
    }

    private void repositionSpawn(Transform spawnTrigger)
    {
        Vector3 newSpawnPostion = new Vector3(spawnTrigger.position.x, spawnTrigger.position.y + (spawnTrigger.position.y / spawnCount), spawnTrigger.position.z);// Move the spawn point up by the height of the platform
        spawnCount++;
        spawnTrigger.position = newSpawnPostion;

    }

    void SpawnCubes(Transform spawnTrigger)
    {
        PlayerManager.spawnOpen = false; //prevent spawn from happening right now
        spawnLayer(Quaternion.Euler(0, 90, 0)); //build one platform
        spawnLayer(Quaternion.identity); // build another platform
        UpdateCubes(); // add the new cubes to our internal state
        CubeBehavior.groundCount = -3; // resest ground count to handle it
        repositionSpawn(spawnTrigger);
        PlayerManager.spawnOpen = true;

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
        rocket.Go();
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
