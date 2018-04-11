using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	public float pickupDuration;

	public PickUpType type;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
            Player thePlayer = col.gameObject.GetComponent<Player>();
            thePlayer.ActivateEffect(type, pickupDuration);
			Destroy(gameObject);
		}
	}
}
