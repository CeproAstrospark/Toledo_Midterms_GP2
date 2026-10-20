using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private Transform[] zomSpawn;
    [SerializeField] private Transform peaPos;
    [SerializeField] private float zomSpeed = 2f;
    [SerializeField] private float minTime = 3f;
    [SerializeField] private float maxTime = 5f;
    [SerializeField] private float stopDist = 0.1f;
    [SerializeField] private GameObject GameOver;

    private readonly List<GameObject> zomList = new List<GameObject>();
    private bool gameStopped;

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(SpawnZoms());
    }

    private void Update()
    {
        if (gameStopped || peaPos == null)
            return;

        for (int i = zomList.Count - 1; i >= 0; i--)
        {
            GameObject zom = zomList[i];

            if (zom == null)
            {
                zomList.RemoveAt(i);
                continue;
            }

            zom.transform.position = Vector3.MoveTowards(
                zom.transform.position, peaPos.position, zomSpeed * Time.deltaTime);

            if ((zom.transform.position - peaPos.position).sqrMagnitude <= stopDist * stopDist)
            {
                StopGame();
                return;
            }
        }
    }

    private void StopGame()
    {
        GameOver.SetActive(true);
        gameStopped = true;
        Time.timeScale = 0f;
    }

    private IEnumerator SpawnZoms()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            if (zombie == null || zomSpawn == null || zomSpawn.Length == 0)
                continue;

            int spawnIndex = Random.Range(0, zomSpawn.Length);

            GameObject newZom = Instantiate(zombie, zomSpawn[spawnIndex].position, Quaternion.identity);
            zomList.Add(newZom);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}