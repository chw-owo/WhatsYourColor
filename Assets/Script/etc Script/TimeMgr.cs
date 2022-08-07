using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMgr : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 50;
    }
    private void Update()
    {
        Update_DeltaTime();

        m_fTimeCur += Time.deltaTime;
        ++m_iFrameCnt;

        if (m_fTimeCur > m_fTimeMax)
        {
            //Debug.Log(m_iFrameCnt);

            m_fTimeCur = 0f;
            m_iFrameCnt = 0;
        }
    }

    private void Update_DeltaTime()
    {
        m_fPL_DeltaTime = Time.deltaTime * m_fPL_DeltaTimeSpeed;
        m_fMO_DeltaTime = Time.deltaTime * m_fMO_DeltaTimeSpeed;
        m_fBG_DeltaTime = Time.deltaTime * m_fBG_DeltaTimeSpeed;
    }

    static public float Get_PL() { return m_fPL_DeltaTime; }
    static public float Get_MO() { return m_fMO_DeltaTime; }
    static public float Get_BA() { return m_fBG_DeltaTime; }

    static public void Set_PL(float fSpeed) { m_fPL_DeltaTimeSpeed = fSpeed; }
    static public void Set_MO(float fSpeed) { m_fMO_DeltaTimeSpeed = fSpeed; }
    static public void Set_BG(float fSpeed) { m_fBG_DeltaTimeSpeed = fSpeed; }


    private float m_fTimeCur = 0f;
    private float m_fTimeMax = 1f;
    private int m_iFrameCnt = 0;

    static private float m_fPL_DeltaTime = 0f;
    static private float m_fMO_DeltaTime = 0f;
    static private float m_fBG_DeltaTime = 0f;

    static private float m_fPL_DeltaTimeSpeed = 1f;
    static private float m_fMO_DeltaTimeSpeed = 1f;
    static private float m_fBG_DeltaTimeSpeed = 1f;
}