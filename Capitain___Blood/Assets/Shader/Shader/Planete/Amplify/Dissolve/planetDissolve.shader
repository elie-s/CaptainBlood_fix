// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "planetDissolve"
{
	Properties
	{
		_Noise_Texture("Noise_Texture", 2D) = "white" {}
		_MainTex("_MainTex", 2D) = "white" {}
		_Dissolve("Dissolve", Float) = 0
		_Bordure("Bordure", Float) = 0
		_Color_Dissolve("Color_Dissolve", Color) = (1,0,0,1)
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_HUEValue("HUEValue", Float) = 0.33
		_Color("Color", Color) = (0.8773585,0.3269402,0.3269402,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _HUEValue;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _Color;
		uniform sampler2D _Noise_Texture;
		uniform float4 _Noise_Texture_ST;
		uniform float _Dissolve;
		uniform float _Bordure;
		uniform float4 _Color_Dissolve;
		uniform float _Cutoff = 0.5;


		float3 HSVToRGB( float3 c )
		{
			float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
			float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
			return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode18 = tex2D( _MainTex, uv_MainTex );
			float3 hsvTorgb3_g1 = HSVToRGB( float3(( _HUEValue + tex2DNode18 ).r,1.0,1.0) );
			float3 desaturateInitialColor45 = tex2DNode18.rgb;
			float desaturateDot45 = dot( desaturateInitialColor45, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar45 = lerp( desaturateInitialColor45, desaturateDot45.xxx, 1.0 );
			float3 temp_cast_2 = (0.15).xxx;
			float3 temp_output_48_0 = step( desaturateVar45 , temp_cast_2 );
			float3 temp_cast_4 = (0.15).xxx;
			o.Albedo = ( float4( ( hsvTorgb3_g1 + ( 1.0 - temp_output_48_0 ) ) , 0.0 ) * ( ( tex2DNode18 + float4( temp_output_48_0 , 0.0 ) ) * _Color ) ).rgb;
			float2 uv_Noise_Texture = i.uv_texcoord * _Noise_Texture_ST.xy + _Noise_Texture_ST.zw;
			float4 tex2DNode10 = tex2D( _Noise_Texture, uv_Noise_Texture );
			float4 temp_cast_7 = (( _Dissolve + _Bordure )).xxxx;
			o.Emission = ( step( tex2DNode10 , temp_cast_7 ) * _Color_Dissolve ).rgb;
			o.Alpha = 1;
			clip( ( tex2DNode10 + (-0.6 + (( 1.0 - _Dissolve ) - 0.0) * (0.6 - -0.6) / (1.0 - 0.0)) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15900
7;7;1906;1044;770.9866;794.5875;1.054409;True;True
Node;AmplifyShaderEditor.SamplerNode;18;-779.5802,-583.9321;Float;True;Property;_MainTex;_MainTex;1;0;Create;False;0;0;False;0;ebf2e2c87a81a024ebe16f65710cd4b8;4abb32508a72daf40b0e2d9c645ac10f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;46;-684.9142,-315.9639;Float;False;Constant;_Float2;Float 2;7;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-304.9142,-199.9639;Float;False;Constant;_Float3;Float 3;7;0;Create;True;0;0;False;0;0.15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-255.9142,-877.9639;Float;False;Property;_HUEValue;HUEValue;6;0;Create;True;0;0;False;0;0.33;-0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;45;-397.9142,-412.9639;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1484.563,439.7334;Float;False;Property;_Dissolve;Dissolve;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-816.862,179.2115;Float;False;Property;_Bordure;Bordure;3;0;Create;False;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-180.9142,-631.9639;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;48;-108.4508,-270.1007;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;42;69.74239,-652.1569;Float;False;Simple HUE;-1;;1;32abb5f0db087604486c2db83a2e817a;0;1;1;COLOR;0,0,0,0;False;4;FLOAT3;6;FLOAT;7;FLOAT;5;FLOAT;8
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-794.0711,270.6293;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;56;203.2866,-119.7623;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;10;-1239.447,135.2091;Float;True;Property;_Noise_Texture;Noise_Texture;0;0;Create;True;0;0;False;0;None;00ddc1265ec46694c8e23aee035fb8c2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;50;187.4702,-410.7799;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;12;-1326.229,529.4432;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;60;385.7007,-31.19365;Float;False;Property;_Color;Color;7;0;Create;True;0;0;False;0;0.8773585,0.3269402,0.3269402,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;498.5222,-364.3886;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;16;-557.1184,31.53565;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;20;-556.4311,250.4544;Float;False;Property;_Color_Dissolve;Color_Dissolve;4;0;Create;False;0;0;False;0;1,0,0,1;0,0,0,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;13;-1153.305,524.2922;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.6;False;4;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;57;354.0667,-646.9664;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-609.3362,630.9385;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-316.1874,75.00062;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;752.6334,-446.6292;Float;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1093.082,-251.7455;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;planetDissolve;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;5;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;45;0;18;0
WireConnection;45;1;46;0
WireConnection;43;0;44;0
WireConnection;43;1;18;0
WireConnection;48;0;45;0
WireConnection;48;1;49;0
WireConnection;42;1;43;0
WireConnection;32;0;11;0
WireConnection;32;1;31;0
WireConnection;56;0;48;0
WireConnection;50;0;18;0
WireConnection;50;1;48;0
WireConnection;12;0;11;0
WireConnection;62;0;50;0
WireConnection;62;1;60;0
WireConnection;16;0;10;0
WireConnection;16;1;32;0
WireConnection;13;0;12;0
WireConnection;57;0;42;6
WireConnection;57;1;56;0
WireConnection;29;0;10;0
WireConnection;29;1;13;0
WireConnection;19;0;16;0
WireConnection;19;1;20;0
WireConnection;59;0;57;0
WireConnection;59;1;62;0
WireConnection;0;0;59;0
WireConnection;0;2;19;0
WireConnection;0;10;29;0
ASEEND*/
//CHKSM=5B0F12DF8D3DDA344F1540AE9E8F1EBE050403F9