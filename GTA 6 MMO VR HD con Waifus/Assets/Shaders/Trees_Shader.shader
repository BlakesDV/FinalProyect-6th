Shader "Custom/Trees_Shader"
{
    Properties
    {
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
        _Color3 ("Color3", Color) = (1,1,1,1)
        _RimColor ("RimColor", Color) = (0,0.5,0.5,0)
        _Range ("Range", Range(0,10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
    
        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float3 worldPos;
            float3 viewDir;
        };

        fixed4 _Color1;
        fixed4 _Color2;
        fixed4 _Color3;
        float4 _RimColor;
        half _Range;

        void surf (Input IN, inout SurfaceOutput o)
        {
           half rim = 1 - (dot(normalize(IN.viewDir), o.Normal));
           o.Emission = frac(IN.worldPos.y * 10 * 0.5) > 0.6 ? _Color1 * rim :
                        frac(IN.worldPos.y * 10 * 0.5) > 0.4 ? _Color2 * rim :
                        frac(IN.worldPos.y * 10 * 0.5) > 0.2 ? _Color3 * rim : 0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
