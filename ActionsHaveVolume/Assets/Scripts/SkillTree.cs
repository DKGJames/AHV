using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillTree : MonoBehaviour
{
    float zoom;
    float prevZoom;
    float zoomDistance;

    private Vector2 pos;

    public GameObject tilemap;
    public GameObject testbutton1;
    public GameObject testbutton2;
    public GameObject testbutton3;

    void Start()
    {
        pos = tilemap.transform.position;
        if (PlayerPrefs.GetInt("TestButton1", 0) == 1)
        {
            testbutton1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("TestButton2", 0) == 1)
        {
            testbutton2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("TestButton3", 0) == 1)
        {
            testbutton3.SetActive(true);
        }
    }

    void Update()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * 150;
        if (zoom <= 0 && zoom >= -530)
        {
            zoomDistance -= Input.GetAxis("Mouse ScrollWheel") * 150;
            pos.y = zoomDistance;
            tilemap.transform.position = pos;    
        }else
        {
            zoom = prevZoom;
        }
        prevZoom = zoom;      
    }

    public void Next()
    {
        SceneManager.LoadScene(1);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void TestButton1()
    {
        if (PlayerPrefs.GetInt("Total", 0) >= 0)
        {
            PlayerPrefs.SetInt("TestButton1", 1);
            testbutton1.SetActive(true);
        }
    }

    public void TestButton2()
    {
        if (PlayerPrefs.GetInt("Total", 0) >= 1)
        {
            PlayerPrefs.SetInt("TestButton3", 1);
            testbutton2.SetActive(true);
        }
    }

    public void TestButton3()
    {
        if (PlayerPrefs.GetInt("Total", 0) >= 2)
        {
            PlayerPrefs.SetInt("TestButton3", 1);
            testbutton3.SetActive(true);
        }
    }

}
