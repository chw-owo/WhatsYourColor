using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ParticleSettings
{
    //public Vector3 v_BoxPos;
    public float v_BoxPosx;
    public float v_BoxPosy;

    //쉐이더 세팅 변수
    public MeshRenderer comRenderer;
    public Material shaderMaterial;

    //월드행렬
    public _matrix matWorld;
    //방향, 위치, 크기 변수
    public Vector3 vDir;
    public Vector3 vPos;
    public Vector3 vOriginPos;
    public float fScale;
    //애니메이션 수치
    public float fWaveValue;
    //파동 변수
    public Wave_Ani0[] tWave_Ani0;
    //스플라인 애니메이션 변수
    public ParticleSpline tSpline;
    //생성할 큐브 트레일
    public int iTrailNum;
    public float fTrailCur;
    public float fTrailMax; //수정====================================================
    public float fScaleAniCur;
    //파티클 상태 변수
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

        //쉐이더 세팅 변수
        comRenderer = null;
        shaderMaterial = null;

        //월드행렬
        matWorld = MM.Matrix_Identity();
    //방향, 위치, 크기 변수
        vDir = Vector3.forward;
        vPos = Vector3.zero;
        vOriginPos = Vector3.zero;       //==============================================================
        fScale = 1f;
    //애니메이션 수치
        fWaveValue = 0f;
    //파동 변수
        tWave_Ani0 = new Wave_Ani0[Wave_Ani0.iWaveNum];
    //스플라인 애니메이션 변수
        tSpline = new ParticleSpline();

    //생성할 큐브 트레일
        iTrailNum = 30;//=============================
        fTrailCur = 0f;
        fTrailMax = 0.01f; //수정====================================================
        fScaleAniCur = 0f;

    //파티클 상태 변수
        PT_DEFAULT = 0b0000000000000000000000000000000000000000000000000000000000000001;
        PT_SPLINE = 0b0000000000000000000000000000000000000000000000000000000000000010;
        m_ulState = PT_DEFAULT;

    }
}
