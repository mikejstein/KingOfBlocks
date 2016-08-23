using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public SteamVR_TrackedController leftController;
	public SteamVR_TrackedController rightController;

	private bool rightTriggered = false;
	private bool leftTriggered = false;

	private bool rightGripped = false;
	private bool leftGripped = false;

	/*
	 * Grip left to fly up, grip right to fly down. Definitely need to mix 'on trigger enter'
	 * Walk on ground like normal
	 * Fly in air with d-pads for movement
	 * grab with either hand
	 */

	// Use this for initialization
	void Start () {

		leftController.TriggerClicked += LeftController_TriggerClicked;
		rightController.TriggerClicked += RightController_TriggerClicked;

		leftController.TriggerUnclicked += LeftController_TriggerUnclicked;
		rightController.TriggerUnclicked += RightController_TriggerUnclicked;

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

	/*
	 * Triggers
	 */
	void RightController_TriggerUnclicked (object sender, ClickedEventArgs e)
	{
		rightTriggered = false;
	}

	void LeftController_TriggerUnclicked (object sender, ClickedEventArgs e)
	{
		leftTriggered = false;
	}

	void RightController_TriggerClicked (object sender, ClickedEventArgs e)
	{
		rightTriggered = true;
	}

	void LeftController_TriggerClicked (object sender, ClickedEventArgs e)
	{
		leftTriggered = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GrabADab(
}
