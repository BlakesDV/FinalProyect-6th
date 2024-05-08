Shader "Custom/Neon_Shader"
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
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
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
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            //o.Emission = _EmissionColor.rgb * _EmissionIntensity;
           
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
