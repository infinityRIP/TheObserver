Shader "Unlit/VHSTape"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Distortion("Distortion", Range(0, 1)) = 0.1
        _Bleed("Bleed", Range(0, 1)) = 0.2
        _Scanlines("Scanlines", Range(0, 1)) = 0.1
        _Noise("Noise", Range(0, 1)) = 0.1
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Distortion;
            float _Bleed;
            float _Scanlines;
            float _Noise;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            // Random number generator
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Screen Distortion
                float2 distortedUV = i.uv;
                distortedUV.x += sin((i.uv.y + _Time.y) * 10.0) * _Distortion * 0.01;
                distortedUV.y += cos((i.uv.x + _Time.y) * 10.0) * _Distortion * 0.01;

                // Color Bleeding
                float bleed = _Bleed * 0.01;
                float r = tex2D(_MainTex, float2(distortedUV.x + bleed, distortedUV.y)).r;
                float g = tex2D(_MainTex, distortedUV).g;
                float b = tex2D(_MainTex, float2(distortedUV.x - bleed, distortedUV.y)).b;
                fixed4 col = fixed4(r, g, b, 1.0);

                // Scanlines
                float scanline = sin((i.uv.y * 500.0) + _Time.y) * _Scanlines;
                col.rgb -= scanline;

                // Noise
                float noise = (rand(i.uv * _Time.y) - 0.5) * _Noise;
                col.rgb += noise;


                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
