using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam; 
    UIManager UIman;
    void Start()
    {
        UIman = GetComponent<UIManager>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;

        if(Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100)) {
                string tag = hit.transform.tag;
                switch (tag) {
                    case "Question":
                        hit.collider.gameObject.GetComponent<QuestionScript>().click();
                        UIman.AddPoints(100);
                        UIman.AddCoin(1);
                        break;
                    case "Brick":
                        Destroy(hit.collider.gameObject);
                        UIman.AddPoints(100);
                        break;
                    default:
                    break;
                }
            }

            
        }
    }
}
