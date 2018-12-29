using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public float speed = 5;
    // public Rigidbody bullet;
    //public Transform gun;
    Rigidbody rb;
    Transform cam;
    public GameObject spotLight;
    public bool isLight;
    Vector3 rotCam;
    bool isOpen;
    void Start()
    {
        isLight = false;
        isOpen = false;
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    private void FixedUpdate()
    {

        rb.velocity = transform.TransformDirection(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);
        transform.Rotate(0, Input.GetAxis("Mouse X") * 2f, 0, Space.World);
        rotCam = cam.localEulerAngles;
        cam.Rotate(-Input.GetAxis("Mouse Y") * 2f, 0, 0, Space.Self);

        //spotLight.transform.Rotate(0, Input.GetAxis("Mouse X"), 0, Space.World);

        //spotLight.transform.TransformDirection(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);
        if ((cam.localEulerAngles.x > 310 && cam.localEulerAngles.x < 360) || (cam.localEulerAngles.x > 0 && cam.localEulerAngles.x < 25))
        {
        }
        else
        {
            cam.localEulerAngles = rotCam;
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
        }

        /* if (Input.GetMouseButtonDown(0))
         {
             Rigidbody temp = Instantiate(bullet, gun.position, Quaternion.identity);
             temp.AddForce(gun.TransformDirection(new Vector3(0, 0, 1000)));
         }*/
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

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50))
            {
                if (hit.transform.tag  == "door")
                {
                    hit.transform.GetComponent<Open>().OpenTheDoor();
                }
               
            }
        }
    }

}
