using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour {
	private bool grabbed = false;
	private CubeBehavior touchedBlock = null;

	public SteamVR_TrackedController controller;
	// Use this for initialization
	void Start () {
		controller = this.transform.parent.gameObject.GetComponent<SteamVR_TrackedController>();
		controller.TriggerClicked += Controller_TriggerClicked;
		controller.TriggerUnclicked += Controller_TriggerUnclicked;
	}

	void Controller_TriggerUnclicked (object sender, ClickedEventArgs e)
	{
		grabbed = false;
		if (touchedBlock != null) {
			touchedBlock.Released ();
			touchedBlock.transform.parent = null;
			touchedBlock = null;
		}
	}

	void Controller_TriggerClicked (object sender, ClickedEventArgs e)
	{
		grabbed = true;
		if (touchedBlock != null) {
			touchedBlock.Grabbed ();
			touchedBlock.transform.parent = this.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Block")  { // I moved onto a block
			if (!grabbed) { //I'm not currently holding onto a block
				CubeBehavior block = col.gameObject.GetComponent<CubeBehavior> ();
				touchedBlock = block;
				block.InTouch ();
			}
		} 
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Block") {
			if (!grabbed) { // I'm not currenlty holding on to a block
				touchedBlock = null;
				CubeBehavior block = col.gameObject.GetComponent<CubeBehavior> ();
				block.OutTouch ();
			}
		}
	}
}
