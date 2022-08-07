using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void Start()
    {
        //���۳�Ʈ �ʱ�ȭ
        //Cam_KeyInput.Instance.Initialize();
        Cam_Transform.Instance.Initialize();
        //Cam_Frustum.Instance.Initialize();
        //Cam_State.Instance.Initialize();
        //Cam_Animation.Instance.Initialize();
    }

    void Update()
    {
        //���۳�Ʈ ������Ʈ
        //Cam_KeyInput.Instance.Update_Component();
        Cam_Transform.Instance.Update_Component();
        //Cam_Frustum.Instance.Update_Component();
        //Cam_Animation.Instance.Update_Component();
        //Cam_State.Instance.Update_Component();

        //��� ����
        SetMatrix(Cam_Transform.Get_ViewInv());
    }

    private void SetMatrix(_matrix matViewInv)
    {
        transform.position = MM.Get_Pos(matViewInv);
        transform.rotation = Quaternion.LookRotation(MM.Get_Look(matViewInv));
    }

    //�̱���
    private static CustomCamera m_pInstance;
    public static CustomCamera Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = GameObject.Find("Camera").GetComponent<CustomCamera>();
            }
            return m_pInstance;
        }
    }
}