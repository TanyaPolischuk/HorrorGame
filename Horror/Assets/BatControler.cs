using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatControler : MonoBehaviour
{
    public Transform player, marker1, marker2;
    public bool isBat;
    public Vector3 nextPoint;
    public AudioSource source;
    public AudioClip clip;
    float timer;
    Renderer m_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        timer = 0;
        nextPoint = new Vector3(Random.Range(marker1.transform.position.x, marker2.transform.position.x),
                                        0, Random.Range(marker1.transform.position.z, marker2.transform.position.z));
        iTween.MoveTo(gameObject, iTween.Hash("position", nextPoint, "speed", 100 * Time.deltaTime, "orienttopath", true));
        isBat = false;
    }
    private void Update()
    {
        source.Play();
        if (!isBat)
        {
            if (gameObject.transform.position == nextPoint)
            {
                nextPoint = new Vector3(Random.Range(marker1.transform.position.x, marker2.transform.position.x),
                                            0, Random.Range(marker1.transform.position.z, marker2.transform.position.z));
                iTween.MoveTo(gameObject, iTween.Hash("position", nextPoint, "speed", 100 * Time.deltaTime, "looktarget", nextPoint, "delay", 10));
            }
        }
        if (m_Renderer.isVisible&&Vector3.Distance(gameObject.transform.position,player.transform.position)<7)
        {
            timer += Time.deltaTime;
           Debug.Log("visible");
           // isBat = true;
        }
        else
        {
            timer = 0;
          //  isBat = false;
           Debug.Log("no visible");
        }
        if (timer>2)
        {
            gameObject.transform.position = Camera.main.transform.position;
            print("arrrrrrr");
            timer = 0;
        }
    }
}  

