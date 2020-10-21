using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : MonoBehaviour
{

    [SerializeField] private int shots = 5;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletEnemy")
        {
            shots--;
            Destroy(collision.gameObject);
        }


    }
    // Update is called once per frame
    void Update()
    {

        if (shots < 0)
        {
            Destroy(gameObject);
        }
    }
}
