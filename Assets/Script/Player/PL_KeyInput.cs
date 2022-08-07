/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_KeyInput 
{
    public void Initialize()
    {
        
    }

    // Update is called once per frame
    public void Update_Component()
    {
        if (Input.GetKey(KeyCode.W))
        {
            PL_State.Set_Forward();
        }
        else
        {
            PL_State.Release_Forward();
        }

        if (Input.GetKey(KeyCode.S))
        {
            PL_State.Set_Back();
        }
        else
        {
            PL_State.Release_Back();
        }

        if (Input.GetKey(KeyCode.D))
        {
            PL_State.Set_Left();
        }
        else
        {
            PL_State.Release_Left();
        }

        if (Input.GetKey(KeyCode.A))
        {
            PL_State.Set_Right();
        }
        else
        {
            PL_State.Release_Right();
        }

        if (Input.GetKey(KeyCode.W)| Input.GetKey(KeyCode.S)
            |Input.GetKey(KeyCode.D)| Input.GetKey(KeyCode.A))
        {
            PL_State.Set_Walk();
        }
        else
        {
            PL_State.Release_Walk();
        }

        /*if (Input.GetKey(KeyCode.Space))
        {
            PL_State.Set_Jump();
        }
    }
    private static PL_KeyInput m_pInstance;
    public static PL_KeyInput Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new PL_KeyInput();

            }
            return m_pInstance;
        }
    }
}*/
