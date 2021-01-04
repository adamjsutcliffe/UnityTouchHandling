Shader "Custom/Stencil/Diffuse NotEqualOne"
{

Properties
{
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Color (RGB) Alpha (A)", 2D) = "white" {}
}

SubShader
{
	Tags { "RenderType"="Transparent" "Queue"="Transparent+100" }
	LOD 200

	Stencil
	{
		Ref 1
		Comp notequal
		Pass keep
	}

	CGPROGRAM
	#pragma surface surf Lambert alpha

	sampler2D _MainTex;
	fixed4 _Color;

	struct Input
	{
		float2 uv_MainTex;
	};

	void surf (Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	
	ENDCG
}

Fallback "VertexLit"
}
