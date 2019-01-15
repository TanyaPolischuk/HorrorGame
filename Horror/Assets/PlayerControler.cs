using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public Transform vampire, GameBox;
    public Image image;
    public Text text;
    public Transform[] sphere;
    public float speed = 5;
    public Transform scroll;
    public bool isHide;
    // public Rigidbody bullet;
    //public Transform gun;
    Rigidbody rb;
    Transform cam;
    public GameObject spotLight;
    public bool isLight, key;
    Vector3 rotCam;
    bool isOpen, isDead;
    public AudioClip[] clip;
    //public AudioClip clip;
    AudioSource source;
    public Ray ray;
    public AudioSource sourceFon, door;
    float move;
    void Start()
    {
        key = false;
        isDead = vampire.GetComponent<VampireControler>().isDead;
        print(isDead);
        source = GetComponent<AudioSource>();
        text.text = "";
        List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        int rand;
        for (int i = 1; i <= 7; i++)
        {
            rand = list[Random.Range(0, list.Count - 1)];
            print(rand);
            text.text += rand;
            list.Remove(rand);
        }
        isHide = false;
        //isHide = Chest.GetComponent<HideAndSeek>().isHide;
        isLight = false;
        isOpen = false;
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        isDead = vampire.GetComponent<VampireControler>().isDead;

       // isHide = Chest.GetComponent<HideAndSeek>().isHide;
        if (isHide==false && !isDead)
         {
        rb.velocity = transform.TransformDirection(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);
            if (rb.velocity.x != 0 && !source.isPlaying)
            {
                if (Physics.Raycast(transform.position - new Vector3(0, 1.65f, 0), Vector3.down, 0.2f))
                {

                    source.clip = clip[1];

                    source.Play();
                }
            }
            transform.Rotate(0, Input.GetAxis("Mouse X") * 2f, 0, Space.World);
        rotCam = cam.localEulerAngles;
        cam.Rotate(-Input.GetAxis("Mouse Y") * 2f, 0, 0, Space.Self);
          
        //spotLight.transform.Rotate(0, Input.GetAxis("Mouse X"), 0, Space.World);

        //spotLight.transform.TransformDirection(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);
        if ((cam.localEulerAngles.x > 310 && cam.localEulerAngles.x < 360) || (cam.localEulerAngles.x > 0 && cam.localEulerAngles.x < 50))
        {
        }
        else
        {
            cam.localEulerAngles = rotCam;
        }
         
         }
         
    }


    void Update()
    {
        //print("* "+ Physics.Raycast(transform.position - new Vector3(0, 0.95f, 0), Vector3.down, 0.2f));
        //if(Input.GetKeyDown(KeyCode.Space)&& Physics.OverlapSphere(transform.position-new Vector3(0,1.2f,0),0.2f).Length>0)
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position - new Vector3(0, 1.65f, 0), Vector3.down, 0.2f))
        {
            // print("add");
            rb.AddForce(0, 300, 0);
            source.PlayOneShot(clip[0]);
        }

        /* if (Input.GetMouseButtonDown(0))
         {
             Rigidbody temp = Instantiate(bullet, gun.position, Quaternion.identity);
             temp.AddForce(gun.TransformDirection(new Vector3(0, 0, 1000)));
         }*/
        if (Input.GetKeyDown(KeyCode.G))
        {
            string s="";
            for (int i = 0; i <= 6; i++)
            {
                s+= sphere[i].GetComponent<MeshRenderer>().material.name[0];
                /*     sphere[i].name = sphere[i].GetComponent<MeshRenderer>().material.name;
                 }
                     for (int i = 0; i <= 6;i++)
                 {
                     s += sphere[i].name;*/
                print(s);
            }
            print(text.text);
            if (s==text.text)
            {
                print("win");
                vampire.GetComponent<VampireControler>().PlayerWon();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isLight = !isLight;
            spotLight.SetActive(isLight);
            // print("Light is"+isLight);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            if (image.IsActive())
            {
                image.gameObject.SetActive(false);
                text.gameObject.SetActive(false);
            }
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
            // Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50))
            {
                print(hit.transform.gameObject.tag);
                if (hit.transform.tag == "door")
                {
                    hit.transform.GetComponent<Open>().isPlayer = true;
                    print("door");
                    hit.transform.GetComponent<Open>().OpenTheDoor();
                }
                else if (hit.transform.tag == "scroll" && !image.IsActive())
                {
                    image.gameObject.SetActive(true);
                    text.gameObject.SetActive(true);
                    /* text.text = "";
                     List<int> list = new List<int>(){1,2,3,4,5,6,7};
                     int rand;
                     for (int i = 1; i <= 7;i++)
                     {
                         rand = list[Random.Range(0, list.Count-1)];
                         print(rand);
                         text.text += rand;
                         list.Remove(rand);
                     }*/
                }
                else if (isHide)
                {
                    isHide = false;
                }
                else if (hit.transform.tag == "chest" && !isHide)
                {

                    hit.transform.gameObject.GetComponent<HideAndSeek>().Hide();
                    isHide = true;
                    // Time.timeScale = 0;
                }
                else if (hit.transform.tag == "element")
                {
                    GameBox.GetComponent<GameScript>().Game(hit.transform);
                    //hit.transform.gameObject.GetComponentInChildren<Light>().intensity=1;
                }
                else
                    if (hit.transform.tag == "key")
                {
                    key = true;
                    Destroy(hit.transform.gameObject);
                }
                }
            }

    }

}
