using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LevelGeneration : MonoBehaviour
{
    private const float scrollAcceleration = 1.0f;
    private static Vector2 finalBounds; public static Vector2 Bounds => finalBounds;
    public static void SetScrollStatus(bool status) => canScroll = status;
    public static void SetClampFallSpeedStatus(bool status) => clampedFallSpeed = status;
    public static void SetScrollDirection(float dir) => scrollDir = dir;
    public static void SetObsMoveStatus(bool status) => obsCanMove = status;


    [SerializeField] private RuleData[] typePool;
    [SerializeField] private uint obstacleAmount = 5;
    [SerializeField] private float scrollSpeed = .5f;
    [SerializeField] private float maxScrollSpeed = 10.0f;
    [SerializeField] private Vector2 boundingBox;
    [SerializeField] private float startOffset = 5;
    [SerializeField] private float scrollSpeedAdditionPerSecond = .001f;
    private List<Rule> obstaclePool = new List<Rule>();
    private static bool clampedFallSpeed = true;
    private static bool canScroll = true;
    private static bool obsCanMove = true;
    private static float scrollDir = 1.0f;
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
        finalBounds.x *= (float)Screen.width / 1920.0f;
        finalBounds.y *= (float)Screen.height / 1080.0f;
    }

    private void ComputeMoveLaneWS(Rule target)
    {
        float moveSize = Random.Range(0, boundingBox.x * .2f);
        float movePivot = Random.Range(boundingBox.x * -.6f, boundingBox.x * .6f);
        Vector3 randomLane;
        randomLane.x = movePivot - moveSize;
        randomLane.y = movePivot + moveSize;
        randomLane.z = Random.Range(0, 5);

        target.MoveLane = randomLane;
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
        float heightWS = index * ((boundingBox.y * 2) / (float)obstacleAmount);
        float horizontalWS = Random.Range(-boundingBox.x, boundingBox.x);

        GameObject obstacle = new GameObject();
        obstacle.name = "Obstacle_Rule";
        obstacle.transform.position = new Vector3(
            horizontalWS,
            heightWS - startOffset,
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
        ComputeMoveLaneWS(target);
    }
    
    private void MoveRules()
    {
        float fallSpeed = (scrollSpeed + (Time.time * scrollSpeedAdditionPerSecond)) * scrollDir;

        if (clampedFallSpeed) fallSpeed = Mathf.Clamp(fallSpeed, -maxScrollSpeed, maxScrollSpeed);
        else fallSpeed += (Time.time * scrollSpeedAdditionPerSecond * .25f) * scrollDir;

        currentScrollSpeed = Mathf.SmoothDamp(currentScrollSpeed, (canScroll ? fallSpeed : 0), ref rf0, scrollAcceleration);

        foreach (Rule item in obstaclePool)
        {
            bool outOfP = item.transform.position.y > boundingBox.y;
            bool outOfN = item.transform.position.y < -boundingBox.y;
            bool outOfBounds = outOfP || outOfN;
            if (outOfBounds && item.WasInside)
            {
                item.transform.position = new Vector3(
                Random.Range(-boundingBox.x, boundingBox.x),
                item.transform.position.y - boundingBox.y * 2 * (outOfP? 1.0f : -1.0f),
                item.transform.position.z
                );
                AssignRandomType(item);
            }
            else
            {
                if(!outOfBounds) item.WasInside = true;

                Vector4 w = item.MoveLane;
                if (obsCanMove) w.w += Time.deltaTime * item.MoveLane.z;
                item.MoveLane = w;
                float xPos = Mathf.Lerp(
                    item.MoveLane.x,
                    item.MoveLane.y,
                    (Mathf.Sin(item.MoveLane.z * item.MoveLane.w) + 1.0f) * .5f
                    );

                item.transform.position = new Vector3(
                xPos,
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
