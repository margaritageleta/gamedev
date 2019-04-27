using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float speed=10f;
    public float range = 20f;
    Vector3 origin;
    GameObject target;
    Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        origin = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(transform.forward*speed);

       if ((origin-transform.position).magnitude >= range)
            Destroy(this.gameObject);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hunter"))
             Destroy(this.gameObject);
    }

    public void ReceiveTarget(GameObject toLook)
    {
        target = toLook;
        transform.LookAt(target.transform);
    }


}




