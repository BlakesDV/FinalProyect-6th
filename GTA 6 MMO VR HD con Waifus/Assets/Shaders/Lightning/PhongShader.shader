Shader "Custom/PhongShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _SpecularColor("Specular Color", Color) = (1,1,1,1)
        _Spec("Specular", Range(0,1)) = 0.5
        _Gloss("Gloss", Range(0,1)) = 0.5
        _Bump("Bump Txt", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "Queue"="Geometry" }
        
        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_Bump;
        };

        sampler2D _Bump;
        float4 _Color;
        half _Spec;
        fixed _Gloss;

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _Color.rgb;
            o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
            o.Specular = _Spec;
            o.Gloss = _Gloss;
        }
        ENDCG
    }
    Fallback "Diffuse"
}
