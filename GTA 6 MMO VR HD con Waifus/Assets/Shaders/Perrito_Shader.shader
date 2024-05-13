Shader "Custom/Perrito_Shader"
{
        Properties
        {
            _MainTex ("Main Texture", 2D) = "white" {}
            _NormalMap ("Normal Map", 2D) = "bump" {}
        }
 
        SubShader
        {
            Tags { "RenderType"="Opaque" }
            LOD 200
 
            CGPROGRAM
            #pragma surface surf Lambert
        
            sampler2D _MainTex;
            sampler2D _NormalMap;
        
            struct Input
            {
                float2 uv_MainTex;
            };
 
            void surf (Input IN, inout SurfaceOutput o)
            {
                fixed4 mainTex = tex2D (_MainTex, IN.uv_MainTex);
                fixed3 normalMap = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
                o.Albedo = mainTex.rgb; 
                o.Alpha = mainTex.a;
            }
            ENDCG
        }
    FallBack "Diffuse"
}
