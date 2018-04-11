using UnityEngine;
using System.Collections;

public class GameEntity : MonoBehaviour {

    public float startMass;
    public Color startColor = Color.red;
    public float speed;
    protected ImmunityColour imColor;
    public float currentMass;
    protected Color currentColor;
	protected Transform m_transform;

    void Awake()
    {
        currentMass = startMass;
    }

	protected virtual void Start () {
		m_transform = transform;        
		SetScale ();
        currentColor = startColor;
	}

    public virtual void Initalise()
    {
        speed = 18f / currentMass;
    }

    protected void LerpScale()
    {
        LerpScale scaleLerp;

        if (gameObject.GetComponent<LerpScale>() != null)
        {
            scaleLerp = gameObject.GetComponent<LerpScale>();
            scaleLerp.StopLerp();
        }
        else
        {
            scaleLerp = gameObject.AddComponent<LerpScale>();
        }

        scaleLerp.SetupLerp(m_transform.localScale, new Vector3(currentMass / 12f, currentMass / 12f, 1f));
		speed = 22 / currentMass;
        scaleLerp.DoLerp();
    }

	void SetScale() {
		m_transform.localScale = new Vector3 (currentMass /12.0f, currentMass / 12.0f, 1f);
        if (m_transform.localScale.x < 1 || m_transform.localScale.y < 1)
        {
            m_transform.localScale = new Vector3(1, 1, 1);    
        }
	}
	
	protected void ChangeColor(Color addedColor, Bacteria eatenBacteria)
	{
		currentColor = ColorCalculator.MergeColors(currentColor, currentMass, addedColor, eatenBacteria.GetCurrentMass());
        ApplyCurrentColor();
	}

    
	
	protected virtual float CheckEating(Collider2D col)
	{
		if (col.CompareTag("Bacteria"))
		{
			Bacteria bacteriaInfo = col.GetComponent<Bacteria>();
			if (bacteriaInfo.GetCurrentMass() < currentMass) 
			{

				ChangeColor(bacteriaInfo.CurrentColor, bacteriaInfo);

				bacteriaInfo.Die();


				if (currentMass < 100)
					currentMass += bacteriaInfo.GetCurrentMass() * 0.2f;
				else
					currentMass += bacteriaInfo.GetCurrentMass() * 0.05f;

				LerpScale();

			}
			else if (bacteriaInfo.GetCurrentMass() > currentMass)
			{
                if (this.GetComponent<Player>() != null)
                {
                    this.GetComponent<Player>().Die();
                }
                else
                {
                    this.Die();
                }
			}

			else if (bacteriaInfo.GetCurrentMass() == currentMass)
			{
				//Do nothing
			}
			return bacteriaInfo.GetCurrentMass();
		}
		else if(col.CompareTag("Player"))
		{
			Player player = col.GetComponent<Player>();

			if(this.currentMass > player.GetCurrentMass())
			{
				ScoreManager.won = false;
				player.Die();
			}
		}

		return -1.0f;
	}

    public virtual void Die()
    {
        currentMass = startMass;
        currentColor = startColor;
		gameObject.SetActive (false);
    }

	public virtual float GetCurrentMass()
	{
		return currentMass;
	}

    protected void ApplyCurrentColor()
    {
        GetComponent<SpriteRenderer>().color = currentColor;
    }

}
