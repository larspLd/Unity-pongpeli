Shader "Instanced/DustShader" {
	Properties {
		_Color ("Colour", Color) = (1,1,1,1)
	}
	SubShader {

		Pass {

			Tags {"Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Transparent"}
			ZWrite Off
			Lighting Off
			Fog { Mode Off }

			Blend SrcAlpha OneMinusSrcAlpha 
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
			#pragma target 4.5

			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			#include "AutoLight.cginc"

			float4 _Color;

		#if SHADER_TARGET >= 45
			StructuredBuffer<float4> positionBuffer;
			StructuredBuffer<float4> colorBuffer;
		#endif
		

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR0;
			};
			void rotate2D(inout float2 v, float r)
            {
                float s, c;
                sincos(r, s, c);
                v = float2(v.x - v.y, v.x + v.y);
            }

			v2f vert (appdata_full v, uint instanceID : SV_InstanceID)
			{
			#if SHADER_TARGET >= 45
				float4 data = positionBuffer[instanceID];
				float4 colorData = colorBuffer[instanceID];
			#else
				float4 data = 0;
			#endif

				float rotation = data.w * data.w * _Time.x * 0.5f;
				rotate2D(data.xz, rotation);

				float4 color = colorData;
				float3 localPosition = v.vertex.xyz * data.w;
				float3 worldPosition = data.xyz + localPosition;

				v2f o;
				o.pos = mul(UNITY_MATRIX_VP, float4(worldPosition, 1.0f));
				o.uv = v.texcoord;
				o.color = color;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 offsetFromCentre = 0.5 - i.uv;
				float r = length(offsetFromCentre) * 2;
				//float alpha = max(0, 1 - (length(offsetFromCentre) * 2));
				//alpha = 1;

				return i.color;
			}

			ENDCG
		}
	}
}
