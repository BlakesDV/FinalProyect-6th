Shader "Custom/Grass_Shader"
{
   Properties {
        _Color1 ("Color 1", Color) = (1,0,0,1)
        _Color2 ("Color 2", Color) = (0,1,0,1)
        _Color3 ("Color 3", Color) = (0,0,1,1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard

        // Declaración de variables
        struct Input {
            float3 worldNormal;
        };
        fixed4 _Color1;
        fixed4 _Color2;
        fixed4 _Color3;

        void surf (Input IN, inout SurfaceOutputStandard o) {
            float lightAngle = dot(normalize(_WorldSpaceLightPos0.xyz), IN.worldNormal);
            fixed4 finalColor;
            if (lightAngle > 0.66)
                finalColor = _Color1;
            else if (lightAngle > 0.33)
                finalColor = _Color2;
            else
                finalColor = _Color3;
            o.Albedo = finalColor.rgb;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}
