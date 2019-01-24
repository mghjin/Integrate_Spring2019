using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoControl : MonoBehaviour {

    Rigidbody rb;
    public GameObject VFX_sparkPrefab;
    public float ammoDamage = 100f;
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 20, ForceMode.Impulse);
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<EnemyControl>().GetHit(ammoDamage);
            GameObject vfx = Instantiate(VFX_sparkPrefab, transform.position, Quaternion.identity);
            Destroy(vfx, 2.0f);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject != this.gameObject)
        {
            GameObject vfx = Instantiate(VFX_sparkPrefab, transform.position, Quaternion.identity);
            Destroy(vfx, 2.0f);
            Destroy(this.gameObject);
        }
    }

}
