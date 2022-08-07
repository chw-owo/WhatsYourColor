/*using System.Collections.Generic;
using UnityEngine;

public class Cam_Animation
{
    private const int CAM_ROUTE_MIN = 3;
    private static List<Vector3> m_listRoute_Pos = new List<Vector3>();
    private static int m_iRoute_Cur = 0;
    private static int m_iRoute_Max = 0;

    private static Vector3 m_vPosCur = Vector3.zero;
    private static Vector3 m_vPosF = Vector3.zero;
    private static Vector3 m_vPosB = Vector3.zero;

    private static Vector3 m_vDir = Vector3.forward;
    private static float m_fTimeCur = 0f;
    private static float m_fTimeMax = 1f;
    private static float m_fLerpSpeed = 1;

    public static Vector3 Get_Pos() { return m_vPosCur; }
    public static Vector3 Get_Dir() { return m_vDir; }

    public static void Start_Animation(int iIndex)
    {
        if (iIndex > GameObject.Find("Cam_Animation").transform.childCount - 1)
        {
            Debug.Log("해당 인덱스의 애니메이션이 존재하지 않습니다");
            return;
        }
        if(CAM_ROUTE_MIN> GameObject.Find("Cam_Animation").transform.GetChild(iIndex).transform.childCount){

            Debug.Log("최소" + (CAM_ROUTE_MIN) + "개 이상의 카메라 경로를 설정해야 합니다");
            return;
        }

        m_iRoute_Cur = 0;
        m_iRoute_Max = GameObject.Find("Cam_Animation").transform.GetChild(iIndex).transform.childCount;
        m_fTimeCur = 0;
        m_listRoute_Pos.Clear();
        for(int i = 0; i <m_iRoute_Max; ++i)
        {
            m_listRoute_Pos.Add(GameObject.Find("Cam_Animation").transform.
                GetChild(iIndex).transform.GetChild(i).transform.localPosition);
        }

        m_vPosCur = m_listRoute_Pos[m_iRoute_Cur];
        m_vPosF = m_listRoute_Pos[m_iRoute_Cur+1];
        m_vDir = m_vPosF - m_vPosCur;
        Cam_State.Set_Animation();
    }


    public void Initialize()
    {
        
    }

    // Update is called once per frame
    public void Update_Component()
    {
        m_fTimeCur += Time.deltaTime;

        if (m_fTimeCur >= m_fTimeMax)
        {
            m_fTimeCur = 0f;
            ++m_iRoute_Cur;
        }
        if (m_iRoute_Cur > m_iRoute_Max - 2)
        {
            Cam_State.Release_Animation();
            return;
        }

        m_vPosB = m_vPosCur;

        if (m_iRoute_Cur == 0)
        {
            m_vPosF = MM.CatmullRomPoint(m_listRoute_Pos[m_iRoute_Cur] * 2f - m_listRoute_Pos[m_iRoute_Cur + 1],
                m_listRoute_Pos[m_iRoute_Cur], m_listRoute_Pos[m_iRoute_Cur + 1], m_listRoute_Pos[m_iRoute_Cur + 2],
                m_fTimeCur / m_fTimeMax);
        }
        else if (m_iRoute_Cur == m_iRoute_Max - 2)
        {
            m_vPosF = MM.CatmullRomPoint(m_listRoute_Pos[m_iRoute_Cur-1], m_listRoute_Pos[m_iRoute_Cur],
                m_listRoute_Pos[m_iRoute_Cur+1], m_listRoute_Pos[m_iRoute_Cur + 1]*2f - m_listRoute_Pos[m_iRoute_Cur],
                m_fTimeCur / m_fTimeMax);
        }
        else
        {
            m_vPosF = MM.CatmullRomPoint(m_listRoute_Pos[m_iRoute_Cur - 1], m_listRoute_Pos[m_iRoute_Cur],
                m_listRoute_Pos[m_iRoute_Cur + 1], m_listRoute_Pos[m_iRoute_Cur + 2],
                m_fTimeCur / m_fTimeMax);
        }

        m_vPosCur = m_vPosF * (Time.deltaTime * m_fLerpSpeed) + m_vPosB * (1f - Time.deltaTime * m_fLerpSpeed);
        m_vDir = (m_vPosF - m_vPosCur).normalized * (Time.deltaTime * m_fLerpSpeed) + m_vDir.normalized *
            (1f - Time.deltaTime * m_fLerpSpeed);
    }
    private static Cam_Animation m_pInstance;
    public static Cam_Animation Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new Cam_Animation();
            }
            return m_pInstance;
        }
    }
}
*/