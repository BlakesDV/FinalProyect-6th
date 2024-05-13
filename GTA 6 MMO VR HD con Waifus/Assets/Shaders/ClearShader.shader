Shader "Custom/ClearShader"
{
      Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Glossiness ("Glossiness", Range(0,1)) = 0.5
        _Refraction ("Refraction", Range(0,1)) = 0.5
        _Alpha ("Alpha", Range(0,1)) = 0.5
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        half _Glossiness;
        half _Refraction;
        half _Alpha;

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Albedo
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;

            // Smoothness
            o.Smoothness = _Glossiness;

            // Refraction
            o.Alpha = _Alpha * _Refraction;
        }
        ENDCG
    }
    FallBack "Transparent/Diffuse"
}
