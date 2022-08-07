Shader "Custom/Back_tmp"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _EffectMap("EffectMap (RGB)", 2D) = "black" {}
        _NoiseTex("_NoiseTex (RGB)", 2D) = "black" {}

        _Red("R", Range(0,1)) = 0
        _Green("G", Range(0,1)) = 0
        _Blue("B", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

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


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);//* _Color;
            fixed4 e = tex2D(_EffectMap, float2(frac(IN.worldPos.x * 0.05 + _Time.y * 0.1), frac(IN.worldPos.y * 0.05 + _Time.y * 0.1))); // * _Color;

            e = (e.r + e.g + e.b) * 0.33;
            o.Emission = float3(_Red, _Green, _Blue)+ e * 0.05; //0.1; ÇÏ¾á»öÀº 0.3

            //float rim = saturate(dot(o.Normal, IN.viewDir));
            //rim = pow(1 - rim, 3) * _Opacity;
            o.Alpha = c.a; // rim;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
