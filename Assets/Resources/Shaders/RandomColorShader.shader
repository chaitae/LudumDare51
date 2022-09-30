Shader "VertexInputSimple" {
    Properties{
        
        _MainTex ("Texture", 2D) = "white" {}
        _MyColor ("Some Color", Color) = (1,1,1,1) 

    }
    SubShader {

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            fixed4 _MyColor;
            struct v2f {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
            };

            
            float random (float2 uv)
            {
                return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
            }
            
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.vertex.x;
                // if(o.pos.x < 50)
                // {
                //     o.color.rgb = _MyColor;

                // }
                // o.color.rgb = random(v.vertex.xy);
                // o.color.g = random(v.vertex.xy);
                //  o.color.w = 0;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target { return i.color; }
            ENDCG
        }
    } 
}