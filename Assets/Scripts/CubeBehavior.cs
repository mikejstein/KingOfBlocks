using UnityEngine;
using DG.Tweening;
using System;

public class CubeBehavior : MonoBehaviour {


    private Color defaultColor;
	private Color highlightColor = new Color(0.0f, 1.0f, 0.0f);
	private Color selectedColor = new Color(0.0f, 0.0f, 1.0f);
    private Renderer myRenderer;

    private Rigidbody rb;
	private bool isSelected = false;

    private bool onGround;
    public static int groundCount = 0;
    public float mass = 100f;

    private Quaternion initialRotation;

    public AudioSource boxHitSource;

    private bool allowSound = false;

//    public ScoreDisplay scoreDisplay;
    


	// Use this for initialization
	void Start () {
        DOTween.Init();

		rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = mass;
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
        rb.mass = 0;
        BoxCollider myCol = rb.GetComponent<BoxCollider>();
        myCol.size = new Vector3(0.9f, 0.9f, 0.9f);
        isSelected = true;
		SetHighlight(selectedColor);

	}

	public void InTouch() {
		if(!isSelected) {
			SetHighlight(highlightColor);
		}
	}


	public void Released(Vector3 addForce = default(Vector3), Vector3 addRotForce = default(Vector3), bool soundOn = false) {
        rb.mass = mass;
        rb.velocity = addForce;
        rb.angularVelocity = addRotForce;
        allowSound = soundOn;
		rb.isKinematic = false;
		isSelected = false;
        BoxCollider myCol = rb.GetComponent<BoxCollider>();
        myCol.size = new Vector3(1.0f, 1.0f, 1.0f);
        OutTouch();
        ScoreDisplay.Instance.blockPlaced(gameObject.transform.position);
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
        else if ((collision.collider.tag == "Block") && (allowSound))
        {
            playCrash();
        }
    }

    private void playCrash()
    {
        if (!boxHitSource.isPlaying) { 
            boxHitSource.Play();
        }
    }


    public void reset(Action callback)
    {
       
        onGround = false;
        allowSound = false;        
        rb.isKinematic = true;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(initialPosition, 1.5f));
        seq.Join(transform.DORotate(initialRotation.eulerAngles, 1.5f));
        seq.AppendCallback(()=>completedReset(callback));
    }

    private void completedReset(Action callback)
    {
        callback(); //callback hits cube.released at it's last action
    }

    public void setSound(bool soundState)
    {
        allowSound = soundState;
    }



}
