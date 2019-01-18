using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class VampireControler : MonoBehaviour
{
    public Animator anim;
    AudioSource source;
    public Transform player, door, vampPoint, spotLight;
    public Transform eyes;
    public Transform bat;
    public Vector3 nextPoint;
    NavMeshAgent agent;
    bool isBat, isPlayer, isHide, isClose, isGo, startBat, startScreamer;
    public bool isDead;
    public bool patrul;
    float timer;
    public Transform[] markers;
    // public Camera cam;
    // Use this for initialization
    void Start()
    {
        startScreamer = false;
        startBat = false;
        isGo = false;
        isClose = false;
        isDead = false;
        isPlayer = false;
        source = GetComponent<AudioSource>();
        patrul = true;
        agent = GetComponent<NavMeshAgent>();
        timer = 0;
        nextPoint = markers[Random.Range(0, markers.Length-1)].position;
    }

    // Update is called once per frame
    void Update()
    {
      //  print("patrul " + patrul + " bat " + isBat + " player " + isPlayer+" startBAt "+startBat);
      //  print("distance " + Vector3.Distance(gameObject.transform.position, player.transform.position));
        //vamp point
        //------------------------------------------------------------------
        if (!patrul&& Vector3.Distance(gameObject.transform.position, markers[markers.Length - 1].position) < 1)
        {
            agent.SetDestination(vampPoint.position);
            //print(Vector3.Distance(gameObject.transform.position, vampPoint.position));
            isClose = true;
        }
        //-----------------------------------------------------------------
        if (Vector3.Distance(gameObject.transform.position, vampPoint.position) <= 1 && isClose)
        {

            isClose = false;
            print("vamp close this door");
            gameObject.transform.LookAt(door);
            door.GetComponent<Open>().OpenTheDoor();
            patrul = true;
        }
      //  if (Vector3.Distance(gameObject.transform.position, vampPoint.position) <= 1&&isClose)
      //  {
           
/*            if (door.GetComponent<Open>().closed)
            {
                isGo = true;
                print("where is MY key");
            }
            if (!isGo)
            {
                isClose = false;
                print("vamp close this door");
                gameObject.transform.LookAt(door);
                door.GetComponent<Open>().OpenTheDoor();

            }*/
          //      patrul = true;
      //  }
        isHide = player.GetComponent<PlayerControler>().isHide;
       /* if (Vector3.Distance(gameObject.transform.position,player.transform.position)<5&&!isHide)
        {
            isPlayer = true;
            eyes.transform.LookAt(player);
        }*/
        //patrul
        //----------------------------------------------------
        if (patrul && !isBat && !isPlayer)
        {
          //  print("patrul");
            if (Vector3.Distance(gameObject.transform.position, nextPoint) > 1)
            {
              //  print("go to "+nextPoint);
                agent.SetDestination(nextPoint);
            }
            else
            {
             //   print("new point");
                nextPoint = markers[Random.Range(0, markers.Length-1)].position;
              //  print(nextPoint);
            }
        }
        //--------------------------------------------------
        isBat = bat.GetComponent<BatControler>().isBat;                     // Bat scream
        //-----------------------------------------------------
        //Ray 
        //---------------------------------------------------
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            Ray ray = new Ray(eyes.position + new Vector3(0, 0.7f, 0), eyes.forward);
            Debug.DrawRay(eyes.position + new Vector3(0, 0.7f, 0), eyes.forward, Color.yellow, 1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                // print("I see " + hit.transform.tag+", patrul is"+patrul+" distance "+ Vector3.Distance(gameObject.transform.position, door.position)+" is open "+ door.GetComponent<Open>().isOpen);
                // print(hit.transform.name);
                //if (hit.transform.tag == "player" && !isDead)

                if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 7 && !isDead)
                {
                    patrul = false;
                    // nextPoint = gameObject.transform.position;

                    print("vamp kill noob player");
                    // gameObject.transform.LookAt(Camera.main.transform);
                    // gameObject.transform.LookAt(player);
                    gameObject.transform.position = Camera.main.transform.position - new Vector3(2, -1, 0);
                    gameObject.transform.LookAt(Camera.main.transform);
                    // anim.SetTrigger("attack");
                    // iTween.MoveTo(gameObject, iTween.Hash("position", player.transform, "looktarget", player.transform, "time", 2));
                    // agent.SetDestination(player.transform.position-new Vector3(0.5f,0,0.5f));
                    //  agent.SetDestination(player.transform.position);

                    agent.speed = 100;
                    startScreamer = true;

                    /* if (startScreamer)
                     {
                         gameObject.transform.LookAt(Camera.main.transform);
                     }
                     /*
                     if (startScreamer && Vector3.Distance(gameObject.transform.position, player.transform.position) < 3)
                         {*/
                    // Debug.LogError("attack");
                    anim.SetBool("Attack", true);
                    //  agent.Stop();
                    //   Vector3 look = eyes.transform.position;
                    // Vector3 look = gameObject.transform.position+Vector3.up*7;
                    //  Camera.main.transform.LookAt(look);
                    // Time.timeScale = 0;
                    //DelayVamp();
                    source.Play();
                    // DelayVamp();
                    isDead = true;
                    agent.Stop();
                }
            }
                else if (isDead)
                {
                    anim.SetBool("Attack", false);
                }
                else if (hit.transform.tag == "door" && Vector3.Distance(gameObject.transform.position,door.position)<3&&door.GetComponent<Open>().isOpen==false)
                {
                    hit.transform.GetComponent<Open>().isPlayer = false;
                    hit.transform.GetComponent<Open>().OpenTheDoor();
                    print("vamp open this door");
                    anim.SetTrigger("door");
                }
            if (startScreamer)
            {
                gameObject.transform.LookAt(Camera.main.transform);
                Camera.main.transform.eulerAngles+= new Vector3(-30, 0, 0);
               // player.transform.LookAt(eyes.transform);
            }

        }
        //----------------------------------------------------
       /* if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 5 || Mathf.Abs(transform.position.z - player.transform.position.z) <= 5)
        {
            transform.LookAt(player);

        }*/
         if (isBat)
         {
           // Debug.LogError("bat d");
            agent.SetDestination(bat.transform.position);
            startBat = true;
            //print("dist " + Vector3.Distance(gameObject.transform.position, bat.transform.position));
           //  iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(bat.transform.position.x,transform.position.y,bat.transform.position.z), "looktarget", player.position, "time", 10));
             // iTween.MoveTo(gameObject, iTween.Hash("position", bat.transform, "looktarget", bat.transform, "time", 10));
         }
        if (startBat&&Vector3.Distance(gameObject.transform.position,bat.transform.position)<=5.2f)
        {
          //  Debug.LogError("bat");
          //  print("ow");
            isBat = false;
            startBat = false;
            patrul = true;
            bat.GetComponent<BatControler>().isBat = false;
         
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
    IEnumerator DelayVamp()
    {
        yield return new WaitForSeconds(3);
    }
    public void PlayerWon()
    {
        gameObject.SetActive(false);
    }
    public void DoorIsOpen()
    {
        patrul = false;
//        agent.SetDestination(vampPoint.position);
        /* if (door.GetComponent<Open>().closed)
         {
             agent.SetDestination(vampPoint.position);
         }
         else*/
         agent.SetDestination(markers[markers.Length - 1].position);
       
        if (Vector3.Distance(gameObject.transform.position, markers[markers.Length - 1].position) < 1)
        {
            agent.SetDestination(vampPoint.position);
          //  print(Vector3.Distance(gameObject.transform.position, vampPoint.position));
            if (Vector3.Distance(gameObject.transform.position, vampPoint.position) <= 1)
            {
                print("vamp close this door");
                gameObject.transform.LookAt(door);
                door.GetComponent<Open>().OpenTheDoor();
                patrul = true;
            }
        }
    }
    }

