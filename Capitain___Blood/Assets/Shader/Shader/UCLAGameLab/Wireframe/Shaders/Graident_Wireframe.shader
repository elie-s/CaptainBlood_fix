﻿Shader "Custom/Graident_Wireframe"
{
    Properties
    {
        _Color ("Top Color", Color) = (1,1,1,1)
		_Color1 ("Bottom Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed4 _Color1;

        struct Input
        {
            float2 uv_MainTex;
			float4 screenPos;
        };




        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * lerp(_Color, _Color1, screenUV.y);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "VertexLit"
}
