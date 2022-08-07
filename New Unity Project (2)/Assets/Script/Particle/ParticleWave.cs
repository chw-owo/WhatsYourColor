using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Wave_Ani0
{
    //파동 갯수
    public static int iWaveNum = 20; //=====================================

    //파동  
    public static float fStartRandom = 4f;

    //파동의 반지름, 바깥쪽 원, 중간쪽 원, 안쪽원
    public float fWaveRadius;
    public float fWaveIn;
    public float fWaveMid;
    public float fWaveOut;

    //파동 스피드
    public float fWaveSpeed;

    //애니메이션 수치
    public float fAnimationValue;

    //현재 시간
    public float fTimeCur;

    //최대 시간, 지속시간
    public float fTimeMax;

    //스위치 온
    public bool bSwitchOn;

    public void Update_KeyInput(float fStartTime)
    {
        fWaveIn = -fWaveRadius * 2f - (fWaveRadius * fStartTime);
        fWaveMid = -fWaveRadius - (fWaveRadius * fStartTime);
        fWaveOut = -(fWaveRadius * fStartTime);
        fTimeCur = 0f;
    }
    public float Update_Animation(Vector3 vAniStartPos, float fDeltaTime, Vector3 vPos)
    {
        //지속시간 적용
        if (fTimeCur > fTimeMax)
        {
            fAnimationValue = 0f;
            return 0f;
        }

        //파동 시간값 누적
        fTimeCur += fDeltaTime * fWaveSpeed;
        fWaveIn += fDeltaTime * fWaveSpeed;
        fWaveMid += fDeltaTime * fWaveSpeed;
        fWaveOut += fDeltaTime * fWaveSpeed;

        //파동 시작점과 큐브와의 거리
        float fDis = Vector3.Distance(vAniStartPos, vPos);

        //파동 애니메이션 값 산출
        if (fDis < fWaveOut && fDis > fWaveIn)
            fAnimationValue = (1f - Mathf.Pow((Mathf.Abs(fDis - fWaveMid)) / fWaveRadius, 1.5f));
        else
            fAnimationValue = 0f;

        return fAnimationValue;
    }
    public void Initialize()
    {
        fWaveRadius = 7f;
        fWaveIn = MM.Float_Max();
        fWaveMid = MM.Float_Max();
        fWaveOut = MM.Float_Max();

        fWaveSpeed = 10f;

        fAnimationValue = 0f;

        fTimeCur = MM.Float_Max();

        fTimeMax = 100f;

        bSwitchOn = false;
    }

    //Get 함수
    public float Get_AnimationValue() { return fAnimationValue; }
}
