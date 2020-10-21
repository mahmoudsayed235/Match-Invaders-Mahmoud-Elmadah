using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float timeBetween2Bullets;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Text healthText;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject losing;
    private float nextBullet = 0;
    private int health = 3;
    void OnEnable()
    {
        health = 3;
        healthText.text = health.ToString();
        this.transform.position = new Vector3(0, -3.42f, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            this.gameObject.GetComponent<MainCharacter>().enabled = false;
            GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().Save();
            losing.SetActive(true);
            Time.timeScale = 0;

        }


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > bounds.x)
        {

            transform.position += Vector3.left * speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < bounds.y)
        {
            transform.position += Vector3.right * speed;
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > nextBullet)
        {
            nextBullet = Time.time + timeBetween2Bullets;
            Instantiate(bullet, transform.position, transform.rotation);
        }

    }
    public void getDamaged()
    {
        if (health <= 1)
        {
            this.gameObject.GetComponent<MainCharacter>().enabled = false;
            GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().Save();
            losing.SetActive(true);
            Time.timeScale = 0;
            return;

        }
        health--;
        healthText.text = health.ToString();
        GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>().clip = clip;
        GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>().Play();


    }
}
