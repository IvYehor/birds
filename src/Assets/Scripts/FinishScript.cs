using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour
{
    public Transform playerTransform;

    public Text DistText;

    public bool finished = false;

    private void Update()
    {
        DistText.text = (transform.position.x - playerTransform.position.x).ToString() + "m left";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            finished = true;
        }
    }
}
