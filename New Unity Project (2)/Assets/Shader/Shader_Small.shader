Shader "Custom/Shader_Small"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Opaque" = "Transparent" }
        LOD 200

        CGPROGRAM


        #pragma surface surf Lambert noambient alpha:fade

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            //float3 worldPos;
        };

        fixed4 _Color;


        UNITY_INSTANCING_BUFFER_START(Props)

        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {

            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Emission = c.rgb; 

            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, 3) * 0.9; // +0.05; 하얀색, 가까이에선 0.2
            o.Alpha = rim;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
//랜덤, 간격 줄이고, 크기 줄이고
