Shader "Custom/ColorLambertSpecularEmmisive"
{
	Properties 
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Specular("Specular", Range(0,1)) = 0
		_EmissionColor("EmissionColor", Color) = (0,0,0,0)
    }
	SubShader 
	{
		Tags { "RenderType" = "Opaque" }

		ColorMask RGB

		CGPROGRAM
		#pragma surface surf SimpleSpecular

		sampler2D _MainTex;
		fixed3 _Color;
		half _Specular;
		fixed3 _EmissionColor;

		 #pragma surface surf SimpleSpecular

		half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
		{
			half3 h = normalize (lightDir + viewDir);

			half diff = max (0, dot (s.Normal, lightDir));

			float nh = max (0, dot (s.Normal, h));
			float spec = pow (nh, s.Specular * 48);

			half4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
			c.rgb = c.rgb + s.Emission;
			c.a = s.Alpha;
			return c;
		}

		struct Input 
		{
			float2 uv_MainTex;
			float3 normal;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed3 c = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
			o.Albedo = c;
			//o.Normal = IN.normal;
			o.Specular = _Specular;
			o.Emission = _EmissionColor;
		}
		ENDCG
	}

	Fallback "Diffuse"
}
