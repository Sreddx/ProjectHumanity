using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;
    public Transform cam;

    RaycastHit hit;
    float range = 1000.0f;


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                shootRay();
                m_shootRateTimeStamp = Time.time + shootRate;
            }
        }

    }

    void shootRay()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Check all layers except the Player layer
        int layerMask = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(cam.position, cam.forward, out hit, range, layerMask))
        {
            Debug.Log(hit.point);
            GameObject laser = GameObject.Instantiate(m_shotPrefab, this.transform.position, this.transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);


        }

    }



}
