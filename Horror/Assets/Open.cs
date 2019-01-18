using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour {
    public Transform player, vampire;
    Transform door;
    int playerPos;
    public bool isOpen, isPlayer, key;
   // public bool closed;
    public AudioClip[] clip;
    public AudioSource source;
	// Use this for initialization
	void Start () {
//closed = false;
       // door = GetComponentInChildren<Transform>();
        isOpen = false;
        isPlayer = true;
        //clip = player.GetComponent<AudioClip[]>();
	}
	
	// Update is called once per frame
	void Update () {
        
      /*  if (Input.GetKeyDown(KeyCode.E))
        {
          
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f,Screen.height/2f));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50))
            {
                Transform obj = hit.transform;
                if (obj.gameObject.tag == "door")
                {
                    // print("playerPos " + playerPos);
                    if (!isOpen)
                    {
                        // print("open the door");
                        playerPos = transform.position.x - player.position.x > 0 ? 1 : -1;
                      iTween.RotateTo(door.gameObject, iTween.Hash("rotation", new Vector3(0, playerPos * 90, 0), "time", 5, "isLocal", true));

                    }
                    if (isOpen)
                    {
                       // print("close the door");
                        iTween.RotateTo(door.gameObject, iTween.Hash("rotation", new Vector3(0, 0, 0), "time", 5, "isLocal", true));
                    }
                    isOpen = !isOpen;
                    print("door is open " + isOpen);
                }
            }

        }
       */
    }
    public void OpenTheDoor()
    {
        door = transform.parent;
        print(gameObject.name);
        print(gameObject.transform.rotation.y);
        if (isPlayer && player.GetComponent<PlayerControler>().key || !isPlayer)
        {
            print("its player"+isPlayer);
            if (!isOpen)
            {
                // print("open the door");
                // playerPos =(int) Mathf.Sign(gameObject.transform.position.y);
                if (isPlayer)
                    playerPos = transform.position.x - player.position.x > 0 ? 1 : -1;
                else playerPos = transform.position.x - vampire.position.x > 0 ? 1 : -1;
                iTween.RotateTo(door.gameObject, iTween.Hash("rotation", new Vector3(0, playerPos * 90, 0), "time", 5, "isLocal", true));
                source.PlayOneShot(clip[0]);
                vampire.GetComponent<VampireControler>().DoorIsOpen();
                // iTween.RotateTo(door.gameObject, iTween.Hash("rotation", new Vector3(0, playerPos * 90, 0), "time", 5, "isLocal", true));
            }

            if (isOpen)
            {
                // print("close the door");
                iTween.RotateTo(door.gameObject, iTween.Hash("rotation", new Vector3(0, 0, 0), "time", 5, "isLocal", true));
                source.PlayOneShot(clip[1]);
               // closed |= isPlayer;
            }

            isOpen = !isOpen;
            print("door is open " + isOpen);
        }
    }
   


}

