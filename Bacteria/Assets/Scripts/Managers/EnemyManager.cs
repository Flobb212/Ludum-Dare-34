using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public string[] pooledObjectsToSpawn;

    public float initialSpawnTime = 0.5f;
	public float spawnTime = 4f;
	public int noToSpawn = 10;

    public bool initialSpawn = true;


	// Use this for initialization
	void Start () {
        if (initialSpawn)
            Invoke("SpawnWave", initialSpawnTime);
        
		InvokeRepeating ("SpawnWave", spawnTime, spawnTime);
	}

	void SpawnWave() {
		for (int loop = 0; loop < noToSpawn; loop++) {
			float randXPos = Random.Range (1, GameManager.worldSizeX - 1f);
			float randYPos = Random.Range (1, GameManager.worldSizeY - 1f);
			Vector3 tempVector = new Vector3(randXPos, randYPos, 0f);

			if (!CheckIfPointIsOffScreen(tempVector)) {
				randXPos = Random.Range (1, GameManager.worldSizeX - 1f);
				randYPos = Random.Range (1, GameManager.worldSizeY - 1f);


			}

			if (!Physics.CheckSphere(tempVector, 0.5f)) {
				int indToSpawn = Random.Range(0, pooledObjectsToSpawn.Length);
				string objToSpawn = pooledObjectsToSpawn[indToSpawn];

				if (objToSpawn != "") 
				{
					GameObject temp = PoolManager.current.GetPooledObject(objToSpawn);
							
					if (temp != null)
					{
						temp.transform.position = new Vector3(randXPos, randYPos, 0f);
						temp.transform.rotation = transform.rotation;
						temp.SetActive(true);
					}
				}
			}
		}
	}


	bool CheckIfPointIsOffScreen(Vector3 vect) {
		bool result = false;

		Vector3 thingOffset = Camera.main.WorldToScreenPoint (vect);

		if (thingOffset.x > Camera.main.pixelWidth && thingOffset.y > Camera.main.pixelHeight) {
			result = true;
		} else if (thingOffset.x < 0 && thingOffset.y < 0) {
			result = true;
		} else {
			return false;
		}

		return result;
	}
}
