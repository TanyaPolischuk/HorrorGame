﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class VampireControler : MonoBehaviour
{
    public Transform player;
    public Transform eyes;
    public Transform bat;
    NavMeshAgent agent;
    bool isBat;
    float timer;
    // public Camera cam;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        isBat = bat.GetComponent<BatControler>().isBat;
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            Ray ray = new Ray(eyes.position + new Vector3(0, 0.7f, 0), eyes.forward);
            Debug.DrawRay(eyes.position + new Vector3(0, 0.7f, 0), eyes.forward, Color.yellow, 1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                print(hit.transform.name);
                if (hit.transform.tag == "player")
                {
                    iTween.MoveTo(gameObject, iTween.Hash("position", player.transform, "looktarget", player.transform, "time", 1));
                }
            }

        }
        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 5 || Mathf.Abs(transform.position.z - player.transform.position.z) <= 5)
        {
            transform.LookAt(player);
        }
         if (isBat)
         {
             print("I hear you");
            agent.SetDestination(bat.transform.position);
           //  iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(bat.transform.position.x,transform.position.y,bat.transform.position.z), "looktarget", player.position, "time", 10));
             // iTween.MoveTo(gameObject, iTween.Hash("position", bat.transform, "looktarget", bat.transform, "time", 10));
         }
     }
        /* private void OnBecameVisible()
         {
             iTween.MoveTo(gameObject, iTween.Hash("position", player.transform, "looktarget", player.transform, "time", 1));
         }*/
        /*public void Attack()
          {
              if (isBat)
              {
                  print("I hear you");
                  iTween.MoveTo(gameObject, iTween.Hash("position", bat.transform, "looktarget", bat.transform, "time", 10));
              }
          }*/

    }
//}
