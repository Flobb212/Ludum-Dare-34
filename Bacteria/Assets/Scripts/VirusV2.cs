using UnityEngine;
using System.Collections;

public class VirusV2 : GameEntity {


    Player playerScript;
    GameObject playerTarget = null;
	Vector3 targetLocation;
	Vector3 directionToTarget;
	ImmunityColour selfColor;
	float distanceError = 0.5f;
	bool seeking = false;
    public ParticleSystem explosion;
    

	void Start()
    {
		try{
			playerScript = GameObject.Find("Player").GetComponent<Player>();
		}
		catch{
        //Do Nothing
		}        m_transform = transform;
		Initalise ();

        explosion = GetComponentInChildren<ParticleSystem>();
	}

    void OnEnable()
    {
        Initalise();
    }

	public override void Initalise ()
	{
        m_transform = transform;
		playerScript = GameObject.Find("Player").GetComponent<Player>();
		currentColor = startColor; //This sometimes causes null ref excepts
		currentMass = playerScript.GetCurrentMass();
		m_transform.localScale = new Vector3 (currentMass / 12f, currentMass / 12f, 1f);
		speed = 25f / currentMass;

		int colorChoice = Random.Range (1, 4);

		switch (colorChoice) 
		{
		case(1):
			selfColor = ImmunityColour.BLUE;
            currentColor = Color.blue;
			break;
		case(2):
			selfColor = ImmunityColour.GREEN;
            currentColor = Color.green;
			break;
		case(3):
			selfColor = ImmunityColour.RED;
            currentColor = Color.red;
			break;
		};

        ApplyCurrentColor();

		FindNewHeading();
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.CompareTag ("Player")) {
			playerTarget = col.gameObject;
		}
	}

    void OnColliderEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bacteria"))
        {
              col.gameObject.SetActive(false);
        }

        if (col.gameObject.CompareTag("Wall"))
        {
            FindNewHeading();
        }

        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().Die();
            Die();
        }
    }

	void OnTriggerExit2D (Collider2D col) 
	{
		if (col.CompareTag ("Player")) {
			playerTarget = null;
		}
	}

	void Update() {

		if (playerTarget != null)
			Seek ();
		else
		{
			seeking = false;
			Wander ();
		}

        if(currentMass < (playerScript.GetCurrentMass() * 0.35f))
        {
            gameObject.SetActive(false);
        }
	}

	void Seek() 
	{
		seeking = true;

		Vector2 steering = playerTarget.transform.position - m_transform.position;

		if (steering != Vector2.zero)
			steering.Normalize ();

		m_transform.position += new Vector3(steering.x, steering.y, 0.0f )* speed * Time.deltaTime;
	}

	void Wander() 
	{
		float distToTarget = Vector2.Distance (m_transform.position, targetLocation);

		if (!seeking) {
			if (distToTarget <= distanceError) {
				FindNewHeading ();
			}
		}
	}

	void FindNewHeading() {
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		
		targetLocation = GameManager.GetRandomPosition ();
		
		
		directionToTarget = targetLocation - m_transform.position;

		gameObject.GetComponent<Rigidbody2D> ().AddForce (directionToTarget.normalized * speed, ForceMode2D.Impulse);
	}

	public ImmunityColour GetColor()
	{
		return selfColor;
	}

    public override void Die()
    {
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);        
        
        base.Die();
    }
}
