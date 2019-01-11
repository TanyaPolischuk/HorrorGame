using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public Transform player;
    Transform[] elements = new Transform[2];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Game(Transform el)
    {
        print(el);
        /*if (elements[0]==el)
         {
             elements[0] = null;
         }
         else if (elements[1]==el)
         {
             elements[1] = null;
         }
             el.gameObject.GetComponentInChildren<Light>().intensity = el.gameObject.GetComponentInChildren<Light>().intensity == 1 ? 0 : 1;*/
        if (el.gameObject.GetComponentInChildren<Light>().intensity == 0) //коли вмикаємо лампочку
        {
            print("turn on");
            el.gameObject.GetComponentInChildren<Light>().intensity = 1;
            if (elements[0] && !elements[1])  //якщо перший елемент вже вибрано міняємо його з другим
            {
                //заміна
                elements[1] = el;
                Color temp = new Color();
                //дані першого елементу збережені в темп
                string temp2 = elements[0].GetComponent<MeshRenderer>().material.name;
                temp = elements[0].GetComponentInChildren<Light>().color;
                //в перший елемент записуємо дані другого(колір назву матеріалу колір світла)
                elements[0].GetComponent<MeshRenderer>().material.color = elements[1].GetComponent<MeshRenderer>().material.color;
                elements[0].GetComponent<MeshRenderer>().material.name = elements[1].GetComponent<MeshRenderer>().material.name;
                print(elements[0].GetComponent<MeshRenderer>().material.name);
                elements[0].GetComponentInChildren<Light>().color = elements[1].GetComponentInChildren<Light>().color;

                //в другий елемент записуємо дані з темп
                elements[1].GetComponent<MeshRenderer>().material.color = temp;
                elements[1].GetComponent<MeshRenderer>().material.name = temp2;
                elements[1].GetComponentInChildren<Light>().color = temp;
                //занулення
                elements[0].GetComponentInChildren<Light>().intensity = 0;
                //elements[1].GetComponentInChildren<Light>().intensity = 0;
                print(elements[0].GetComponent<MeshRenderer>().material.name);
                print(elements[1].GetComponent<MeshRenderer>().material.name);
                elements[0] = null;
                elements[1] = null;



            }

            if (!elements[0] || !elements[1])
            {
                if (!elements[0])
                    elements[0] = el;
                else elements[1] = el;
            }
            else if (elements[0] && elements[1])
            {
                elements[0].gameObject.GetComponentInChildren<Light>().intensity = 0;
                elements[0] = elements[1];
                elements[1] = el;
            }
        }
        else
        {
            el.gameObject.GetComponentInChildren<Light>().intensity = 0;
            if (elements[0] == el)
            {
                elements[0] = elements[1];
                print("turn off 1");
            }
            else
            {
                print("turn off 2");
            }
            elements[1] = null;
        }
    }
}
