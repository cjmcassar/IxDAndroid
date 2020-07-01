Shader "Pixyz/Blurred UI" {

	Properties
	{
      _MainColor("Main Color", COLOR) = (1,1,1,1)
		_Alpha("Alpha", Range(0, 1)) = 0.5

		_KernelSize("Kernel Size", Range(5.0, 20.0)) = 5.0
		_Radius("Radius", Range(1.0, 50.0)) = 1.0

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		GrabPass {Tags{"LightMode" = "Always"}}

		Pass
		{
			Name "Default"

			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 

			#include "UnityCG.cginc"

			float4 _MainColor;
			sampler2D _GrabTexture;
			float4 _GrabTexture_TexelSize;
			float _Alpha;
			float _Radius;
			float _KernelSize;

			struct appdata
			{
				float4 vertex : POSITION; 
				float4 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 uvgrab : COLOR;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata  v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);  // Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
				o.uv = v.texcoord.xy;

#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
#else
				float scale = 1.0;
#endif
				o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5;
				o.uvgrab.zw = o.vertex.zw;
				return o;
			}

			float4 frag(v2f  i) : COLOR
			{
				half4 sum = half4(0, 0, 0, 0);

				#define GRABPIXEL(kernelx, kernely) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely, i.uvgrab.z, i.uvgrab.w)))
				
				int measurements = 0;

				for (float p = _KernelSize; p < _Radius; p += _KernelSize) {
					float d = 0.7071068 * p;
					sum += GRABPIXEL(p, 0);
					sum += GRABPIXEL(0, p);
					sum += GRABPIXEL(-p, 0);
					sum += GRABPIXEL(0, -p);
					sum += GRABPIXEL(d, d);
					sum += GRABPIXEL(d, -d);
					sum += GRABPIXEL(-d, d);
					sum += GRABPIXEL(-d, -d);
					measurements += 8;
				}
				
				return (1 - _Alpha) * sum / measurements + _Alpha * _MainColor;
			}

			ENDCG
		}
	}
}