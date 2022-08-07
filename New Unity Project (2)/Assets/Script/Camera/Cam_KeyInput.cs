/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_KeyInput
{
    public void Initialize()
    {
    }
    public void Update_Component()
    {
        Vector3 vRot = Cam_Transform.Instance.Get_Rot();

        if (Input.GetKey(KeyCode.UpArrow))
            vRot.x += -m_fRot_Speed * TimeMgr.Get_PL();

        if (Input.GetKey(KeyCode.DownArrow))
            vRot.x += +m_fRot_Speed * TimeMgr.Get_PL();

        if (Input.GetKey(KeyCode.LeftArrow))
            vRot.y += -m_fRot_Speed * TimeMgr.Get_PL();

        if (Input.GetKey(KeyCode.RightArrow))
            vRot.y += +m_fRot_Speed * TimeMgr.Get_PL();

        Cam_Transform.Instance.Set_Rot(vRot);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Cam_Animation.Start_Animation(0);
        }
    }

    private static float m_fRot_Speed = 25f;

    //ΩÃ±€≈Ê
    private static Cam_KeyInput m_pInstance;
    public static Cam_KeyInput Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new Cam_KeyInput();
            }
            return m_pInstance;
        }
    }
}*/