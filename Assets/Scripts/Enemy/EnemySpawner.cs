using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    private void Start()
    {

        
        for (int i = 0; i < 150; i++)
        {
            int rndX = Random.Range(-10, 10);
            int rndZ = Random.Range(-10, 10);
            int rndY = Random.Range(-10, 10);
            GameObject enemyObject = Instantiate(enemyPrefab, player.position + new Vector3(rndX, rndY, rndZ), Quaternion.identity);
            //enemyObject.GetComponent<Enemy>().Target = player;
        }
    }


}
