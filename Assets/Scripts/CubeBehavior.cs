using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	private Color defaultColor;
	private Color highlightColor = new Color(0.0f, 1.0f, 0.0f);
	private Color selectedColor = new Color(0.0f, 0.0f, 1.0f);
    private Renderer myRenderer;

    private Rigidbody rb;
	private bool isSelected = false;

    private bool onGround;
    public static int groundCount;
    public static int towerHeight = 0;

    private Quaternion initialRotation;
    private Vector3 initialPosition;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		myRenderer = gameObject.GetComponent<Renderer>();
		defaultColor  = myRenderer.material.color;
        initialRotation = rb.transform.rotation;
        initialPosition = rb.transform.position;

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
		rb.isKinematic = true;
		isSelected = true;
		SetHighlight(selectedColor);

	}

	public void InTouch() {
		if(!isSelected) {
			SetHighlight(highlightColor);
		}
	}

	public void Released() {
		rb.isKinematic = false;
		isSelected = false;
		OutTouch();
	}

	public void OutTouch() {
		RemoveHighlight();
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (!onGround)
            {
                onGround = true;
                CubeBehavior.groundCount++;
            }
        }
    }



    public void reset()
    {
        onGround = false;
        gameObject.transform.position = initialPosition;
        gameObject.transform.rotation = initialRotation;
        rb.velocity = new Vector3(0, 0, 0);
        Released();
    }
}
