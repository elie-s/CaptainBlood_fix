
Shader "Custom/Clip/Plane" {
    Properties {
    
      _PlanePoint ("Plane Point (World Space)", Vector) = (0,0,0,0)
      _PlaneNormal ("Plane Normal (World Space)", Vector) = (1,0,0,0)
  
      _MainTex ("Main Texture", 2D) = "white" {}

	  
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      Cull Off
      CGPROGRAM
      // Physically based Standard lighting model, and enable shadows on all light types
	  #pragma surface surf Standard fullforwardshadows
	  // Use shader model 3.0 target, to get nicer looking lighting
	  #pragma target 3.0
		
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float2 uv_Conversion;
          float3 worldPos;
      };


	  
      float3 _PlanePoint;
      float3 _PlaneNormal;
      
      sampler2D _MainTex;

      
      
      void surf (Input IN, inout SurfaceOutputStandard o) {
      
         //clip (frac((IN.worldPos.y+IN.worldPos.z*0.1) * 5) - 0.5);
         // clip ( distance( IN.worldPos, _Point) - _Distance);
         
         _PlaneNormal = normalize(_PlaneNormal);
          
          half dist = (IN.worldPos.x * _PlaneNormal.x) + (IN.worldPos.y * _PlaneNormal.y) + (IN.worldPos.z * _PlaneNormal.z)
        - (_PlanePoint.x * _PlaneNormal.x) - (_PlanePoint.y * _PlaneNormal.y) - (_PlanePoint.z * _PlaneNormal.z) 
        / sqrt( pow(_PlaneNormal.x, 2) + pow(_PlaneNormal.y, 2) + pow(_PlaneNormal.z,2));
          
          // distance from plane
          clip(dist);
          
          
          // min = 0 // value = dist // max = _ConvertDistance

          
          fixed4 albedo = tex2D (_MainTex, IN.uv_MainTex);
          
       
        
          o.Albedo = albedo.rgb ;
          
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }