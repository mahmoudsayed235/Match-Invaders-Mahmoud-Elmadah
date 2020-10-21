using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float maxY;
    [SerializeField] private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.up * speed;
        if (transform.position.y > maxY)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>().clip = clip;
            GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>().Play();
            GameObject.FindGameObjectWithTag("EnemyContainer").GetComponent<Enemy>().destroyEnemies(int.Parse(collision.gameObject.name), collision.gameObject.GetComponent<EnemyManager>().color);
            //  Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        /* else if (collision.tag == "Protection")
         {
             Destroy(this.gameObject);
         }
         */
    }
}
