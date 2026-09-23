using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject playerPrefab;
    public GameObject _playerPrefab => playerPrefab; 
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;
    public bool gameOver = false;

    public int meteorCount = 0;

    // Assignable reference to the Cinemachine camera in the scene.
    public CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;    
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        if (vcam != null)
        {
            vcam.Follow = player.transform;
            vcam.LookAt = player.transform;
        }

        InvokeRepeating("SpawnMeteor", 1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            CancelInvoke();
        }

        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            SceneManager.LoadScene("Week5Lab");
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
