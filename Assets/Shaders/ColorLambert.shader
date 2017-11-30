Shader "Custom/ColorLambert"
{
	Properties 
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
    }
	SubShader 
	{
		Tags { "RenderType" = "Opaque" }

		

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed3 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed3 c = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
			o.Albedo = c;
		}
		ENDCG
	}

	Fallback "Diffuse"
}
