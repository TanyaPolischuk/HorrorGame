using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeek : MonoBehaviour
{
    public Transform player;
    public bool isHide;
    Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        isHide=false;
        coll=GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,player.transform.position)<5&&Input.GetKeyDown(KeyCode.E)&&isHide==false)
        {
        coll.isTrigger=true;
        player.transform.localScale-=new Vector3(0,0.7f,0);
        player.position=gameObject.transform.position;
        isHide=true;
        //print("hide");
        }
      else if (Vector3.Distance(transform.position,player.transform.position)<5&&Input.GetKeyDown(KeyCode.E)&&isHide)
        {
        coll.isTrigger=false;
        player.transform.localScale+=new Vector3(0,0.7f,0);
        player.position=gameObject.transform.position+Vector3.one;
        isHide=false;
        //print("hide"); 
        }
    }
}
