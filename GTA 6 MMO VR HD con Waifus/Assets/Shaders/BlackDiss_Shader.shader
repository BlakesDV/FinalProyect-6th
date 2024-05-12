Shader "Custom/BlackDiss_Shader"
{
     Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _EmissionColor("EmissionColor", Color) = (1,1,1,1)
        _EmissionIntensity ("EmissionIntensity", Range(0,10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0l

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _EmissionColor;
        half _EmissionIntensity;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
           
            //con el producto punto quitar el color negro
            if (dot(c.rgb, float3(1, 1, 1)) > 0) {
                o.Albedo = c.rgb;
                o.Emission = _EmissionColor.rgb * _EmissionIntensity;
            }
            else {
                o.Albedo = c.rgb;
                o.Emission = float3(0, 0, 0);
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
