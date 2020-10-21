using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{


    [SerializeField] private Color[] colors;
    public int color;
    private void Start()
    {
        color = (Random.Range(1, 100) % 4) + 1;
        this.GetComponent<SpriteRenderer>().color = colors[color - 1];
    }
}
