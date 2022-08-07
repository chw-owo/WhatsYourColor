using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ParticleSettings
{
    //public Vector3 v_BoxPos;
    public float v_BoxPosx;
    public float v_BoxPosy;

    //���̴� ���� ����
    public MeshRenderer comRenderer;
    public Material shaderMaterial;

    //�������
    public _matrix matWorld;
    //����, ��ġ, ũ�� ����
    public Vector3 vDir;
    public Vector3 vPos;
    public Vector3 vOriginPos;
    public float fScale;
    //�ִϸ��̼� ��ġ
    public float fWaveValue;
    //�ĵ� ����
    public Wave_Ani0[] tWave_Ani0;
    //���ö��� �ִϸ��̼� ����
    public ParticleSpline tSpline;
    //������ ť�� Ʈ����
    public int iTrailNum;
    public float fTrailCur;
    public float fTrailMax; //����====================================================
    public float fScaleAniCur;
    //��ƼŬ ���� ����
    public ulong PT_DEFAULT;
    public ulong PT_SPLINE;
    public ulong m_ulState;
    public bool Check_Default() { return (MM.StateExist(m_ulState, PT_DEFAULT)); }
    public void Set_Default() { m_ulState |= PT_DEFAULT; }
    public bool Check_Spline() { return (MM.StateExist(m_ulState, PT_SPLINE)); }
    public void Set_Spline() { m_ulState |= PT_SPLINE; }
    public void Release_Spline() { if (Check_Spline()) m_ulState ^= PT_SPLINE; }

    public void Initialize()
    {
        //v_BoxPos = Vector3.zero;
        v_BoxPosx = 0f;
        v_BoxPosy = 0f;

        //���̴� ���� ����
        comRenderer = null;
        shaderMaterial = null;

        //�������
        matWorld = MM.Matrix_Identity();
    //����, ��ġ, ũ�� ����
        vDir = Vector3.forward;
        vPos = Vector3.zero;
        vOriginPos = Vector3.zero;       //==============================================================
        fScale = 1f;
    //�ִϸ��̼� ��ġ
        fWaveValue = 0f;
    //�ĵ� ����
        tWave_Ani0 = new Wave_Ani0[Wave_Ani0.iWaveNum];
    //���ö��� �ִϸ��̼� ����
        tSpline = new ParticleSpline();

    //������ ť�� Ʈ����
        iTrailNum = 30;//=============================
        fTrailCur = 0f;
        fTrailMax = 0.01f; //����====================================================
        fScaleAniCur = 0f;

    //��ƼŬ ���� ����
        PT_DEFAULT = 0b0000000000000000000000000000000000000000000000000000000000000001;
        PT_SPLINE = 0b0000000000000000000000000000000000000000000000000000000000000010;
        m_ulState = PT_DEFAULT;

    }
}
