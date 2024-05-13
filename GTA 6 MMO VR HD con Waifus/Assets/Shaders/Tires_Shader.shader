Shader "Custom/Tires_Shader"
{
    Properties
    {
         _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
        _Color3 ("Color3", Color) = (1,1,1,1)
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
        half _Range;

        void surf (Input IN, inout SurfaceOutput o)
        {
           half rim = 1 - (dot(normalize(IN.viewDir), o.Normal));

           // Calculate vertical lines
           float linePos = frac(IN.worldPos.x * 10 * 0.5);
           o.Emission = linePos > 0.6 ? _Color1 * rim :
                        linePos > 0.4 ? _Color2 * rim :
                        linePos > 0.2 ? _Color3 * rim : 0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
