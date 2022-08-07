using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        //PL_KeyInput.Instance.Initialize();
        //PL_State.Instance.Initialize();
        //PL_Move.Instance.Initialize();
        //PL_Jumper.Instance.Initialize();
        PL_Transform.Instance.Initialize(transform.localScale, transform.localEulerAngles, transform.localPosition);
    }

    void Update()
    {
        //PL_KeyInput.Instance.Update_Component();
        //PL_State.Instance.Update_Component();
        //PL_Move.Instance.Update_Component();
        //PL_Jumper.Instance.Update_Component();
        PL_Transform.Instance.Update_Component();

        SetMatrix(PL_Transform.Instance.Get_World(), PL_Transform.Instance.Get_Scale());
    }

    private void SetMatrix(_matrix matWorld, Vector3 vScale)
    {
        transform.rotation = Quaternion.LookRotation(MM.DoubleToVector(matWorld._31, matWorld._32, matWorld._33).normalized);
        transform.position = MM.DoubleToVector(matWorld._41, matWorld._42, matWorld._43);
        transform.localScale = MM.FloatToVector(vScale.x, vScale.y, vScale.z);
    }

    private static Player m_pInstance;
    public static Player Instance
    {
        get
        {
            if (null == m_pInstance)
            {
                m_pInstance = GameObject.Find("Player").GetComponent<Player>();

            }
            return m_pInstance;
        }
    }
}
