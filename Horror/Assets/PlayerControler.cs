using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public Sprite keySprite, bookSprite;
    public Transform vampire, GameBox;
    public Image image, keyImage, bookImage, pointImage;
    public Text text;
    public Transform[] sphere;
    public float speed = 10;
    public Transform scroll;
    public bool isHide;
    Rigidbody rb;
    Transform cam;
    public GameObject spotLight;
    public bool isLight, key;
    Vector3 rotCam;
    bool isOpen, isDead;
    public AudioClip[] clip;
    AudioSource source;
    public Ray ray;
    public AudioSource sourceFon, door;
    float move;
    Transform posCamera, newPosCamera;
    void Start()
    {
        posCamera = Camera.main.transform;
        key = false;
        isDead = vampire.GetComponent<VampireControler>().isDead;
        print(isDead);
        source = GetComponent<AudioSource>();
        text.text = "";
        List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        int rand;
        for (int i = 1; i <= 7; i++)
        {
            rand = list[Random.Range(0, list.Count - 1)];
            print(rand);
            text.text += rand;
            list.Remove(rand);
        }
        source.Play();
        isHide = false;
        isLight = false;
        isOpen = false;
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        isDead = vampire.GetComponent<VampireControler>().isDead;

        if (isHide==false && !isDead)
         {
        rb.velocity = transform.TransformDirection(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);
            transform.Rotate(0, Input.GetAxis("Mouse X") * 2f, 0, Space.World);
        rotCam = cam.localEulerAngles;
        cam.Rotate(-Input.GetAxis("Mouse Y") * 2f, 0, 0, Space.Self);
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
        //death
        if (isDead)
        {
            gameObject.transform.LookAt(vampire);
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position - new Vector3(0, 1.65f, 0), Vector3.down, 0.2f))
        {
         
            rb.AddForce(0, 300, 0);
         
        }

       //check the game
        if (Input.GetKeyDown(KeyCode.G))
        {
            string s="";
            for (int i = 0; i <= 6; i++)
            {
                s+= sphere[i].GetComponent<MeshRenderer>().material.name[0];
                print(s);
            }
            print(text.text);
            if (s==text.text)
            {
                print("win");
                vampire.GetComponent<VampireControler>().PlayerWon();
                image.gameObject.SetActive(true);
                text.gameObject.SetActive(true);
                text.text = "Congratulations!!!";
            }
        }
        //light
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isLight = !isLight;
            spotLight.SetActive(isLight);
        }
        //cursor
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Tab)&&bookImage.IsActive())
        {
            image.gameObject.SetActive(!image.IsActive());
            text.gameObject.SetActive(!text.IsActive());
            if (image.IsActive())
            {
                pointImage.gameObject.SetActive(false);
            }
            else pointImage.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (image.IsActive())
            {
                image.gameObject.SetActive(false);
                text.gameObject.SetActive(false);
            }
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50))
            {
                print(hit.transform.gameObject.tag);
                if (hit.transform.tag == "door" && Vector3.Distance(gameObject.transform.position, hit.transform.position) < 3)
                {
                    hit.transform.GetComponent<Open>().isPlayer = true;
                    print("door");
                    hit.transform.GetComponent<Open>().OpenTheDoor();
                }
                else if (hit.transform.tag == "scroll" && !image.IsActive())
                {
                    if (!keyImage.sprite)
                    {
                        keyImage.sprite = bookSprite;
                        keyImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        bookImage.sprite = bookSprite;
                        bookImage.gameObject.SetActive(true);
                    }
                    hit.transform.gameObject.SetActive(false);
                }
                else if (isHide)
                {
                    isHide = false;
                }
                else if (hit.transform.tag == "chest" && !isHide && Vector3.Distance(gameObject.transform.position,hit.transform.position)<4)
                {
                    hit.transform.gameObject.GetComponent<HideAndSeek>().Hide();
                    isHide = true;
                }
                else if (hit.transform.tag == "element")
                {
                    GameBox.GetComponent<GameScript>().Game(hit.transform);
                }
                else
                    if (hit.transform.tag == "key")
                {
                    key = true;
                    Destroy(hit.transform.gameObject);
                    if (!keyImage.sprite)
                    {
                        keyImage.sprite = keySprite;
                        keyImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        bookImage.sprite = keySprite;
                        bookImage.gameObject.SetActive(true);
                    }
                }
                }
            }

    }

}
