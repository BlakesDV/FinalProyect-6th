Shader "Custom/Metallic_Shader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _Metallic("Metallic", Range(0,1)) = 1
        _Glossiness("Smoothness", Range(0,1)) = 0.5
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        float4 _Color;
        float _Metallic;
        float _Glossiness;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Normal = normalize(o.Normal);
            o.Alpha = max(0, dot(IN.viewDir, o.Normal));
        }
        ENDCG
    }
        FallBack "Diffuse"
}
