using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class GameLogic : MonoBehaviour
{
    private static GameLogic instance;

    public static GameLogic Instance()
    {
        if(instance == null)
        {
            instance = new GameLogic();
        }

        return instance;
    }

    [SerializeField] Transform bottomLimit;
    [SerializeField] Transform topLimit;
    [SerializeField] TextMeshProUGUI scoreText;
    [Space(5)]
    [SerializeField] List<GameObject> obstacles = new List<GameObject>();

    private new ParticleSystem particleSystem;

    [SerializeField] private float minimum_spawnTime;
    [SerializeField] private float maximum_spawnTime;
    [SerializeField] private float minimum_offsetLeft;
    [SerializeField] private float maximum_offsetRight;
    [SerializeField] private float minimum_spawnedObstacles;
    [SerializeField] private float maximum_spawnedObstacles;
    [SerializeField] private float lerpTime;
    [SerializeField] private float obstacleSpeed;
    [SerializeField] private float difficultyRate = 20;
    [SerializeField] private float difficultyMultiplier = 0.99f;

    public static int score = 0;
    
    private float current_spawnTime;
    private float current_score = 0;
    private float base_simulationSpeed = 0;

    private Vector2 _bottomLimit;
    private Vector2 _topLimit;

    private static bool changeScore = false;
    private static bool boost = false;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        current_spawnTime = Random.Range(minimum_spawnTime, maximum_spawnTime);

        _bottomLimit = bottomLimit.position;
        _topLimit = topLimit.position;
        Destroy(bottomLimit.gameObject);
        Destroy(topLimit.gameObject);

        base_simulationSpeed = particleSystem.main.simulationSpeed;

        score = 0;
    }

    private void FixedUpdate()
    {
        current_spawnTime -= Time.fixedDeltaTime;
        if(current_spawnTime <= 0)
        {
            for(int i = 0; i < Mathf.Round(Random.Range(minimum_spawnedObstacles, maximum_spawnedObstacles)); i++ )
            {
                GameObject bullet = Instantiate(obstacles[Random.Range(0, obstacles.Count)],
                                                new Vector2(_bottomLimit.x + Random.Range(minimum_offsetLeft, maximum_offsetRight),
                                                Random.Range(_bottomLimit.y, _topLimit.y)), Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-obstacleSpeed, 0);
                current_spawnTime = Random.Range(minimum_spawnTime, maximum_spawnTime);
            }
        }

        if (changeScore)
        {
            scoreText.text = $"SCORE:{score}";
            changeScore = false;
        }

        if (boost)
        {
            var main = particleSystem.main;
            main.simulationSpeed = Mathf.Lerp(main.simulationSpeed, 4, lerpTime * Time.fixedDeltaTime);
        }
        else
        {
            var main = particleSystem.main;
            main.simulationSpeed = Mathf.Lerp(main.simulationSpeed, base_simulationSpeed, lerpTime * Time.fixedDeltaTime);
        }

        if(score >= current_score + difficultyRate)
        {
            minimum_spawnedObstacles /= difficultyMultiplier;
            maximum_spawnedObstacles /= difficultyMultiplier;
            obstacleSpeed /= difficultyMultiplier;
            obstacleSpeed = Mathf.Clamp(obstacleSpeed, 0, 6.5f);
            minimum_spawnTime *= difficultyMultiplier;
            maximum_spawnTime *= difficultyMultiplier;
            difficultyRate /= difficultyMultiplier;
            var main = particleSystem.main;
            main.simulationSpeed = base_simulationSpeed /= difficultyMultiplier;


            float newValue = Mathf.Pow(difficultyMultiplier, 2);
            difficultyMultiplier = newValue;
            current_score = score;
        }
    }

    public static void SetScore()
    {
        score += 1;
        changeScore = true;
    }

    public static void Boost(bool value)
    {
        boost = value;
    }
}
