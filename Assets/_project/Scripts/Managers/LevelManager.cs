using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public Enemy enemyPrefab;
    public GameObject wallPrefab;

    public GameObject currentLevel;

    public float mapXBorder;

    public void CreateNewLevel()
    {
        currentLevel = new GameObject("Map");

        var entityCount = Random.Range(2,5);

        for (int i = 0; i < entityCount; i++)
        {
            if (Random.value < .5f)
            {
                var newWall = Instantiate(wallPrefab, currentLevel.transform);
                newWall.transform.position = new Vector3(Random.Range(-mapXBorder, mapXBorder), 0, 2 + i * 1.1f);
            }
            else
            {
                var newEnemy = Instantiate(enemyPrefab, currentLevel.transform);
                newEnemy.transform.position = new Vector3(Random.Range(-mapXBorder, mapXBorder), 0, 2 + i * 1.1f);
                newEnemy.StartEnemy(player);
            }
                
        }        
    }

    public void DeleteCurrentLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
    }
}
