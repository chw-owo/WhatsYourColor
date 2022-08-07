using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ParticleSpline
{
    public List<Vector3> m_listRoute_Pos;
    //현재 지나는 카메라 경로
    public int m_iRoute_Cur;
    //최대 카메라 경로 수
    public int m_iRoute_Max;
    public Vector3 m_vPosCur;
    public Vector3 m_vPosF;
    public Vector3 m_vPosB;
    //방향
    public Vector3 m_vDir;
    //시간 누적값(0-1사이의 값)
    public float m_fTimeCur;
    //최대 시간값
    public float m_fTimeMax;
    //러프 속도
    public float m_fLerpSpeed;
    //스플라인 폭
    public float m_fCurvePower;
    // 스플라인 사이즈
    public float m_fScale;

    public float Get_Scale() { return m_fScale; }
    public Vector3 Get_Pos() { return m_vPosCur; }
    public Vector3 Get_Dir() { return m_vDir; }

    public void Initialize(Vector3 vStartPos) //=======vOriginPos = new Vector3(ParticleSpawner.Get_CubeX() * 10f,0f, ParticleSpawner.Get_CubeZ() * 10f);
    {


        m_listRoute_Pos = new List<Vector3>();
        //현재 지나는 카메라 경로
        m_iRoute_Cur = 0;
        //최대 카메라 경로 수
        m_iRoute_Max = 5;

        //방향
        m_vDir = Vector3.forward;

        //시간 누적값(0-1사이의 값)
        m_fTimeCur = 0f;

        //최대 시간값
        m_fTimeMax = 1f;

        //러프 속도
        m_fLerpSpeed = 1f;

        //스플라인 폭
        m_fCurvePower = 8f;//범위=======================================

        // 스플라인 사이즈
        m_fScale = 2f;

        m_vPosCur = vStartPos;
        m_vPosF = vStartPos;
        m_vPosB = vStartPos;

        //스플라인 애니메이션 초기화
        for (int i = 0; i < m_iRoute_Max; ++i)
        {
            if (i == 0)
                m_listRoute_Pos.Add(vStartPos);
            else 
                m_listRoute_Pos.Add(vStartPos + MM.FloatToVector( //2. 이거 바꾸니까 제자리에서 움직인다 근데 가운데로 모임===================================
                    Random.Range(0f, m_fCurvePower) - m_fCurvePower * 0.5f,
                    Random.Range(0f, m_fCurvePower) - m_fCurvePower * 0.5f, 
                    Random.Range(0f, m_fCurvePower) - m_fCurvePower * 0.5f));
        }
    }

    // Update is called once per frame
    public void Update_Animation(float fDeltaTime, Vector3 vStartPos)
    {
        m_fTimeCur += fDeltaTime * 1.5f;

        if (m_fTimeCur >= m_fTimeMax)
        {
            m_fTimeCur = 0f;
            ++m_iRoute_Cur;
        }

        if (m_iRoute_Cur > m_iRoute_Max - 3)
        {
            m_iRoute_Cur = m_iRoute_Cur - 1;

            for (int i = 0; i < m_iRoute_Max; ++i)
            {
                if (i < m_iRoute_Max - 1)
                    m_listRoute_Pos[i] = m_listRoute_Pos[i + 1];
                else
                    m_listRoute_Pos[i] = vStartPos + MM.FloatToVector( //3. 이거 바꾸니까 trail이 제대로 돈다
                        Random.Range(0f, m_fCurvePower) - m_fCurvePower * 0.5f,
                        Random.Range(0f, m_fCurvePower) - m_fCurvePower * 0.5f, 
                        Random.Range(0f, m_fCurvePower) - m_fCurvePower * 0.5f);
                // 수정===================================================================================
            }
        }

        m_vPosB = m_vPosCur;

        if (m_iRoute_Cur == 0)
        {
            m_vPosF = MM.CatmullRomPoint(m_listRoute_Pos[m_iRoute_Cur] * 2f - m_listRoute_Pos[m_iRoute_Cur + 1],
                m_listRoute_Pos[m_iRoute_Cur], m_listRoute_Pos[m_iRoute_Cur + 1], m_listRoute_Pos[m_iRoute_Cur + 2], m_fTimeCur / m_fTimeMax);

        }
        else
        {
            m_vPosF = MM.CatmullRomPoint(m_listRoute_Pos[m_iRoute_Cur - 1], m_listRoute_Pos[m_iRoute_Cur],
                m_listRoute_Pos[m_iRoute_Cur + 1], m_listRoute_Pos[m_iRoute_Cur + 2], m_fTimeCur / m_fTimeMax);
        }

        m_vPosCur = m_vPosF * (TimeMgr.Get_BA() * m_fLerpSpeed) + m_vPosB * (1f - TimeMgr.Get_BA() * m_fLerpSpeed);
        m_vDir = (m_vPosF - m_vPosCur).normalized * (Time.deltaTime * m_fLerpSpeed) + m_vDir.normalized * (1f - Time.deltaTime * m_fLerpSpeed);
    }
}