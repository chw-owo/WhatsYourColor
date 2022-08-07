// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Shader_Particle"
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
        Tags { "RenderType"="Transparent" "Opaque" = "Transparent" }

        CGPROGRAM
        #pragma surface surf Lambert noambient vertex:vert alpha:fade

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
        float _LensRadius;


        void vert(inout appdata_full v) {

            float4 vWorld = mul(unity_ObjectToWorld, v.vertex);

            float4 vMousePos = _MousePos;
            float4 vDir = 0;
            float fDis = distance(vWorld, vMousePos);

            // trail과 대가리의 분리
            if (fDis < 20)
            {
                vWorld.z = 0;
                vMousePos.z = 0;
                vDir = normalize(vWorld - vMousePos);

                vWorld = vMousePos + vDir * fDis * 1.2;
                vWorld.w = 1;

                fDis = distance(vWorld, vMousePos);
                if (fDis > 12 && fDis < 27)
                {
                    vWorld = vMousePos + vDir * (12 + 8 * (abs(fDis - 12) / 15));
                }

              
                vWorld.w = 1;
            }


            /*if (_Noise == 1)
            {
                vWorld.x = vWorld.x + sin(vWorld.y * 7 + _Time.y * 200) * 1; //폭, 속도, 높낮이
                
            }*/

            v.vertex = mul(unity_WorldToObject, vWorld);

        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex); // * _Color;
            fixed4 e = tex2D(_EffectMap,  float2(frac(IN.worldPos.x * 0.05 + _Time.y * 0.2), frac(IN.worldPos.y * 0.05 + _Time.y* 0.2))); // * _Color;

            e = (e.r + e.g + e.b) * 0.33;
            
            //o.Albedo = c.rgb;
            o.Emission = float3(_Red, _Green, _Blue); // +e * 0.1; //0.1; 하얀색은 0.3

            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, 3)* _Opacity; // +0.05; 하얀색, 가까이에선 0.2
            o.Alpha = rim;

            //=============================================================================================
            float3 vWorldPos = IN.worldPos;
            float fDis = distance(vWorldPos, float3(_MousePos.x, _MousePos.y, _MousePos.z));

            float fDisMid = (_DeltaTimeF + _DeltaTimeB) * 0.5;
            float fRadius = (_DeltaTimeF - _DeltaTimeB) * 0.5;
            
            if (fDis < _DeltaTimeF && fDis > _DeltaTimeB)
            {
                 o.Emission = o.Emission + (float3(1, 1, 1) * pow(1 - abs(fDis - fDisMid) / fRadius, 3));
                 //o.Alpha = o.Alpha + c.a * pow(1 - abs(fDis - fDisMid) / fRadius, 3);

            }

            fDisMid = ((_DeltaTimeF + 30) + (_DeltaTimeB + 30)) * 0.5;
            fRadius = ((_DeltaTimeF + 30) - (_DeltaTimeB + 30)) * 0.5;

            if (fDis < _DeltaTimeF + 30 && fDis > _DeltaTimeB + 30)
            {
                o.Emission = o.Emission + (float3(1, 1, 1) * pow(1 - abs(fDis - fDisMid) / fRadius, 3));
                //o.Alpha = o.Alpha + c.a * pow(1 - abs(fDis - fDisMid) / fRadius, 3);
                
            }

            fDisMid = (30 + (-30)) * 0.5;
            fRadius = (30 - (-30)) * 0.5;

            if (fDis < 30 && fDis > -30)
            {
                o.Emission = o.Emission + (float3(1, 1, 1) * pow(1 - abs(fDis - fDisMid) / fRadius, 3)); 
                o.Alpha = o.Alpha + c.a * pow(1 - abs(fDis - fDisMid) / fRadius, 3) * 2;
            }

        }
        ENDCG
    }
    FallBack "Diffuse"

        // 0,0,0 지점에 확대됨
        
}
