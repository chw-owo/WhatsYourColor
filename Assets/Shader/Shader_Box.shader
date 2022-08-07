
Shader "Custom/Shader_Box"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _EffectMap("EffectMap (RGB)", 2D) = "black" {}
        _NoiseTex("_NoiseTex (RGB)", 2D) = "black" {}

        _Red("R", Range(0,1)) = 0.5
        _Green("G", Range(0,1)) = 0.5
        _Blue("B", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Opaque" = "Transparent" }

        CGPROGRAM
        #pragma target 4.0
        #pragma surface surf Lambert noambient vertex:vert alpha:fade ///vertex:vert 

        sampler2D _MainTex;
        sampler2D _EffectMap;
        sampler2D _NoiseTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float3 worldPos;
        };

        fixed4 _Color;
        float _Red;
        float _Green;
        float _Blue;
        float _DeltaTimeF;
        float _DeltaTimeB;
        float4 _WorldPos;
        float4 _MousePos;
        float _Noise;
        float _Opacity;

        void vert(inout appdata_full v) {


            float4 vWorld = mul(unity_ObjectToWorld, v.vertex);
            vWorld.y = vWorld.y + sin(vWorld.x * 1 + _Time.y * 1.5) * 1; //Æø, ¼Óµµ, ³ô³·ÀÌ
            v.vertex = mul(unity_WorldToObject, vWorld);

        }




        void surf(Input IN, inout SurfaceOutput o)
        {
            
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);//* _Color;
            fixed4 e = tex2D(_EffectMap, float2(frac(IN.worldPos.x * 0.05 + _Time.y * 0.2), frac(IN.worldPos.y * 0.05 + _Time.y * 0.2))); // * _Color;

            e = (e.r + e.g + e.b) * 0.33;
            o.Emission = float3(_Red, _Green, _Blue) +e * 0.3; //0.1; ÇÏ¾á»öÀº 0.3

            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, 3) * _Opacity; 
            o.Alpha = rim;

            //===================================================================================

            /*float3 vWorldPos = IN.worldPos;
            float fDis = distance(vWorldPos, float3(_MousePos.x, _MousePos.y, _MousePos.z));

            float fDisMid = (30 + (-30)) * 0.5;
            float fRadius = (30 - (-30)) * 0.5;

            if (fDis < 30 && fDis > -30)
            {
                o.Emission = o.Emission - (float3(1, 1, 1) * pow(1 - abs(fDis - fDisMid) / fRadius, 3))*3;
                //o.Alpha = o.Alpha + c.a * pow(1 - abs(fDis - fDisMid) / fRadius, 3);
            }*/

        }
        ENDCG
    }
        FallBack "Diffuse"
}
