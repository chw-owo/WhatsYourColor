/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_Jumper
{
    private Vector3 m_vWorldUp = MM.FloatToVector(0f, 1f, 0f);

    private Vector3 m_vJumpDis_F= MM.FloatToVector(0f, 0f, 0f);
    private Vector3 m_vJumpDis_B = MM.FloatToVector(0f, 0f, 0f);

    private Vector3 m_vDropDis_F = MM.FloatToVector(0f, 0f, 0f);
    private Vector3 m_vDropDis_B = MM.FloatToVector(0f, 0f, 0f);

    private double m_dJumpPower = 7;
    private double m_dGraAcc = 9.8;

    private double m_dJumpTime = 0;
    private double m_dDropTime = 0;

    private double m_dJumpTimeSpeed = 1.5;
    private double m_dDropTimeSpeed = 1.5;

    public void Initialize()
    {
    }

    public void Update_Component()
    {
        if (PL_State.Check_Drop())
        {
            m_dDropTime += (double)TimeMgr.Get_PL() * m_dDropTimeSpeed;
        }
        else
        {
            m_dDropTime = 0;
        }

        if (PL_State.Check_Jump())
        {
            m_dJumpTime += (double)TimeMgr.Get_PL() * m_dJumpTimeSpeed;
        }
        else
        {
            m_dJumpTime = 0;
        }

        Vector3 vPos = PL_Transform.Instance.Get_Pos();

        // 자유 낙하 공식
        if (PL_State.Check_Jump())
        {
            m_vJumpDis_F = (float)(m_dJumpPower * m_dJumpTime) * m_vWorldUp;
            vPos += (m_vJumpDis_F - m_vJumpDis_B);
            m_vJumpDis_B = m_vJumpDis_F;
        }
        if (PL_State.Check_Drop())
        {
            m_vDropDis_F = (float)(0.5f * m_dGraAcc * m_dDropTime * m_dDropTime) * m_vWorldUp;
            vPos -= (m_vDropDis_F - m_vDropDis_B);
            m_vDropDis_B = m_vDropDis_F;
        }
        if (vPos.y < 0f)
        {
            PL_State.Release_Jump();
            vPos.y = 0f;

            m_dDropTime = 0;
            m_dJumpTime = 0;

            m_vJumpDis_F = MM.FloatToVector(0f, 0f, 0f);
            m_vJumpDis_B = MM.FloatToVector(0f, 0f, 0f);

            m_vDropDis_F = MM.FloatToVector(0f, 0f, 0f);
            m_vDropDis_B = MM.FloatToVector(0f, 0f, 0f);

        }

        PL_Transform.Instance.Set_Pos(vPos);
    }

    private static PL_Jumper m_pInstance;
    public static PL_Jumper Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new PL_Jumper();
            }
            return m_pInstance;
        }
    }
}*/

