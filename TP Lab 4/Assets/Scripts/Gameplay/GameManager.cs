using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;
    public GameObject _player => player;
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;
    public bool gameOver = false;

    public int meteorCount = 0;

    // Assignable reference to the Cinemachine camera in the scene.
    public CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        if (Instance == null)
            Instance = this;
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        if (vcam != null)
        {
            vcam.Follow = player.transform;
            vcam.LookAt = player.transform;
        }

        InvokeRepeating("SpawnMeteor", 1f, 2f);
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += GameOver;
        MeteorHealth.OnMeteorDeath += MeteorCounting;
    }

    private void MeteorCounting(int obj)
    {
        meteorCount++;
    }

    private void GameOver(GameObject player)
    {
        gameOver = true;
        player.SetActive(false);
    }

    // Restarts the game/scene when the respawn input is pressed and the game has ended.
    private void OnRespawn(InputValue value)
    {
        if (value.isPressed && gameOver)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= GameOver;
        MeteorHealth.OnMeteorDeath  -= MeteorCounting;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            CancelInvoke();
        }
 

        if (meteorCount == 5)
        {
            BigMeteor();
        }
    }

    void SpawnMeteor()
    {
        Instantiate(meteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }

    void BigMeteor()
    {
        meteorCount = 0;
        Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }
}
