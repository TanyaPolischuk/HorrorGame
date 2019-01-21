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
           // Camera.main.transform.position = temp.position;
           Camera.main.transform.rotation = temp.rotation;
            isHide =false;
        //print("hide");
        }
    /*  else if (Vector3.Distance(transform.position,player.transform.position)<5&&Input.GetKeyDown(KeyCode.E)&&isHide)
        {
        coll.isTrigger = false;
           
        player.transform.localScale+=new Vector3(0,0.7f,0);
            player.position = temp.position;
          //  player.position=gameObject.transform.position+Vector3.one;
        isHide=false;
        //print("hide"); 
        }*/
    }
   
    public void Hide()
    {
        // (Vector3.Distance(transform.position, player.transform.position) < 5 && Input.GetKeyDown(KeyCode.E) && isHide)
        // {
        print("hide");
        coll.isTrigger = true;;
        //temp = player.transform;
            player.transform.localScale -= new Vector3(0, 0.7f, 0);

        //player.position = gameObject.transform.position+new Vector3(0,1.4f,0);
        //  temp = Camera.main.transform;
        player.transform.position = eye.transform.position;
        player.transform.rotation = eye.transform.rotation;
        //Camera.main.transform.position = eye.transform.position;
        Camera.main.transform.rotation = eye.transform.rotation;

        // player.position = temp.position;
        //  player.position=gameObject.transform.position+Vector3.one;
        isHide = true;
        }
}
