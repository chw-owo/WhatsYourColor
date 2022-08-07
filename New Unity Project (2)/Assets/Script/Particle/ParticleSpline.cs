using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ParticleSpline
{
    public List<Vector3> m_listRoute_Pos;
    //���� ������ ī�޶� ���
    public int m_iRoute_Cur;
    //�ִ� ī�޶� ��� ��
    public int m_iRoute_Max;
    public Vector3 m_vPosCur;
    public Vector3 m_vPosF;
    public Vector3 m_vPosB;
    //����
    public Vector3 m_vDir;
    //�ð� ������(0-1������ ��)
    public float m_fTimeCur;
    //�ִ� �ð���
    public float m_fTimeMax;
    //���� �ӵ�
    public float m_fLerpSpeed;
    //���ö��� ��
    public float m_fCurvePower;
    // ���ö��� ������
    public float m_fScale;

    public float Get_Scale() { return m_fScale; }
    public Vector3 Get_Pos() { return m_vPosCur; }
    public Vector3 Get_Dir() { return m_vDir; }

    public void Initialize(Vector3 vStartPos) //=======vOriginPos = new Vector3(ParticleSpawner.Get_CubeX() * 10f,0f, ParticleSpawner.Get_CubeZ() * 10f);
    {


        m_listRoute_Pos = new List<Vector3>();
        //���� ������ ī�޶� ���
        m_iRoute_Cur = 0;
        //�ִ� ī�޶� ��� ��
        m_iRoute_Max = 5;

        //����
        m_vDir = Vector3.forward;

        //�ð� ������(0-1������ ��)
        m_fTimeCur = 0f;

        //�ִ� �ð���
        m_fTimeMax = 1f;

        //���� �ӵ�
        m_fLerpSpeed = 1f;

        //���ö��� ��
        m_fCurvePower = 8f;//����=======================================

        // ���ö��� ������
        m_fScale = 2f;

        m_vPosCur = vStartPos;
        m_vPosF = vStartPos;
        m_vPosB = vStartPos;

        //���ö��� �ִϸ��̼� �ʱ�ȭ
        for (int i = 0; i < m_iRoute_Max; ++i)
        {
            if (i == 0)
                m_listRoute_Pos.Add(vStartPos);
            else 
                m_listRoute_Pos.Add(vStartPos + MM.FloatToVector( //2. �̰� �ٲٴϱ� ���ڸ����� �����δ� �ٵ� ����� ����===================================
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
                    m_listRoute_Pos[i] = vStartPos + MM.FloatToVector( //3. �̰� �ٲٴϱ� trail�� ����� ����
                        Random.Range(0f, m_fCurvePower) - m_fCurvePower * 0.5f,
                        Random.Range(0f, m_fCurvePower) - m_fCurvePower * 0.5f, 
                        Random.Range(0f, m_fCurvePower) - m_fCurvePower * 0.5f);
                // ����===================================================================================
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