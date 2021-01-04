Shader "Unlit/DepthShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		Blend One OneMinusSrcAlpha
		ZWrite Off
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
            {
            	v2f o;
            	o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
            	return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float4 px = tex2D(_MainTex, i.uv);
				px.a =  px.a;
                return fixed4(px.a, px.a, px.a, px.a);
            }
			ENDCG
		}
	}
}
