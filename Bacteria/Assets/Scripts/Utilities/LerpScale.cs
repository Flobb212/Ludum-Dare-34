using UnityEngine;
using System.Collections;

public class LerpScale : MonoBehaviour {

    private Vector3 startScale;
    private Vector3 endScale;
    public float speed = 0.8f;
    private bool doLerp = false;
    private Transform m_transform;
    private float lerpVal = 0.0f;

    // Use this for initialization
    void Start()
    {
        m_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (doLerp)
        {
            if (lerpVal < 1)
            {
                lerpVal += Time.deltaTime * speed;
                m_transform.localScale = Vector3.Lerp(startScale, endScale, lerpVal);
            }
            else
            {
                Destroy(this);
            }
        }
    }

    public void SetupLerp(Vector3 startScale, Vector3 endScale)
    {
        this.endScale = endScale;
        this.startScale = startScale;
        lerpVal = 0.0f;
    }

    public void DoLerp()
    {
        doLerp = true;
    }

    public void StopLerp()
    {
        doLerp = false;
    }
}
