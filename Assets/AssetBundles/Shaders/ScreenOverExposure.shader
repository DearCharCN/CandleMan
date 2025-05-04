Shader "Custom/ScreenOverExposure"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Threshold ("Threshold", Range(0, 2)) = 1.0
        _Intensity ("Intensity", Range(0, 5)) = 1.0
    }
    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Threshold;
            float _Intensity;

            fixed4 frag(v2f_img i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // return fixed4(1,1,1,1);
                float brightness = dot(col.rgb, float3(0.299, 0.587, 0.114));
                float over = saturate((brightness - _Threshold) * _Intensity);
                col.rgb += over;

                return col;
            }
            ENDCG
        }
    }
}
