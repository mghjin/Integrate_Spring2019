using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoControl : MonoBehaviour {

    Rigidbody rb;
    public GameObject VFX_sparkPrefab;
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 20, ForceMode.Impulse);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player")
        {
            GameObject vfx = Instantiate(VFX_sparkPrefab, transform.position, Quaternion.identity);
            Destroy(vfx, 2.0f);
            Destroy(this.gameObject);
        }
    }
}
