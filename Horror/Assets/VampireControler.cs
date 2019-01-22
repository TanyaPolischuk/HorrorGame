using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class VampireControler : MonoBehaviour
{
    public Animator anim;
    public AudioClip[] clips;
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
        source.PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
        agent = GetComponent<NavMeshAgent>();
        timer = 0;
        nextPoint = markers[Random.Range(0, markers.Length - 1)].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying && !startScreamer)
        {
            source.PlayOneShot(clips[Random.Range(0, clips.Length - 2)]);
        }
        //vamp point
        //------------------------------------------------------------------
        if (!patrul && Vector3.Distance(gameObject.transform.position, markers[markers.Length - 1].position) < 1)
        {
            agent.SetDestination(vampPoint.position);
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
        isHide = player.GetComponent<PlayerControler>().isHide;
        //patrul
        //----------------------------------------------------
        if (patrul && !isBat && !isPlayer)
        {
            if (Vector3.Distance(gameObject.transform.position, nextPoint) > 1)
            {
                agent.SetDestination(nextPoint);
            }
            else
            {
                nextPoint = markers[Random.Range(0, markers.Length - 1)].position;
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
            eyes.LookAt(player.position);
            Ray ray = new Ray(eyes.position + new Vector3(0, 0.7f, 0), eyes.forward);
            Debug.DrawRay(eyes.position + new Vector3(0, 0.7f, 0), eyes.forward, Color.yellow, 1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                if (hit.transform.tag == "player" && Vector3.Distance(gameObject.transform.position, player.transform.position) < 10 && !isDead && !isHide)
                {
                    patrul = false;
                    print("vamp kill noob player");
                    gameObject.transform.position = Camera.main.transform.position - new Vector3(2, -1, 0);
                    gameObject.transform.LookAt(Camera.main.transform);
                    agent.speed = 100;
                    startScreamer = true;
                    anim.SetBool("Attack", true);
                    source.PlayOneShot(clips[clips.Length - 1]);
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
            if (hit.transform.tag == "door" && Vector3.Distance(gameObject.transform.position, door.position) < 3 && door.GetComponent<Open>().isOpen == false)
            {
                hit.transform.GetComponent<Open>().isPlayer = false;
                hit.transform.GetComponent<Open>().OpenTheDoor();
                print("vamp open this door");
                anim.SetTrigger("door");
            }
            if (startScreamer)
            {
                gameObject.transform.LookAt(Camera.main.transform);
                Camera.main.transform.eulerAngles += new Vector3(-30, 0, 0);

            }
            //----------------------------------------------------

        /*    if (isBat)
            {

                agent.SetDestination(bat.transform.position);
                startBat = true;

            }
            if (startBat && Vector3.Distance(gameObject.transform.position, bat.transform.position) <= 5.2f)
            {

                isBat = false;
                startBat = false;
                patrul = true;
                bat.GetComponent<BatControler>().isBat = false;

            }*/
        }
    }

        


            public void PlayerWon()
            {

                gameObject.SetActive(false);
            }
            public void DoorIsOpen()
            {
                patrul = false;

                if (gameObject.activeInHierarchy)
                    agent.SetDestination(markers[markers.Length - 1].position);

                if (Vector3.Distance(gameObject.transform.position, markers[markers.Length - 1].position) < 1)
                {
                    agent.SetDestination(vampPoint.position);
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

    