Shader "Lava Flowing Shader/Unlit/Simple" 
{
    Properties {
        _MainTex ("Main Texture (RGBA)", 2D) = "white" {}
        _LavaTex ("Lava Texture (RGB)", 2D) = "white" {}
    }

    SubShader {
        Tags { "RenderType" = "Opaque" }
        Lighting Off

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing // Support for Single Pass Instanced Rendering

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _LavaTex;

            // UV Transform properties for tiling/offset
            fixed4 _MainTex_ST;
            fixed4 _LavaTex_ST;

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID // Instance ID input for VR
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO // Stereo output for VR
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); // Setup for Single Pass Instanced Rendering
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); // Initialize stereo output

                o.vertex = UnityObjectToClipPos(v.vertex); // Transform to clip space
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex); // Transform UV for _MainTex
                o.texcoord1 = TRANSFORM_TEX(v.texcoord, _LavaTex); // Transform UV for _LavaTex
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 tex1 = tex2D(_MainTex, i.texcoord); // Sample main texture
                float4 tex2 = tex2D(_LavaTex, i.texcoord1); // Sample lava texture

                // Blend textures based on alpha
                float4 result = lerp(tex2, tex1, tex1.a);
                return result;
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
