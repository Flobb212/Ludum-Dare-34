using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BacteriaUI : MonoBehaviour {

	public Canvas uiCanvas;
	public float uiOffset = -0.65f;
	public GameObject enemyUIPrefab;
	public Sprite warningImage;
	public Sprite cautionImage;
    public Sprite canEatImage;

	public Image uiImage;

	public GameObject uiObject;

	Bacteria bacteriaScript;
    


 	void Start() {
		bacteriaScript = GetComponent<Bacteria> ();
		uiCanvas = (Canvas)FindObjectOfType (typeof(Canvas));
		
		uiObject = Instantiate (enemyUIPrefab) as GameObject;
		uiObject.transform.SetParent (uiCanvas.transform);
        uiImage = uiObject.GetComponentInChildren<Image>();
	}

	void Update() {
		UpdatePosition ();
	}

	void UpdatePosition() {
		Vector3 worldPos = new Vector2 (transform.position.x, transform.position.y + (uiOffset * bacteriaScript.gameObject.transform.localScale.x));
		Vector3 screenPos = Camera.main.WorldToScreenPoint (worldPos);
		uiObject.transform.position = new Vector3 (screenPos.x, screenPos.y, screenPos.z);
	}

    public void SwitchToWarning()
    {
        if (uiImage.sprite != warningImage)
            uiImage.sprite = warningImage;
    }

    public void SwitchToCaution()
    {
        if (uiImage.sprite != cautionImage)
            uiImage.sprite = cautionImage;
    }

    public void SwitchToNull()
    {
        if (uiImage.sprite != canEatImage)
            uiImage.sprite = canEatImage;
    }
}
