using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour {
    public Transform player, vampire;
    Transform door;
    int playerPos;
    public bool isOpen, isPlayer, key;
    public AudioClip[] clip;
    public AudioSource source;
	// Use this for initialization
	void Start () {
        isOpen = false;
        isPlayer = true;
	}
	
	// Update is called once per frame
	void Update () {
   
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
               
                if (isPlayer)
                    playerPos = transform.position.x - player.position.x > 0 ? 1 : -1;
                else playerPos = transform.position.x - vampire.position.x > 0 ? 1 : -1;
                iTween.RotateTo(door.gameObject, iTween.Hash("rotation", new Vector3(0, -90, 0), "time", 5, "isLocal", true));
                source.PlayOneShot(clip[0]);
                if (vampire.gameObject.activeInHierarchy)
                vampire.GetComponent<VampireControler>().DoorIsOpen();
            }

            if (isOpen)
            {
                iTween.RotateTo(door.gameObject, iTween.Hash("rotation", new Vector3(0, 0, 0), "time", 5, "isLocal", true));
                source.PlayOneShot(clip[1]);
            }

            isOpen = !isOpen;
            print("door is open " + isOpen);
        }
    }
   


}

