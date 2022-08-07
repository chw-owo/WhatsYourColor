using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct _matrix
{
    public double _11, _12, _13, _14;
    public double _21, _22, _23, _24;
    public double _31, _32, _33, _34;
    public double _41, _42, _43, _44;

    public _matrix(double __11, double __12, double __13, double __14,
                            double __21, double __22, double __23, double __24,
                            double __31, double __32, double __33, double __34,
                            double __41, double __42, double __43, double __44)
    {
        _11 = __11; _12 = __12; _13 = __13; _14 = __14;
        _21 = __21; _22 = __22; _23 = __23; _24 = __24;
        _31 = __31; _32 = __32; _33 = __33; _34 = __34;
        _41 = __41; _42 = __42; _43 = __43; _44 = __44;
    }
    public void Set(double __11, double __12, double __13, double __14,
                                double __21, double __22, double __23, double __24,
                                double __31, double __32, double __33, double __34,
                                double __41, double __42, double __43, double __44)
    {
        _11 = __11; _12 = __12; _13 = __13; _14 = __14;
        _21 = __21; _22 = __22; _23 = __23; _24 = __24;
        _31 = __31; _32 = __32; _33 = __33; _34 = __34;
        _41 = __41; _42 = __42; _43 = __43; _44 = __44;
    }
    public void Identity()
    {
        _11 = 1.0; _12 = 0.0; _13 = 0.0; _14 = 0.0;
        _21 = 0.0; _22 = 1.0; _23 = 0.0; _24 = 0.0;
        _31 = 0.0; _32 = 0.0; _33 = 1.0; _34 = 0.0;
        _41 = 0.0; _42 = 0.0; _43 = 0.0; _44 = 1.0;
    }
    public void ReflectZ()
    {
        _11 = 1.0; _12 = 0.0; _13 = 0.0; _14 = 0.0;
        _21 = 0.0; _22 = 1.0; _23 = 0.0; _24 = 0.0;
        _31 = 0.0; _32 = 0.0; _33 = -1.0; _34 = 0.0;
        _41 = 0.0; _42 = 0.0; _43 = 0.0; _44 = 1.0;
    }
    public static _matrix operator *(_matrix mat1, _matrix mat2)
    {
        return MM.Mul(mat1, mat2);
    }
}

public class MM : MonoBehaviour
{
    enum eInfo { INFO_RIGHT, INFO_UP, INFO_LOOK, INFO_POS, INFO_END };

    private static _matrix m_matOut;
    private static Matrix4x4 m_mat44;
    private static Vector3 m_vOut;
    private static Vector4 m_vOutShader;
    private static Vector3 m_vWorldUp = new Vector3(0, 1, 0);
    private static Vector3[] m_vInfo = new Vector3[(int)eInfo.INFO_END];
    private static float m_fOut;

    public static _matrix Scale(float fScale)
    {
        m_matOut.Identity();
        m_matOut._11 = fScale;
        m_matOut._22 = fScale;
        m_matOut._33 = fScale;
        return m_matOut;
    }
    public static _matrix Scale(float fScaleX, float fScaleY, float fScaleZ)
    {
        m_matOut.Identity();
        m_matOut._11 = fScaleX;
        m_matOut._22 = fScaleY;
        m_matOut._33 = fScaleZ;
        return m_matOut;
    }
    public static _matrix ScaleX(float fScale)
    {
        m_matOut.Identity();
        m_matOut._11 = fScale;
        return m_matOut;
    }
    public static _matrix ScaleY(float fScale)
    {
        m_matOut.Identity();
        m_matOut._22 = fScale;
        return m_matOut;
    }
    public static _matrix ScaleZ(float fScale)
    {
        m_matOut.Identity();
        m_matOut._33 = fScale;
        return m_matOut;
    }
    public static Vector4 SetShaderVector(Vector3 _vIn, bool bW)
    {
        m_vOutShader.x = _vIn.x;
        m_vOutShader.y = _vIn.y;
        m_vOutShader.z = _vIn.z;
        m_vOutShader.w = bW ? 1 : 0;
        return m_vOutShader;
    }
    public static Matrix4x4 SetShaderMatrix(_matrix matIn)
    {
        m_mat44.m00 = (float)matIn._11; m_mat44.m10 = (float)matIn._12; m_mat44.m20 = (float)matIn._13; m_mat44.m30 = (float)matIn._14;
        m_mat44.m01 = (float)matIn._21; m_mat44.m11 = (float)matIn._22; m_mat44.m21 = (float)matIn._23; m_mat44.m31 = (float)matIn._24;
        m_mat44.m02 = (float)matIn._31; m_mat44.m12 = (float)matIn._32; m_mat44.m22 = (float)matIn._33; m_mat44.m32 = (float)matIn._34;
        m_mat44.m03 = (float)matIn._41; m_mat44.m13 = (float)matIn._42; m_mat44.m23 = (float)matIn._43; m_mat44.m33 = (float)matIn._44;
        return m_mat44;
    }

    public static Matrix4x4 ViewForUnityClient(_matrix matIn)
    {
        m_matOut.ReflectZ();
        m_matOut = matIn * m_matOut;
        m_mat44.m00 = (float)m_matOut._11; m_mat44.m10 = (float)m_matOut._12; m_mat44.m20 = (float)m_matOut._13; m_mat44.m30 = (float)m_matOut._14;
        m_mat44.m01 = (float)m_matOut._21; m_mat44.m11 = (float)m_matOut._22; m_mat44.m21 = (float)m_matOut._23; m_mat44.m31 = (float)m_matOut._24;
        m_mat44.m02 = (float)m_matOut._31; m_mat44.m12 = (float)m_matOut._32; m_mat44.m22 = (float)m_matOut._33; m_mat44.m32 = (float)m_matOut._34;
        m_mat44.m03 = (float)m_matOut._41; m_mat44.m13 = (float)m_matOut._42; m_mat44.m23 = (float)m_matOut._43; m_mat44.m33 = (float)m_matOut._44;
        return m_mat44;
    }
    public static _matrix RotAxis(Vector3 vLook)
    {
        m_matOut.Identity();
        m_vInfo[(int)eInfo.INFO_LOOK] = vLook.normalized;
        m_vInfo[(int)eInfo.INFO_RIGHT] = Vector3.Cross(m_vWorldUp, m_vInfo[(int)eInfo.INFO_LOOK]).normalized;
        m_vInfo[(int)eInfo.INFO_UP] = Vector3.Cross(vLook, m_vInfo[(int)eInfo.INFO_RIGHT]).normalized;

        m_matOut._11 = m_vInfo[(int)eInfo.INFO_RIGHT].x; m_matOut._12 = m_vInfo[(int)eInfo.INFO_RIGHT].y; m_matOut._13 = m_vInfo[(int)eInfo.INFO_RIGHT].z;
        m_matOut._21 = m_vInfo[(int)eInfo.INFO_UP].x; m_matOut._22 = m_vInfo[(int)eInfo.INFO_UP].y; m_matOut._23 = m_vInfo[(int)eInfo.INFO_UP].z;
        m_matOut._31 = m_vInfo[(int)eInfo.INFO_LOOK].x; m_matOut._32 = m_vInfo[(int)eInfo.INFO_LOOK].y; m_matOut._33 = m_vInfo[(int)eInfo.INFO_LOOK].z;
        return m_matOut;
    }
    public static _matrix Rotation(float fAngleX, float fAngleY, float fAngleZ)
    {
        m_matOut.Identity();
        m_matOut = RotX(fAngleX) * RotY(fAngleY) * RotZ(fAngleZ);
        return m_matOut;
    }

    public static _matrix RotX(float fAngleX)
    {
        m_matOut.Identity();
        m_matOut._22 = (double)(Mathf.Cos(Mathf.PI * fAngleX / 180f));
        m_matOut._23 = (double)(Mathf.Sin(Mathf.PI * fAngleX / 180f));
        m_matOut._32 = -(double)(Mathf.Sin(Mathf.PI * fAngleX / 180f));
        m_matOut._33 = (double)(Mathf.Cos(Mathf.PI * fAngleX / 180f));
        return m_matOut;
    }

    public static _matrix RotY(float fAngleY)
    {
        m_matOut.Identity();
        m_matOut._11 = (double)(Mathf.Cos(Mathf.PI * fAngleY / 180f));
        m_matOut._13 = -(double)(Mathf.Sin(Mathf.PI * fAngleY / 180f));
        m_matOut._31 = (double)(Mathf.Sin(Mathf.PI * fAngleY / 180f));
        m_matOut._33 = (double)(Mathf.Cos(Mathf.PI * fAngleY / 180f));
        return m_matOut;
    }
    public static _matrix RotZ(float fAngleZ)
    {
        m_matOut.Identity();
        m_matOut._11 = (double)(Mathf.Cos(Mathf.PI * fAngleZ / 180f));
        m_matOut._12 = (double)(Mathf.Sin(Mathf.PI * fAngleZ / 180f));
        m_matOut._21 = -(double)(Mathf.Sin(Mathf.PI * fAngleZ / 180f));
        m_matOut._22 = (double)(Mathf.Cos(Mathf.PI * fAngleZ / 180f));
        return m_matOut;
    }
    public static _matrix Trans(double dX, double dY, double dZ)
    {
        m_matOut.Identity();
        m_matOut._41 = dX;
        m_matOut._42 = dY;
        m_matOut._43 = dZ;
        return m_matOut;
    }
    public static _matrix Trans(float fX, float fY, float fZ)
    {
        m_matOut.Identity();
        m_matOut._41 = (double)fX;
        m_matOut._42 = (double)fY;
        m_matOut._43 = (double)fZ;
        return m_matOut;
    }
    public static _matrix TransX(float fX)
    {
        m_matOut.Identity();
        m_matOut._41 = (double)fX;
        return m_matOut;
    }
    public static _matrix TransY(float fY)
    {
        m_matOut.Identity();
        m_matOut._42 = (double)fY;
        return m_matOut;
    }
    public static _matrix TransZ(float fZ)
    {
        m_matOut.Identity();
        m_matOut._43 = (double)fZ;
        return m_matOut;
    }
    public static _matrix Trans(Vector3 _vMovement)
    {
        m_matOut.Identity();
        m_matOut._41 = (double)_vMovement.x;
        m_matOut._42 = (double)_vMovement.y;
        m_matOut._43 = (double)_vMovement.z;
        return m_matOut;
    }
    public static _matrix normalize(_matrix matIn)
    {
        m_matOut.Identity();
        m_vOut.x = (float)matIn._11; m_vOut.y = (float)matIn._12; m_vOut.z = (float)matIn._13; m_vOut = m_vOut.normalized;
        m_matOut._11 = (double)m_vOut.x; m_matOut._12 = (double)m_vOut.y; m_matOut._13 = (double)m_vOut.z;
        m_vOut.x = (float)matIn._21; m_vOut.y = (float)matIn._22; m_vOut.z = (float)matIn._23; m_vOut = m_vOut.normalized;
        m_matOut._21 = (double)m_vOut.x; m_matOut._22 = (double)m_vOut.y; m_matOut._23 = (double)m_vOut.z;
        m_vOut.x = (float)matIn._31; m_vOut.y = (float)matIn._32; m_vOut.z = (float)matIn._33; m_vOut = m_vOut.normalized;
        m_matOut._31 = (double)m_vOut.x; m_matOut._32 = (double)m_vOut.y; m_matOut._33 = (double)m_vOut.z;
        m_matOut._41 = matIn._41; m_matOut._42 = matIn._42; m_matOut._43 = matIn._43;
        return m_matOut;
    }
    public static _matrix NonPos(_matrix matIn)
    {
        m_matOut = matIn;
        m_matOut._41 = 0;
        m_matOut._42 = 0;
        m_matOut._43 = 0;
        return m_matOut;
    }
    public static _matrix Inverse(_matrix matIn)
    {
        m_mat44.m00 = (float)matIn._11; m_mat44.m10 = (float)matIn._12; m_mat44.m20 = (float)matIn._13; m_mat44.m30 = (float)matIn._14;
        m_mat44.m01 = (float)matIn._21; m_mat44.m11 = (float)matIn._22; m_mat44.m21 = (float)matIn._23; m_mat44.m31 = (float)matIn._24;
        m_mat44.m02 = (float)matIn._31; m_mat44.m12 = (float)matIn._32; m_mat44.m22 = (float)matIn._33; m_mat44.m32 = (float)matIn._34;
        m_mat44.m03 = (float)matIn._41; m_mat44.m13 = (float)matIn._42; m_mat44.m23 = (float)matIn._43; m_mat44.m33 = (float)matIn._44;
        m_mat44 = m_mat44.inverse;
        m_matOut._11 = (double)(m_mat44.m00); m_matOut._12 = (double)(m_mat44.m10); m_matOut._13 = (double)(m_mat44.m20); m_matOut._14 = (double)(m_mat44.m30);
        m_matOut._21 = (double)(m_mat44.m01); m_matOut._22 = (double)(m_mat44.m11); m_matOut._23 = (double)(m_mat44.m21); m_matOut._24 = (double)(m_mat44.m31);
        m_matOut._31 = (double)(m_mat44.m02); m_matOut._32 = (double)(m_mat44.m12); m_matOut._33 = (double)(m_mat44.m22); m_matOut._34 = (double)(m_mat44.m32);
        m_matOut._41 = (double)(m_mat44.m03); m_matOut._42 = (double)(m_mat44.m13); m_matOut._43 = (double)(m_mat44.m23); m_matOut._44 = (double)(m_mat44.m33);
        return m_matOut;
    }

    public static _matrix Mul(_matrix _mat1, _matrix _mat2)
    {
        m_matOut.Set
            (
                  _mat1._11 * _mat2._11 + _mat1._12 * _mat2._21 + _mat1._13 * _mat2._31 + _mat1._14 * _mat2._41,
                  _mat1._11 * _mat2._12 + _mat1._12 * _mat2._22 + _mat1._13 * _mat2._32 + _mat1._14 * _mat2._42,
                  _mat1._11 * _mat2._13 + _mat1._12 * _mat2._23 + _mat1._13 * _mat2._33 + _mat1._14 * _mat2._43,
                  _mat1._11 * _mat2._14 + _mat1._12 * _mat2._24 + _mat1._13 * _mat2._34 + _mat1._14 * _mat2._44,

                  _mat1._21 * _mat2._11 + _mat1._22 * _mat2._21 + _mat1._23 * _mat2._31 + _mat1._24 * _mat2._41,
                  _mat1._21 * _mat2._12 + _mat1._22 * _mat2._22 + _mat1._23 * _mat2._32 + _mat1._24 * _mat2._42,
                  _mat1._21 * _mat2._13 + _mat1._22 * _mat2._23 + _mat1._23 * _mat2._33 + _mat1._24 * _mat2._43,
                  _mat1._21 * _mat2._14 + _mat1._22 * _mat2._24 + _mat1._23 * _mat2._34 + _mat1._24 * _mat2._44,

                  _mat1._31 * _mat2._11 + _mat1._32 * _mat2._21 + _mat1._33 * _mat2._31 + _mat1._34 * _mat2._41,
                  _mat1._31 * _mat2._12 + _mat1._32 * _mat2._22 + _mat1._33 * _mat2._32 + _mat1._34 * _mat2._42,
                  _mat1._31 * _mat2._13 + _mat1._32 * _mat2._23 + _mat1._33 * _mat2._33 + _mat1._34 * _mat2._43,
                  _mat1._31 * _mat2._14 + _mat1._32 * _mat2._24 + _mat1._33 * _mat2._34 + _mat1._34 * _mat2._44,

                  _mat1._41 * _mat2._11 + _mat1._42 * _mat2._21 + _mat1._43 * _mat2._31 + _mat1._44 * _mat2._41,
                  _mat1._41 * _mat2._12 + _mat1._42 * _mat2._22 + _mat1._43 * _mat2._32 + _mat1._44 * _mat2._42,
                  _mat1._41 * _mat2._13 + _mat1._42 * _mat2._23 + _mat1._43 * _mat2._33 + _mat1._44 * _mat2._43,
                  _mat1._41 * _mat2._14 + _mat1._42 * _mat2._24 + _mat1._43 * _mat2._34 + _mat1._44 * _mat2._44
            );
        return m_matOut;
    }

    public static Vector3 Movement_Vec3(Vector3 vPos, float fX, float fY, float fZ, float fDeltaTime)
    {
        m_vOut = vPos;
        m_vOut.x += fX * fDeltaTime;
        m_vOut.y += fY * fDeltaTime;
        m_vOut.z += fZ * fDeltaTime;
        return m_vOut;
    }

    public static Vector3 Movement_Vec3(Vector3 vPos, double dX, double dY, double dZ, float fDeltaTime)
    {
        m_vOut = vPos;
        m_vOut.x += (float)dX * fDeltaTime;
        m_vOut.y += (float)dY * fDeltaTime;
        m_vOut.z += (float)dZ * fDeltaTime;
        return m_vOut;
    }

    public static Vector3 Get_Scale(float fScale)
    {
        m_vOut.x = fScale;
        m_vOut.y = fScale;
        m_vOut.z = fScale;
        return m_vOut;
    }

    public static Vector3 Get_Pos(_matrix _matIn)
    {
        m_vOut.x = (float)(_matIn._41);
        m_vOut.y = (float)(_matIn._42);
        m_vOut.z = (float)(_matIn._43);
        return m_vOut;
    }
    public static Vector3 Get_Look(_matrix _matIn)
    {
        m_vOut.x = (float)(_matIn._31);
        m_vOut.y = (float)(_matIn._32);
        m_vOut.z = (float)(_matIn._33);
        return m_vOut;
    }
    public static Vector3 Get_LookNY(_matrix _matIn)
    {
        m_vOut.x = (float)(_matIn._31);
        m_vOut.y = 0;
        m_vOut.z = (float)(_matIn._33);
        m_vOut.Normalize();
        return m_vOut;
    }
    public static Vector3 Get_RightNY(_matrix _matIn)
    {
        m_vOut.x = (float)(_matIn._11);
        m_vOut.y = 0;
        m_vOut.z = (float)(_matIn._13);
        m_vOut.Normalize();
        return m_vOut;
    }
    public static Vector3 Get_Right(_matrix _matIn)
    {
        m_vOut.x = (float)(_matIn._11);
        m_vOut.y = (float)(_matIn._12);
        m_vOut.z = (float)(_matIn._13);
        return m_vOut;
    }
    public static Vector3 TransformCoord(Vector3 _vIn, _matrix _matIn)
    {
        m_vOut.x = _vIn.x * (float)(_matIn._11) + _vIn.y * (float)(_matIn._21) + _vIn.z * (float)(_matIn._31) + (float)(_matIn._41);
        m_vOut.y = _vIn.x * (float)(_matIn._12) + _vIn.y * (float)(_matIn._22) + _vIn.z * (float)(_matIn._32) + (float)(_matIn._42);
        m_vOut.z = _vIn.x * (float)(_matIn._13) + _vIn.y * (float)(_matIn._23) + _vIn.z * (float)(_matIn._33) + (float)(_matIn._43);
        return m_vOut;
    }
    public static _matrix Matrix_Identity()
    {
        m_matOut.Identity();
        return m_matOut;
    }

    public static Vector3 Vec3_AdjustX(Vector3 vIn, float fIn)
    {
        m_vOut = vIn;
        m_vOut.x += fIn;
        return m_vOut;
    }
    public static Vector3 Vec3_AdjustY(Vector3 vIn, float fIn)
    {
        m_vOut = vIn;
        m_vOut.y += fIn;
        return m_vOut;
    }
    public static Vector3 Vec3_AdjustZ(Vector3 vIn, float fIn)
    {
        m_vOut = vIn;
        m_vOut.z += fIn;
        return m_vOut;
    }
    public static Vector3 FloatToVector(float fX, float fY, float fZ)
    {
        m_vOut.x = fX;
        m_vOut.y = fY;
        m_vOut.z = fZ;
        return m_vOut;
    }
    public static Vector3 DoubleToVector(double dX, double dY, double dZ)
    {
        m_vOut.x = (float)dX;
        m_vOut.y = (float)dY;
        m_vOut.z = (float)dZ;
        return m_vOut;
    }
    public static Vector3 CatmullRomPoint(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, float fDeltaTime)
    {
        return v1 + (0.5f * (v2 - v0) * fDeltaTime) + 0.5f * (2f * v0 - 5f * v1 + 4f * v2 - v3) * fDeltaTime * fDeltaTime +
                0.5f * (-v0 + 3f * v1 - 3f * v2 + v3) * fDeltaTime * fDeltaTime * fDeltaTime;
    }
    public static float DegreeToRadian(float fIn)
    {
        m_fOut = Mathf.PI * fIn / 180f;
        return m_fOut;
    }
    public static float RadianToDegree(float fIn)
    {
        m_fOut = fIn / Mathf.PI * 180f;
        return m_fOut;
    }
    public static Vector3 RadianToDegree(Vector3 vRot)
    {
        m_vOut.x = vRot.x / Mathf.PI * 180f;
        m_vOut.y = vRot.y / Mathf.PI * 180f;
        m_vOut.z = vRot.z / Mathf.PI * 180f;
        return m_vOut;
    }
    public static float Float_Max() { return 999999f; }
    public static float Float_Zero() { return 0f; }
    public static float Scale_Init() { return 1f; }

    public static bool StateExist(ulong ulState, ulong ulCheck)
    {
        return (ulState & ulCheck) != 0 ? true : false;
    }
    public static float RD_Float()
    {
        return (float)Random.Range(0, 10000) / 10000f;
    }
}
