/*Shader "Unlit/Shader_Wireframe"
{
    Properties
    {
        _texMain("Texture", 2D) = "white" {}
        [PowerSlider(3.0)]
        _widthWireframe("Wireframe width", Range(0., 0.34)) = 0.05
        _colorFront("Front color", color) = (1.0, 1.0, 1.0, 1.0)
        _colorBack("Back color", color) = (1.0, 1.0, 1.0, 1.0)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }


            Pass
            {
                //Cull Front
                CGPROGRAM
                #pragma vertex _vtxShader
                #pragma geometry _geoShader
                #pragma fragment _pxShader
                #pragma multi_compile_instancing
                #include "UnityCG.cginc"
                #include "UnityInstancing.cginc"

                struct VTX_OUT1 {
                    float4 vPos : SV_POSITION;
                    float4 vNormal : TEXCOORD0;
                };

                struct VTX_OUT2 {
                    float4 vPos : SV_POSITION;
                    float4 vNormal : TEXCOORD0;
                    float3 vBary : TEXCOORD1;
                };

                float _widthWireframe;
                fixed4 _colorBack;
                uniform float4x4 _matView;
                uniform float4x4 _matWorld;
                float4 _vCamDir;
                float4 _vLightDir;

                //πˆ≈ÿΩ∫ Ω¶¿Ã¥ı
                VTX_OUT1 _vtxShader(float4 vVertex : POSITION, float3 vLocalNormal : NORMAL) {
                    VTX_OUT1 _tOut;
                    _tOut.vPos = UnityObjectToClipPos(vVertex);
                    _tOut.vNormal = mul(_matView, mul(_matWorld, float4(vLocalNormal.xyz,0)));
                    return _tOut;
                }

                //¡ˆø¿∏ﬁ∆Æ∏Æ Ω¶¿Ã¥ı
                [maxvertexcount(3)]
                void _geoShader(triangle VTX_OUT1 _vtxIn[3], inout TriangleStream<VTX_OUT2> _vtxOut) {
                    VTX_OUT2 _tOut;
                    _tOut.vPos = _vtxIn[0].vPos;
                    _tOut.vNormal = _vtxIn[0].vNormal;
                    _tOut.vBary = float3(1., 0., 0.);
                    _vtxOut.Append(_tOut);
                    _tOut.vPos = _vtxIn[1].vPos;
                    _tOut.vNormal = _vtxIn[1].vNormal;
                    _tOut.vBary = float3(0., 0., 1.);
                    _vtxOut.Append(_tOut);
                    _tOut.vPos = _vtxIn[2].vPos;
                    _tOut.vNormal = _vtxIn[2].vNormal;
                    _tOut.vBary = float3(0., 1., 0.);
                    _vtxOut.Append(_tOut);
                }

                //«»ºø Ω¶¿Ã¥ı
                float4 _pxShader(VTX_OUT2 _tIn) : SV_Target{
                    if (!any(bool3(_tIn.vBary.x < _widthWireframe, _tIn.vBary.y < _widthWireframe, _tIn.vBary.z < _widthWireframe)))
                        discard;

                    float4 vColor = _colorBack;

                    vColor = vColor * (1 - saturate(dot(_tIn.vNormal, _vLightDir))) * 0.8 + vColor * 0.2;
                    vColor.a = 1;

                    return vColor;
                }

                ENDCG
            }
        }
}
*/