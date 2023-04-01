using UnityEngine;

public class Spawner : MonoBehaviour
{
    // System.Serializable enables Unity to store and reload data about the struct locally 
    [System.Serializable]
    // Basically a template for any spawnable object
    public struct SpawnableObject
    {
        // Prefab of the object to be spawned
        public GameObject prefab;
        
        // Probability of the object to be spawned
        [Range(0f, 1f)]
        public float spawnChance;
    }

    // Array tracking all the objects that have been spawned
    public SpawnableObject[] objects;

    // Bounds on the rate at which objects are to be spawned
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    // Called when the GameObject becomes active
    private void OnEnable()
    {
        // Random.Range randomly (uniformly) chooses a spawn rate in the given range
        // The Spawn() method is invoked after that many seconds
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    // Called when the GameObject becomes inactive 
    private void OnDisable()
    {
        // Cancel all pending invokes
        CancelInvoke();
    }

    // Custom method to spawn an obstacle
    private void Spawn()
    {
        // Choose a random float in [0, 1]
        float spawnChance = Random.value;

        // Loop over the array of predefined objects
        foreach (var obj in objects)
        {
            // The objects are arranged in decreasing order of their spawn probabilities
            // Ensure that the object with higher spawnChance is more likely to spawn
            if (spawnChance < obj.spawnChance)
            {
                // Instantiate a prefab of the chosen object
                GameObject obstacle = Instantiate(obj.prefab);

                // The obstacle is spawned at the position of its prefab
                // Shift it by the position of the spawner to maintain some initial distance between the player and the obstacle
                obstacle.transform.position += transform.position;

                // Since an obstacle has been spawned, break the loop
                break;
            }
            // Decrement spawnChance to check for the next object
            spawnChance -= obj.spawnChance;
        }
        // Invoke the Spawn() method again to spawn the next object
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

}
