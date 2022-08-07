using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distortion : MonoBehaviour
{
    private static Vector3 m_vMousePos = Vector3.zero;
    public static Vector3 Get_MousePos() { return m_vMousePos; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //m_vMousePos = BodySourceView.Get_Pos();
        //m_vMousePos = transform.position;

        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//targetPosition); // Input.mousePosition); //Vector3 º¯¼ö

        m_vMousePos = MM.TransformCoord(ray.origin, Cam_Transform.Get_View()) * 70; //150f
        m_vMousePos.z = 0f;
        */


    }
}
