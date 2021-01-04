Shader "Unlit/MaskingShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OurCoolDepthTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		Blend One OneMinusSrcAlpha
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
			sampler2D _OurCoolDepthTex;
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
			    float4 vertex = i.vertex;
				float2 fragmentScreenCoordinates = float2(vertex.x / _ScreenParams.x, vertex.y / _ScreenParams.y);
                float4 sceneDepthSample = tex2D(_OurCoolDepthTex, fragmentScreenCoordinates);
                //bool culled = (sceneDepthSample < vertex.z) && abs(sceneDepthSample - vertex.z) > 0.05;
                //return (culled) ? fixed4(0.0, 0.0, 0.0, 0.0) : fixed4(0.0, 0.0, 0.0, 1.0);
				sceneDepthSample.a = sceneDepthSample.a;// sceneDepthSample.a * abs(sceneDepthSample - vertex.z)
				return fixed4(sceneDepthSample.a, sceneDepthSample.a, sceneDepthSample.a, sceneDepthSample.a);
			}
			ENDCG
		}
	}
}
