using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatControler : MonoBehaviour
{
    public Transform player;
    public bool isBat;
    // Start is called before the first frame update
    void Start()
    {
        isBat = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.GetComponent<PlayerControler>().isLight && isBat == false)
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

