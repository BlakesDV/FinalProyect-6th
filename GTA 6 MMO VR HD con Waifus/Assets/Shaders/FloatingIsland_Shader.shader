Shader "Custom/FloatingIsland_Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _Color ("Color", Color) = (1,1,1,1)
        _SpecularColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
        _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0
 
        sampler2D _MainTex;
        sampler2D _NormalMap;
        fixed4 _Color;
        fixed4 _SpecularColor;
        fixed _Shininess;
 
        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };
 
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
 
            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
 
            o.Specular = _SpecularColor.rgb;
            o.Smoothness = _Shininess;
        }
        ENDCG
    }
 
    FallBack "Diffuse"
}
