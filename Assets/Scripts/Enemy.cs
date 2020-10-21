using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject bulletEnemy;
    [SerializeField] private float speed;
    public List<GameObject> Enemies;
    [SerializeField] private float timeBetween2Bullets;
    public int nRows, nCols;
    float timeFrame = .7f;

    private void Start()
    {
        InvokeRepeating("moving", 0.1f, timeFrame);

    }
    int direction = 1;
    void moving()
    {
        transform.position += Vector3.right * speed * direction;
        foreach (Transform enemy in this.transform)
        {

            if (enemy.position.x < -9 || enemy.position.x > 9)
            {
                direction *= -1;
                transform.position += Vector3.down * speed;
                transform.position += Vector3.right * speed * direction;
                return;
            }

            if (noDown(int.Parse(enemy.gameObject.name) + nCols))
            {
                if (GameObject.FindGameObjectsWithTag("BulletEnemy").Length < 5)
                {


                    Instantiate(bulletEnemy, enemy.position, enemy.rotation);
                }
            }





        }

        if (transform.childCount == 0)
        {
            GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().levelCounter++;
            GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().Reset();
            //player win
        }
    }
    bool noDown(int I)
    {
        //  print("Down : " + I + "   ."+ Enemies[I - 1].gameObject.active+".");
        if (I > Enemies.Count || Enemies[I - 1] == null)
        {
            return true;
        }
        return false;
    }

    public void moveFaster()
    {
        CancelInvoke();
        timeFrame -= 0.02f;
        InvokeRepeating("moving", 0.1f, timeFrame);
    }

    public void destroyEnemies(int index, int color)
    {
        Destroy(Enemies[index - 1]);
        Enemies[index - 1] = null;
        int up = index - nCols;
        int down = index + nCols;
        int right = index + 1;
        int left = index - 1;
        int N = 1 + destroyUP(up, color) + destroyDOWN(down, color) + destroyRight(right, color) + destroyLeft(left, color);
        GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().IncrementScore(N);

    }

    int destroyUP(int I, int color)
    {
        if (I < 1)
        {
            return 0;
        }
        return DestroyEnemy(I - 1, color);
    }
    int destroyDOWN(int I, int color)
    {
        if (I > Enemies.Count)
        {
            return 0;
        }
        return DestroyEnemy(I - 1, color);
    }
    int destroyLeft(int I, int color)
    {
        if (I % nCols == 0)
        {
            return 0;
        }
        return DestroyEnemy(I - 1, color);
    }
    int destroyRight(int I, int color)
    {
        if (I % nCols == 1)
        {
            return 0;
        }
        return DestroyEnemy(I - 1, color);
    }
    int DestroyEnemy(int index, int color)
    {
        if (Enemies[index] != null && color == Enemies[index].GetComponent<EnemyManager>().color)
        {
            Destroy(Enemies[index]);
            Enemies[index] = null;
            moveFaster();
            return 1;
            //Enemies[index] = null;
        }
        return 0;
    }

}
