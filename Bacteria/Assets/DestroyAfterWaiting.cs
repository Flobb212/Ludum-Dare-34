using UnityEngine;
using System.Collections;

public class DestroyAfterWaiting : MonoBehaviour {

	void Start () {
        Destroy(this.gameObject, 5.0f);
	}
}
