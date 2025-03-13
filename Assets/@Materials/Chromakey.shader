Shader "Custom/ChromaKey" {
    Properties{
      _MainTex("Base (RGB)", 2D) = "white" {}
      _AlphaValue("Alpha Value", Range(0.0,1.0)) = 1.0
    }
        SubShader{
          Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
          LOD 800
          CGPROGRAM
          #pragma surface surf Lambert alpha
          sampler2D _MainTex;
          float _AlphaValue;
          struct Input {
           float2 uv_MainTex;
          };
          void surf(Input IN, inout SurfaceOutput o) {
           half4 c = tex2D(_MainTex, IN.uv_MainTex);
           half alpha_pass = saturate(distance(c.rgb, half3 (0.0, 1.0, 0.0)) / 1); // Amount of not matching the color
           c.a *= alpha_pass; // Adjustment of alpha
           half alpha_pass_limit = max(0.1, alpha_pass); // Prevents a division by zero
           c.rgb = (c.rgb - (1 - alpha_pass_limit) * half3 (0.0, 1.0, 0.0)) / alpha_pass_limit; // Adjustment of color
           o.Emission = c.rgb;
           // Green screen level - leaves minor green glow
           if (c.g == 1) {
               o.Alpha = 0.0;
           }
        else {
           o.Alpha = c.a;
       }
      }
      ENDCG
      }
          FallBack "Diffuse"
}