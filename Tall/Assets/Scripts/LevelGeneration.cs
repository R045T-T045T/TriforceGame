using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LevelGeneration : MonoBehaviour
{
    private const float scrollAcceleration = .1f;
    private static bool canScroll = true;
    private static Vector2 finalBounds; public static Vector2 Bounds => finalBounds;
    public static void SetScrollStatus(bool status) => canScroll = status;


    [SerializeField] private RuleData[] typePool;
    [SerializeField] private uint obstacleAmount = 5;
    [SerializeField] private float scrollSpeed = .5f;
    [SerializeField] private float maxScrollSpeed = 10.0f;
    [SerializeField] private Vector2 boundingBox;
    [SerializeField] private float startOffset = 5;
    [SerializeField] private float scrollSpeedAdditionPerSecond = .001f;
    private List<Rule> obstaclePool = new List<Rule>();
    private float currentScrollSpeed;

    private void Awake()
    {
        ComputeScreenBounds();
        PlaceInitialObstacles();
        finalBounds = boundingBox;
    }

    private void ComputeScreenBounds()
    {
        //full HD 16/9 as reference
        finalBounds.x *= (float)Screen.width / 1960.0f;
        finalBounds.y *= (float)Screen.height / 1060.0f;
    }

    private void PlaceInitialObstacles()
    {
        for (int i = 0; i < obstacleAmount; i++)
        {
            AddSingleObstacle(i);
        }
    }

    private void Update()
    {
        MoveRules();
    }


    private void AddSingleObstacle(int index)
    {
        float heightWS = index * ((boundingBox.y * 2) / (float)obstacleAmount) - startOffset;
        float horizontalWS = Random.Range(-boundingBox.x, boundingBox.x);

        GameObject obstacle = new GameObject();
        obstacle.name = "Obstacle_Rule";
        obstacle.transform.position = new Vector3(
            horizontalWS,
            heightWS,
            0
            );

        obstacle.transform.parent = transform;
        Rule r = obstacle.AddComponent<Rule>();
        obstaclePool.Add(r);

        AssignRandomType(r);
    }

    private void AssignRandomType(Rule target)
    {
        int ruleIndex = Random.Range(0, typePool.Length);
        RuleData type = typePool[ruleIndex];
        target.InitializeAs(type);
    }
    
    private void MoveRules()
    {
        float speedTarget = canScroll ? 
            Mathf.Clamp(scrollSpeed + (Time.time * scrollSpeedAdditionPerSecond), 0, maxScrollSpeed)
            : 0;
        currentScrollSpeed = Mathf.SmoothDamp(currentScrollSpeed, speedTarget, ref rf0, scrollAcceleration);

        foreach (Rule item in obstaclePool)
        {
            bool outOfBounds = item.transform.position.y > boundingBox.y;
            if (outOfBounds)
            {
                item.transform.position = new Vector3(
                Random.Range(-boundingBox.x, boundingBox.x),
                item.transform.position.y - boundingBox.y * 2,
                item.transform.position.z
                );
                AssignRandomType(item);
            }
            else
            {
                item.transform.position = new Vector3(
                item.transform.position.x,
                item.transform.position.y + currentScrollSpeed * Time.deltaTime,
                item.transform.position.z
                );
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, boundingBox * 2);
    }

    private float rf0;
}
