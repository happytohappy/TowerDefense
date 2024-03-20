Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _CT ("Time", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        CGPROGRAM

        #pragma surface surf Standard alpha:blend
        #pragma target 3.0

        sampler2D _MainTex;
        float _CT;

        struct Input
        {
            float2 uv_MainTex;
            
        };

        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 MainTex = tex2D(_MainTex, IN.uv_MainTex + _Time.y*_CT);
            o.Emission = MainTex.rgb * _Color.rgb;
            o.Alpha = MainTex.a * _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
