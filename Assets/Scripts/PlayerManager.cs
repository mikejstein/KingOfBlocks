using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public SteamVR_TrackedController leftController;
	public SteamVR_TrackedController rightController;    

	private bool rightGripped = false;
	private bool leftGripped = false;
    private bool lateFlag = false;

    private float maxWalkHeight = 999f;
	/*
	 * Grip left to fly up, grip right to fly down. Definitely need to mix 'on trigger enter'
	 * Walk on ground like normal
	 * Fly in air with d-pads for movement
	 * grab with either hand
	 */

	// Use this for initialization
	void Start () {


		leftController.Gripped += LeftController_Gripped;
		rightController.Gripped += RightController_Gripped;

		leftController.Ungripped += LeftController_UnGripped;
		rightController.Ungripped += RightController_Ungripped;

		leftController.PadClicked += PadClicked;
		rightController.PadClicked += PadClicked;

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
		
	}
		
	
	// Update is called once per frame
	void Update () {
		if (leftGripped && !rightGripped) {
			gameObject.transform.Translate (Vector3.up * Time.deltaTime);
		} else if (rightGripped && !leftGripped)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime);
            Vector3 newPos = gameObject.transform.position;
            if (newPos.y < 0f)
            {
                gameObject.transform.position = new Vector3(newPos.x, 0, newPos.z);
            }
        }
        //Debug.Log("Count: " + CubeBehavior.groundCount);
        if (CubeBehavior.groundCount > 3)
        {
            StartCoroutine(ResetGame());

        }
    }


    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(3);
        CubeBehavior.groundCount = 0;
        CubeBehavior[] cubes = FindObjectsOfType<CubeBehavior>();
        foreach (CubeBehavior cube in cubes)
        {
            cube.reset();
        }
    }




}
