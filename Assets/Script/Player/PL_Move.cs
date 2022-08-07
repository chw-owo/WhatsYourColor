/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Move 
{
    public void Initialize()
    {
        
    }

    public void Update_Component()
    {
        Vector3 vPos = PL_Transform.Instance.Get_Pos();
        Vector3 vDir = PL_Transform.Instance.Get_Dir();

        Vector3 vCamLook = MM.Get_Look(Cam_Transform.Get_ViewInv());
        Vector3 vCamRight = MM.Get_Right(Cam_Transform.Get_ViewInv());


        if (PL_State.Check_Forward() && PL_State.Check_Walk())
        {
            vPos += vCamLook * TimeMgr.Get_PL() * m_fMove_Speed;
            vDir = (vCamLook * TimeMgr.Get_PL() * m_fRot_Speed + vDir * (1f - TimeMgr.Get_PL() * m_fRot_Speed)).normalized;
        }
        else if (PL_State.Check_Back() && PL_State.Check_Walk())
        {
            vPos += -vCamLook * TimeMgr.Get_PL() * m_fMove_Speed;
            vDir = (-vCamLook * TimeMgr.Get_PL() * m_fRot_Speed + vDir * (1f - TimeMgr.Get_PL() * m_fRot_Speed)).normalized;

        }
        else if (PL_State.Check_Left() && PL_State.Check_Walk())
        {
            vPos += vCamRight * TimeMgr.Get_PL() * m_fMove_Speed;
            vDir = (vCamRight * TimeMgr.Get_PL() * m_fRot_Speed + vDir * (1f - TimeMgr.Get_PL() * m_fRot_Speed)).normalized;
        }
        else if (PL_State.Check_Right() && PL_State.Check_Walk())
        {
            vPos += -vCamRight * TimeMgr.Get_PL() * m_fMove_Speed;
            vDir = (-vCamRight * TimeMgr.Get_PL() * m_fRot_Speed + vDir * (1f - TimeMgr.Get_PL() * m_fRot_Speed)).normalized;
        }

        PL_Transform.Instance.Set_Pos(vPos);
        PL_Transform.Instance.Set_Dir(vDir);
    }

    private static float m_fMove_Speed = 2f;
    private static float m_fRot_Speed = 5f;

    //ΩÃ±€≈Ê
    private static PL_Move m_pInstance;
    public static PL_Move Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new PL_Move();
            }
            return m_pInstance;
        }
    }
}*/
