/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Frustum
{
    //사각뿔의 5개의 꼭지점
    private static Vector3 m_vFrustumPoint_0;
    private static Vector3 m_vFrustumPoint_1;
    private static Vector3 m_vFrustumPoint_2;
    private static Vector3 m_vFrustumPoint_3;
    private static Vector3 m_vFrustumPoint_4;
    //사각뿔의 밑면을 제외한 옆면들
    private static Plane m_tPlane_0;
    private static Plane m_tPlane_1;
    private static Plane m_tPlane_2;
    private static Plane m_tPlane_3;
    //레이 검출을 위한 각 방향에 따른 구조체들
    private static Ray m_tRayUp;
    private static Ray m_tRayRight;
    private static Ray m_tRayDown;
    private static Ray m_tRayLeft;
    //레이 함수에 들어가지만 사용하지 않는 인자값
    private static float m_fRayLength = 0f;
    //해당 객체의 뷰 스페이스에서의 위치
    private static Vector3 m_vViewPos = Vector3.zero;
    //카메라 트렌스폼에서 가져온 뷰행렬
    private static _matrix m_matView;
    //객체의 반지름을 통해 구한 객체의 사선 길이
    private static float m_fDiagonal = 0f;
    //절두체의 세로 각도
    private float m_fFov = 60f;
    //절두체의 원평면의 높이
    private float m_fHeight = 0f;
    //절두체의 원평면의 너비
    private float m_fWidth = 0f;
    //절두체의 근평면
    private static float m_fNear = 0.1f;
    //절두체의 원평면
    private static float m_fFar = 1000f;

    public static bool Check_Culling(Vector3 vPos, float fRadius)
    {
        //객체의 위치를 뷰 스페이스로 전환
        m_vViewPos = MM.TransformCoord(vPos, m_matView);
        //근평면과 원평면을 통한 1차검출
        if (m_vViewPos.z < m_fNear && m_vViewPos.z > m_fFar)
            return true;
        //객체의 대각선의 길이
        m_fDiagonal = fRadius * Mathf.Sqrt(3);
        //반지름을 포함한 검출을 하기위해 레이의 시작점을 보정
        m_tRayUp.origin = MM.Vec3_AdjustY(m_vViewPos, -m_fDiagonal);
        m_tRayRight.origin = MM.Vec3_AdjustX(m_vViewPos, -m_fDiagonal);
        m_tRayDown.origin = MM.Vec3_AdjustY(m_vViewPos, +m_fDiagonal);
        m_tRayLeft.origin = MM.Vec3_AdjustX(m_vViewPos, +m_fDiagonal);
        //레이캐스트를 통한 2차 검출 
        if ((m_tPlane_0.Raycast(m_tRayUp, out m_fRayLength) && m_tPlane_2.Raycast(m_tRayDown, out m_fRayLength)) &&
            (m_tPlane_1.Raycast(m_tRayRight, out m_fRayLength) && m_tPlane_3.Raycast(m_tRayLeft, out m_fRayLength)))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Initialize()
    {
        //레이 구조체의 방향 백터 세팅
        m_tRayRight.direction = Vector3.right;
        m_tRayUp.direction = Vector3.up;
        m_tRayDown.direction = Vector3.down;
        m_tRayLeft.direction = Vector3.left;
    }
    public void Update_Component()
    {
        //탄젠트30 = 높이 / 밑변 (밑변은 1(임시 z값))
        m_fHeight = Mathf.Tan(Mathf.PI * m_fFov * 0.5f / 180f) * m_fFar;
        m_fWidth = m_fHeight * 16 / 9;

        //절두체 꼭지점을 세팅

        m_vFrustumPoint_0.x = 0f;
        m_vFrustumPoint_0.y = 0f;
        m_vFrustumPoint_0.z = 0f;

        m_vFrustumPoint_1.x = -m_fWidth;
        m_vFrustumPoint_1.y = +m_fHeight;
        m_vFrustumPoint_1.z = m_fFar;

        m_vFrustumPoint_2.x = +m_fWidth;
        m_vFrustumPoint_2.y = +m_fHeight;
        m_vFrustumPoint_2.z = m_fFar;

        m_vFrustumPoint_3.x = +m_fWidth;
        m_vFrustumPoint_3.y = -m_fHeight;
        m_vFrustumPoint_3.z = m_fFar;

        m_vFrustumPoint_4.x = -m_fWidth;
        m_vFrustumPoint_4.y = -m_fHeight;
        m_vFrustumPoint_4.z = m_fFar;

        //절두체 꼭지점을 이용해 절두체 평면을 세팅
        m_tPlane_0.Set3Points(m_vFrustumPoint_0, m_vFrustumPoint_1, m_vFrustumPoint_2);
        m_tPlane_1.Set3Points(m_vFrustumPoint_0, m_vFrustumPoint_2, m_vFrustumPoint_3);
        m_tPlane_2.Set3Points(m_vFrustumPoint_0, m_vFrustumPoint_3, m_vFrustumPoint_4);
        m_tPlane_3.Set3Points(m_vFrustumPoint_0, m_vFrustumPoint_4, m_vFrustumPoint_1);

        //뷰 행렬을 업데이트
        m_matView = Cam_Transform.Get_View();
    }

    //싱글톤
    private static Cam_Frustum m_pInstance;
    public static Cam_Frustum Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = new Cam_Frustum();
            }
            return m_pInstance;
        }
    }
}*/