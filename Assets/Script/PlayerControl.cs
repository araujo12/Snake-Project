using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Vector3 target, saveDir;
    public Transform bodyPrefab, foodPrefab, foodInGame;
    public List<Transform> childlist;
    public bool lockKeyBoard;
    public int points;
    public static float speed;
    public Text pointsText;
   
    
    void Start()
    {
        target = transform.position;
        saveDir = Vector3.up;
        foodInGame = FoodSpawn();
        points = 0;
        speed = 5;

    }

    
    void Update()
    {
        MoviSnake();
    }

    void MoviSnake()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (!lockKeyBoard && dir.x != 0 && dir.x != saveDir.x * -1)
        {
            lockKeyBoard = true;
            saveDir = Vector3.right * dir.x;
        }

        if (!lockKeyBoard && dir.y != 0 && dir.y != saveDir.y * -1)
        {
            lockKeyBoard = true;
            saveDir = Vector3.up * dir.y;
        }

        if (transform.position == target)
        {
            target += saveDir;

            PositionCheck();
            SetNewTarget();
            lockKeyBoard = false;
        }
    }

    private void PositionCheck()
    {
        if (target.x >= 30 || target.x <= -30 || target.y >= 16 || target.y <= -16)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            speed = 5;
        }

        foreach (Transform t in childlist)
        {
            if (target == t.position)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                speed = 5;
            }
        }


        if (foodInGame != null && transform.position == foodInGame.position)
        {
            points = points + 1;
            pointsText.text = points.ToString();
            Destroy(foodInGame.gameObject);

            Transform obj = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
            obj.GetComponent<SnakeBody>().WaitHeadUpdates(childlist.Count);
            childlist.Add(obj);

            foodInGame = FoodSpawn();

            speed += 0.1f;
        }
    }


    private Transform FoodSpawn()
    {
        return Instantiate(foodPrefab, new Vector3(Random.Range(-29, 29), Random.Range(-15, 15), 0), Quaternion.identity);
    }

    private void SetNewTarget()
    {
        if (childlist.Count > 0)
        {
            childlist[0].GetComponent<SnakeBody>().SetTarget(transform.position);
            for (int a = childlist.Count - 1; a > 0; a--)
            {
                Vector3 p = new Vector3(Mathf.RoundToInt(childlist[a - 1].position.x), Mathf.RoundToInt(childlist[a - 1].position.y), 0);
                childlist[a].GetComponent<SnakeBody>().SetTarget(p);
            }
        }
    }
}
