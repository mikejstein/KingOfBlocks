using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	private Color defaultColor;
	private Color highlightColor = new Color(0.0f, 1.0f, 0.0f);
	private Color selectedColor = new Color(0.0f, 0.0f, 1.0f);
	private bool isSelected = false;
	private Renderer myRenderer;



	// Use this for initialization
	void Start () {
		myRenderer = gameObject.GetComponent<Renderer>();
		defaultColor  = myRenderer.material.color;

	}

	private void SetHighlight(Color color) {
		myRenderer.material.color = color;
	}

	private void RemoveHighlight() {
		myRenderer.material.color = defaultColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Grabbed() {
		isSelected = true;
		SetHighlight(selectedColor);

	}

	public void InTouch() {
		if(!isSelected) {
			SetHighlight(highlightColor);
		}
	}

	public void Released() {
		isSelected = false;
		OutTouch();
	}

	public void OutTouch() {
		RemoveHighlight();
	}
}
