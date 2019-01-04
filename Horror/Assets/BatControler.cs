using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatControler : MonoBehaviour
{
    public Transform player, marker1, marker2;
    public bool isBat;
    public Vector3 nextPoint;
    // Start is called before the first frame update
    void Start()
    {
        nextPoint = new Vector3(Random.Range(marker1.transform.position.x, marker2.transform.position.x),
                                        0, Random.Range(marker1.transform.position.z, marker2.transform.position.z));
        iTween.MoveTo(gameObject, iTween.Hash("position", nextPoint, "speed", 100*Time.deltaTime, "orienttopath",true));

        isBat = false;
    }
    private void Update()
    {
        // gameObject.transform.eulerAngles = new Vector3(-90, 0, 0);
        if (!isBat)
        {
            if (gameObject.transform.position == nextPoint)
            {
                nextPoint = new Vector3(Random.Range(marker1.transform.position.x, marker2.transform.position.x),
                                            0, Random.Range(marker1.transform.position.z, marker2.transform.position.z));
                iTween.MoveTo(gameObject, iTween.Hash("position", nextPoint, "speed", 100 * Time.deltaTime, "looktarget", nextPoint, "delay", 10));
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.GetComponent<PlayerControler>().isLight && !isBat)
        {
            OnBecameVisible();
        }
    }

    void OnBecameVisible()
    {
        if  (player.GetComponent<PlayerControler>().isLight)
          {
            isBat = true;
            print("arrrr");
        }
    }
}

