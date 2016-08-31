using UnityEngine;
using System.Collections;

public class ClothObject : MonoBehaviour {

    public float thrust = 0.001f;
    public Rigidbody rb;
    public Cloth cloth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Controller")
        {
            fly();
        }
    }

    private void fly()
    {
        cloth.useGravity = false;
        cloth.damping = 0;
        cloth.randomAcceleration = new Vector3(0.3f, 0.3f, 0.3f);
        rb.AddForce(thrust, thrust, 0, ForceMode.Impulse);
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        Renderer renderer = cloth.GetComponent<Renderer>();
        for (float f = 1f; f >= 0; f -= 0.00001f)
        {

            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return null;
        }
        Destroy(gameObject);
    }
}
