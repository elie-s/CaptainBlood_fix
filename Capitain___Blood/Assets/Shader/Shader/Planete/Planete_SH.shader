Shader "Unlit/Planete_SH"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

	_Offset("Colour offset", Float) = 0
		_Color("Color", Color) = (1,1,1,1)

		////////////////////// PIXELISE
				_PixelNumberX("Pixel number along X", float) = 500
				_PixelNumberY("Pixel number along X", float) = 500

				_SaturationAmount("Saturation Amount", Range(0.0, 2.0)) = 1.5
				_BrightnessAmount("Brightness Amount", Range(0.0, 1.0)) = 1.0
				_ContrastAmount("Contrast Amount", Range(0.0,20.0)) = 1.0
	}

		SubShader
	{
		Tags { "RenderType" = "Opaque" }
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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
			// apply fog
			UNITY_APPLY_FOG(i.fogCoord, col);
			return col;
		}
		ENDCG
			}


		Pass {


				CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

				sampler2D _MainTex;
				float _PixelNumberX;
				float _PixelNumberY;

				struct v2f {
					half4 pos : POSITION;
					half2 uv : TEXCOORD0;
				};

				float4 _MainTex_ST;

				v2f vert(appdata_base v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					return o;
				}


				half4 frag(v2f i) : COLOR{

					half ratioX = 1 / _PixelNumberX;
				half ratioY = 1 / _PixelNumberY;
				half2 uv = half2((int)(i.uv.x / ratioX) * ratioX, (int)(i.uv.y / ratioY) * ratioY);


				return tex2D(_MainTex, uv);
				}

					ENDCG
			}

			Pass
				{
					CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

						uniform sampler2D _MainTex;
					uniform float _SaturationAmount;
					uniform float _BrightnessAmount;
					uniform float _ContrastAmount;
					fixed4 _Color;


					float3 ContrastSaturationBrightness(float3 color, float brt, float sat, float con)
					{
						//RGB Color Channels
						float AvgLumR = 0.5;
						float AvgLumG = 0.5;
						float AvgLumB = 0.5;

						//Luminace Coefficients for brightness of image
						float3 LuminaceCoeff = float3(0.2125,0.7154,0.0721);

						//Brigntess calculations
						float3 AvgLumin = float3(AvgLumR,AvgLumG,AvgLumB);
						float3 brtColor = color * brt;
						float intensityf = dot(brtColor, LuminaceCoeff);
						float3 intensity = float3(intensityf, intensityf, intensityf);

						//Saturation calculation
						float3 satColor = lerp(intensity, brtColor, sat);

						//Contrast calculations
						float3 conColor = lerp(AvgLumin, satColor, con);

						return conColor;

					}


					float4 frag(v2f_img i) : COLOR
					{
						float4 renderTex = tex2D(_MainTex, i.uv);


						renderTex.rgb = ContrastSaturationBrightness(renderTex.rgb, _BrightnessAmount, _SaturationAmount, _ContrastAmount);

						return fixed4(
							renderTex.r - _Color.r*(2 * renderTex.r - renderTex.g - renderTex.b),
							renderTex.g - _Color.g*(2 * renderTex.g - renderTex.r - renderTex.b),
							renderTex.b - _Color.b*(2 * renderTex.b - renderTex.r - renderTex.g), renderTex.a);
					}




						ENDCG
					}


	}
		FallBack "Diffuse"


	}



