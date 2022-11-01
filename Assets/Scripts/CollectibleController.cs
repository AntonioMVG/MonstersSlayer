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
        }
    }

    private void InfoCollectibles()
    {
        int collectiblesNum = GameObject.FindGameObjectsWithTag("Collectible").Length;
        hud.SetCollectiblesTxt(collectiblesNum);

        if (collectiblesNum == 0)
        {
            
        }
    }
}
