Shader "Custom/Neon_Shader"
{
   Properties
    {
       MyColor("Color", Color) = (1,1,1,1)
       MyEmission("Emission", Color) = (1,1,1,1)
       MyNormal("Normal", Color) = (1,1,1,1)

    }
        SubShader
    {

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input {
            float2 uvMainTex;
        };
        fixed4 MyColor;
        fixed4 MyEmission;
        fixed4 MyNormal;

        void surf(Input In, inout SurfaceOutput o)
        {
            o.Albedo = MyColor.rgb;
            o.Emission = MyEmission.xyz;
            o.Normal = MyNormal.xyz;
        }


        ENDCG
    }
        FallBack "Diffuse"
}
