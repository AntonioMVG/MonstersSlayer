using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private Rigidbody2D col;
    private HUDController hud;

    private void Start()
    {
        col = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            col.constraints = RigidbodyConstraints2D.FreezePosition;
            // Placing the Acorn above the Ground
            col.transform.position = new Vector2(col.transform.position.x, col.transform.position.y + 0.3f);
        }
    }
}
