using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeek : MonoBehaviour
{
    public Transform player, side, eye;
    public bool isHide;
    Collider coll;
    Transform temp;
    // Start is called before the first frame update
    void Start()
    {
        isHide=false;
        coll=GetComponent<Collider>();
        temp = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,player.transform.position)<5&&Input.GetKeyDown(KeyCode.E)&&isHide)
        {
        coll.isTrigger=false;
         
            player.transform.localScale+=new Vector3(0,0.7f,0);
            player.position = side.position;
           Camera.main.transform.rotation = temp.rotation;
            isHide =false;
        }
    }
   
    public void Hide()
    {
        print("hide");
        coll.isTrigger = true;;
            player.transform.localScale -= new Vector3(0, 0.7f, 0);
        player.transform.position = eye.transform.position;
        player.transform.rotation = eye.transform.rotation;
        Camera.main.transform.rotation = eye.transform.rotation;
        isHide = true;
        }
}
