Shader "Custom/CubeTextShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _CubeText ("Cube Text", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
    
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        
        sampler2D _MainTex;
        sampler2D _CubeText;
        
        struct Input
        {
            float2 uv_MainTex;
        };
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Get text color from _CubeText texture
            fixed4 textColor = tex2D(_CubeText, IN.uv_MainTex);
            // Apply text color to surface
            o.Albedo = textColor.rgb;
        }
        ENDCG
    }
    
    FallBack "Diffuse"
}
