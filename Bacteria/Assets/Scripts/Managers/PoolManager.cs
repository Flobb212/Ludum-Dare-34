using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour {
	
	[System.Serializable]
	public class PooledObject {
		public string name;
		public GameObject objectPrefab;
		public int count;
	}

	[Space(10)]
	public List<PooledObject> objectPool = new List<PooledObject>();
	public static PoolManager current;
	
	private Hashtable mainPool = new Hashtable();
	private List<GameObject> tempList;

	void Awake() {
		current = this;
	}

	void Start() {
		tempList = new List<GameObject> ();

		for (int i = 0; i < objectPool.Count; i++) {
			List<GameObject> objList = new List<GameObject> ();

			for (int j = 0; j < objectPool[i].count; j++) {
				GameObject obj = (GameObject)Instantiate(objectPool[i].objectPrefab);
				obj.SetActive(false);
				objList.Add (obj);
			}

			mainPool.Add (objectPool[i].name, objList);
		}
	}

	public GameObject GetPooledObject(string name) {
		if (mainPool.ContainsKey (name)) {
			tempList = mainPool[name] as List<GameObject>;

			for (int i = 0; i < tempList.Count; i++) {
				if(tempList[i] != null) {
					if (!tempList[i].activeInHierarchy) {
						return tempList[i];
					}
				}
			}
		}
		return null;
	}


	public void ResetPool() {
		for (int i = 0; i < tempList.Count; i++) {
			tempList = mainPool[objectPool[i].name] as List<GameObject>;

			for (int j = 0; j < tempList.Count; j++) {
				if (tempList[j] != null) {
					if (tempList[j].activeInHierarchy) {
						tempList[j].SetActive(false);
					}
				}
			}
		}
	}
}
