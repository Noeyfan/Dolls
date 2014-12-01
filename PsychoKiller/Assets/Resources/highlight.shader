Shader "Custom/highlight" {
	Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _Color ("Outline Color",Color) = (1,0.5,0.5,1.0)  
	_Offset ("Offset length", Float) = 0.05
    }
	
	SubShader {
	Tags {"Queue"="Overlay"}
	
	Pass { 
	Blend Off
	Cull Off
	//ZTest Off
	ZWrite Off
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 vertex : POSITION;
				float3 normalDir : TEXCOORD0;
				float4 worldpos : TEXCOORD1;
			};

			float _Offset; 
			fixed4 _Color;

			v2f vert (appdata_t v)
			{
				v2f o;
				//float3 viewdir = WorldSpaceViewDir(v.vertex);
				//float3 sidedir = normalize(cross(viewdir,float3(0.0,1.0,0.0)));
				
				//o.vertex = mul(UNITY_MATRIX_MVP, v.vertex + normalize(float4(normalize(sidedir) * sign(v.vertex.x),0) + float4(0.0,1.0,0.0,0.0) * sign(v.vertex.y)) * _Offset);
				v.vertex.xyz *= (1.0 + _Offset);
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);				 
				o.normalDir = normalize( mul(float4(v.normal, 0.0), _World2Object).xyz);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				//fixed4 col = fixed4(1,0.2,0.2,1.0);//_Color;
				fixed4 col = fixed4(0.674,0.98,1,1.0);//_Color;
				return col;
			}
		ENDCG
	}
	
	Pass {  
	
	Blend Off
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Cutoff;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				col.a = 0;
				return col;
			}
		ENDCG
	}

    }
}
