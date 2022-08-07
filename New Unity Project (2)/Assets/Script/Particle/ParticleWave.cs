using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Wave_Ani0
{
    //�ĵ� ����
    public static int iWaveNum = 20; //=====================================

    //�ĵ�  
    public static float fStartRandom = 4f;

    //�ĵ��� ������, �ٱ��� ��, �߰��� ��, ���ʿ�
    public float fWaveRadius;
    public float fWaveIn;
    public float fWaveMid;
    public float fWaveOut;

    //�ĵ� ���ǵ�
    public float fWaveSpeed;

    //�ִϸ��̼� ��ġ
    public float fAnimationValue;

    //���� �ð�
    public float fTimeCur;

    //�ִ� �ð�, ���ӽð�
    public float fTimeMax;

    //����ġ ��
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
        //���ӽð� ����
        if (fTimeCur > fTimeMax)
        {
            fAnimationValue = 0f;
            return 0f;
        }

        //�ĵ� �ð��� ����
        fTimeCur += fDeltaTime * fWaveSpeed;
        fWaveIn += fDeltaTime * fWaveSpeed;
        fWaveMid += fDeltaTime * fWaveSpeed;
        fWaveOut += fDeltaTime * fWaveSpeed;

        //�ĵ� �������� ť����� �Ÿ�
        float fDis = Vector3.Distance(vAniStartPos, vPos);

        //�ĵ� �ִϸ��̼� �� ����
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

    //Get �Լ�
    public float Get_AnimationValue() { return fAnimationValue; }
}
