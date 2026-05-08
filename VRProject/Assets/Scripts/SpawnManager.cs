using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoint = new Transform[6]; // 스폰 위치 배열
    public GameObject[] enemies; // 여러 종류의 적들
    float spawnTime;

    void Start()
    {
        spawnTime = 10.0f;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (spawnTime > 0)
        {
            int pointIndex = Random.Range(0, spawnPoint.Length); // 스폰 위치 랜덤
            int enemyIndex = Random.Range(0, enemies.Length);    // 적 종류 랜덤

            Instantiate(enemies[enemyIndex], spawnPoint[pointIndex].position, spawnPoint[pointIndex].rotation);

            yield return new WaitForSeconds(spawnTime);
            spawnTime -= 0.01f; // 점점 더 빨리 소환
        }

        yield return null;
    }
}

