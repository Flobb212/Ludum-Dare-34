using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bacteria : GameEntity {

    Player playerScript;
	Rigidbody2D m_rigidbody;
	BacteriaUI bactUI;
    ImprovedBacteriaAI ai;
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();

		m_rigidbody = GetComponent<Rigidbody2D> ();
		                              
		bactUI = GetComponent<BacteriaUI> ();
        ai = GetComponent<ImprovedBacteriaAI>();

	}

	void OnEnable() {

		try{
		playerScript = GameObject.Find("Player").GetComponent<Player>();
		}
		catch{
		}

		float playersMass = playerScript.GetCurrentMass ();
		currentMass = (int)Random.Range (playersMass * 0.5f, playersMass * 1.2f);

		Initalise ();
	}

	void OnCollisionEnter2D(Collision2D col){
		CheckEating (col.collider);

        if (col.gameObject.CompareTag("Wall"))
        {
            ai.FindNewHeading();
        }
	}

	public void Initalise ()
	{        
	
		StartCoroutine("CheckForUIElement");
        StartCoroutine("CheckForUIObject");

        currentColor = new Color(Random.Range(0.0f, 1.0f),Random.Range(0.0f, 1.0f), Random.Range(0.0f,1.0f));       
        ApplyCurrentColor();

        transform.localScale = new Vector3(currentMass / 12f, currentMass / 12f, 1f);
		new Vector3 (currentMass / 12f, currentMass / 12f, 1f);
		
		
		if (bactUI != null && bactUI.uiObject != null)
		{
            StartCoroutine("SpawnUI");
		}

	}

	public override void Die ()
	{
		base.Die ();
	}
	
    void Update()
    {
        float playersMass = playerScript.GetCurrentMass();

        if (currentMass < (playerScript.GetCurrentMass () * 0.35f)) {
            Die();
		}


        if (currentMass > playersMass)
        {
            bactUI.SwitchToWarning();
        }
        
		if (currentMass == playersMass)
        {
            bactUI.SwitchToCaution();
        }
		
        if (currentMass < playersMass)
        {
            bactUI.SwitchToNull();
        }

    }

    public Color CurrentColor
    {
        get
        {
            return currentColor;
        }
    }

	public override float GetCurrentMass ()
	{
		return currentMass;
	}

    void OnDisable()
    {
        if (bactUI.uiObject != null)
        {
            bactUI.uiObject.SetActive(false);
        }
    }
	
	IEnumerator CheckForUIElement() {
		
		while (bactUI == null)
		{
			bactUI = GetComponent<BacteriaUI>();
			
			yield return null;
		}
	}
	
	IEnumerator CheckForUIObject() {
		while (bactUI.uiObject == null) 
		{
 			yield return null;
		}
	}

    IEnumerator SpawnUI()
    {
        if (bactUI.uiObject.active == false) {

            yield return new WaitForSeconds(1f);

            bactUI.uiObject.SetActive(true);
        }
    }
}
