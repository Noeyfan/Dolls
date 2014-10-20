﻿// simple "dissolving" shader by genericuser (radware.wordpress.com)
// clips materials, using an image as guidance.
// use clouds or random noise as the slice guide for best results.
  Shader "Custom Shaders/Dissolving" {
    Properties {
      _MainTex ("Texture (RGB)", 2D) = "white" {}
      _SliceGuide ("Slice Guide (RGB)", 2D) = "white" {}
      _BurnAmount ("Burn Amount", Range(0.0, 1)) = 0.5
	  _AshAmount ("Ash Amount", Range(0.0, 1)) = 0.1
	  _BurnSpread ("Burn Spread", Range(0.0,1)) = 0.3
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      Cull Off
      CGPROGRAM
      //if you're not planning on using shadows, remove "addshadow" for better performance
      #pragma surface surf BlinnPhong
      struct Input {
          float2 uv_MainTex;
          float2 uv_SliceGuide;
          float _SliceAmount;
      };

      sampler2D _MainTex;
      sampler2D _SliceGuide;
      float _BurnAmount;
	  float _AshAmount;
	  float _BurnSpread;

      void surf (Input IN, inout SurfaceOutput o) {
          float alpha = tex2D (_SliceGuide, IN.uv_SliceGuide).a;
		  float burnt_amount = alpha - _BurnAmount ;
		  clip(burnt_amount+ _AshAmount); 
		  float burn_function = -( pow(((burnt_amount/_BurnSpread)-1),2.0) ) + 1;
		  float burnt_term = (burnt_amount < _BurnSpread ? (1 - burn_function) : 0);
		  

		  half3 color = clamp( tex2D (_MainTex, IN.uv_MainTex).rgb - burnt_term, 0.0, 1.0);
		  o.Albedo = color;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }