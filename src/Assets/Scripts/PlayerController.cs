using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject wing;
    public float windsAngle = 0f;
    public float wingsRotationSpeed;
    public float flyingPower;

    public float forceMagnitude;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.right * forceMagnitude, 0.1f);
    }

    void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = -Input.GetAxis("Horizontal");
        windsAngle += (vInput + hInput) * wingsRotationSpeed * Time.fixedDeltaTime;
        windsAngle = Mathf.Clamp(windsAngle, -30f, 30f);

        wing.transform.rotation = Quaternion.Euler(0f, 0f, windsAngle + rb.rotation);
        float planeAngle = rb.rotation;

        if (Input.GetKey(KeyCode.Space)) 
        {
            Vector2 dir = GetVectorByAngle(planeAngle + windsAngle);
            Vector2 pos = GetVectorByAngle(rb.rotation) * forceMagnitude;
            rb.AddForceAtPosition(dir * flyingPower, (Vector2)transform.position + pos);
        }
    }

    private Vector2 GetVectorByAngle(float angle) 
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}
