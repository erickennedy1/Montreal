Shader "Custom/WhiskyRadialGradientShader"
{
    Properties
    {
        _CenterColor ("Center Color", Color) = (1,1,1,1)
        _EdgeColor ("Edge Color", Color) = (1,1,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _CenterColor;
            fixed4 _EdgeColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate the distance from the center
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);

                // Interpolate between the edge and center color based on the distance
                return lerp(_CenterColor, _EdgeColor, dist * 2);
            }
            ENDCG
        }
    }
}
