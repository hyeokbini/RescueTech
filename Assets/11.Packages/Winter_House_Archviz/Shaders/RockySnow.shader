// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WILL/RockySnow"
{
	Properties
	{
		_RockColor("Rock Color", Color) = (1,1,1,0)
		_RockAlbedo("Rock Albedo", 2D) = "white" {}
		_RockNormal("Rock Normal", 2D) = "bump" {}
		_RockNormalPower("Rock Normal Power", Float) = 1
		_RockAO("Rock AO", 2D) = "white" {}
		_RockAOPower("Rock AO Power", Range( 0 , 1)) = 0
		_RockMetallicSmooth("Rock MetallicSmooth", 2D) = "white" {}
		_RockMetallic("Rock Metallic", Range( 0 , 1)) = 0
		_RockSmoothness("Rock Smoothness", Range( 0 , 1)) = 0
		_SnowColor("Snow Color", Color) = (1,1,1,0)
		_SnowAlbedo("Snow Albedo", 2D) = "white" {}
		_SnowNormal("Snow Normal", 2D) = "bump" {}
		_SnowNormalPower("Snow Normal Power", Float) = 1
		_SnowAO("Snow AO", 2D) = "white" {}
		_SnowAOPower("Snow AO Power", Range( 0 , 1)) = 0
		_SnowMetallicSmooth("Snow MetallicSmooth", 2D) = "white" {}
		_SnowMetallic("Snow Metallic", Range( 0 , 1)) = 0
		_SnowSmoothness("Snow Smoothness", Range( 0 , 1)) = 0
		_SnowAmount("SnowAmount", Float) = 0.13
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		ZTest LEqual
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform float _RockNormalPower;
		uniform sampler2D _RockNormal;
		uniform float4 _RockNormal_ST;
		uniform float _SnowNormalPower;
		uniform sampler2D _SnowNormal;
		uniform float4 _SnowNormal_ST;
		uniform float _SnowAmount;
		uniform sampler2D _RockAlbedo;
		uniform float4 _RockAlbedo_ST;
		uniform float4 _RockColor;
		uniform sampler2D _SnowAlbedo;
		uniform float4 _SnowAlbedo_ST;
		uniform float4 _SnowColor;
		uniform sampler2D _RockMetallicSmooth;
		uniform float4 _RockMetallicSmooth_ST;
		uniform float _RockMetallic;
		uniform sampler2D _SnowMetallicSmooth;
		uniform float4 _SnowMetallicSmooth_ST;
		uniform float _SnowMetallic;
		uniform float _RockSmoothness;
		uniform float _SnowSmoothness;
		uniform sampler2D _RockAO;
		uniform float4 _RockAO_ST;
		uniform float _RockAOPower;
		uniform sampler2D _SnowAO;
		uniform float4 _SnowAO_ST;
		uniform float _SnowAOPower;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_RockNormal = i.uv_texcoord * _RockNormal_ST.xy + _RockNormal_ST.zw;
			float2 uv_SnowNormal = i.uv_texcoord * _SnowNormal_ST.xy + _SnowNormal_ST.zw;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 lerpResult15 = lerp( UnpackScaleNormal( tex2D( _RockNormal, uv_RockNormal ) ,_RockNormalPower ) , UnpackScaleNormal( tex2D( _SnowNormal, uv_SnowNormal ) ,_SnowNormalPower ) , saturate( ( ase_worldNormal.y * _SnowAmount ) ));
			o.Normal = lerpResult15;
			float2 uv_RockAlbedo = i.uv_texcoord * _RockAlbedo_ST.xy + _RockAlbedo_ST.zw;
			float2 uv_SnowAlbedo = i.uv_texcoord * _SnowAlbedo_ST.xy + _SnowAlbedo_ST.zw;
			float temp_output_18_0 = saturate( ( WorldNormalVector( i , lerpResult15 ).y * _SnowAmount ) );
			float4 lerpResult10 = lerp( ( tex2D( _RockAlbedo, uv_RockAlbedo ) * _RockColor ) , ( tex2D( _SnowAlbedo, uv_SnowAlbedo ) * _SnowColor ) , temp_output_18_0);
			o.Albedo = lerpResult10.rgb;
			float2 uv_RockMetallicSmooth = i.uv_texcoord * _RockMetallicSmooth_ST.xy + _RockMetallicSmooth_ST.zw;
			float4 tex2DNode2 = tex2D( _RockMetallicSmooth, uv_RockMetallicSmooth );
			float2 uv_SnowMetallicSmooth = i.uv_texcoord * _SnowMetallicSmooth_ST.xy + _SnowMetallicSmooth_ST.zw;
			float4 tex2DNode16 = tex2D( _SnowMetallicSmooth, uv_SnowMetallicSmooth );
			float4 lerpResult17 = lerp( ( tex2DNode2 * _RockMetallic ) , ( tex2DNode16 * _SnowMetallic ) , temp_output_18_0);
			o.Metallic = lerpResult17.r;
			float lerpResult23 = lerp( ( tex2DNode2.a * _RockSmoothness ) , ( tex2DNode16.a * _SnowSmoothness ) , temp_output_18_0);
			o.Smoothness = lerpResult23;
			float4 temp_cast_2 = (1.0).xxxx;
			float2 uv_RockAO = i.uv_texcoord * _RockAO_ST.xy + _RockAO_ST.zw;
			float4 lerpResult31 = lerp( temp_cast_2 , tex2D( _RockAO, uv_RockAO ) , _RockAOPower);
			float4 temp_cast_3 = (1.0).xxxx;
			float2 uv_SnowAO = i.uv_texcoord * _SnowAO_ST.xy + _SnowAO_ST.zw;
			float4 lerpResult35 = lerp( temp_cast_3 , tex2D( _SnowAO, uv_SnowAO ) , _SnowAOPower);
			float4 lerpResult36 = lerp( lerpResult31 , lerpResult35 , temp_output_18_0);
			o.Occlusion = lerpResult36.r;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=15401
350;197;1802;1137;3298.917;515.1046;2.203232;True;True
Node;AmplifyShaderEditor.RangedFloatNode;12;-1773.599,-115.1001;Float;False;Property;_SnowAmount;SnowAmount;18;0;Create;True;0;0;False;0;0.13;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;20;-1685.906,-319.7951;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;46;-1808.449,293.5183;Float;False;Property;_SnowNormalPower;Snow Normal Power;12;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-1822.449,68.51828;Float;False;Property;_RockNormalPower;Rock Normal Power;3;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1444.505,-272.8951;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-1548.101,267.1998;Float;True;Property;_SnowNormal;Snow Normal;11;0;Create;True;0;0;False;0;None;4f892efbede37b24fb7a62705fe6fe86;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;22;-1283.806,-264.4951;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-1548.101,72.7999;Float;True;Property;_RockNormal;Rock Normal;2;0;Create;True;0;0;False;0;None;b43c3e2a5eb101c498cbc8be3ab200bf;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;15;-1067.3,73.59992;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;19;-865.5047,-194.2967;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;2;-773.0005,186.9001;Float;True;Property;_RockMetallicSmooth;Rock MetallicSmooth;6;0;Create;True;0;0;False;0;None;15c2414b791d3994bb42cace924a0e96;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;29;-832.3375,1435.631;Float;True;Property;_SnowAO;Snow AO;13;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;37;-747.6633,392.1023;Float;False;Property;_RockMetallic;Rock Metallic;7;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-635.6617,1232.64;Float;False;Constant;_Float0;Float 0;11;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;28;-806.0158,1034.507;Float;True;Property;_RockAO;Rock AO;4;0;Create;True;0;0;False;0;None;7ab069d3fef625c439037fa7fe0c7f7a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;41;-804.0576,-861.5811;Float;False;Property;_RockColor;Rock Color;0;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;16;-781.8019,661.7009;Float;True;Property;_SnowMetallicSmooth;Snow MetallicSmooth;15;0;Create;True;0;0;False;0;None;89dfff602af024144abbb1ff2e8e1909;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;25;-768.2457,949.9339;Float;False;Property;_SnowSmoothness;Snow Smoothness;17;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-651.7996,-116.3;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-856.4,-1061.652;Float;True;Property;_RockAlbedo;Rock Albedo;1;0;Create;True;0;0;False;0;None;15c2414b791d3994bb42cace924a0e96;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;34;-823.4656,1801.146;Float;False;Property;_SnowAOPower;Snow AO Power;14;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-794.4747,1319.935;Float;False;Property;_RockAOPower;Rock AO Power;5;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;9;-850.4003,-629.1513;Float;True;Property;_SnowAlbedo;Snow Albedo;10;0;Create;True;0;0;False;0;None;89dfff602af024144abbb1ff2e8e1909;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;33;-701.109,1715.027;Float;False;Constant;_Float1;Float 1;11;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;43;-794.7476,-431.7825;Float;False;Property;_SnowColor;Snow Color;9;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;39;-754.9703,859.8183;Float;False;Property;_SnowMetallic;Snow Metallic;16;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-733.0459,474.9345;Float;False;Property;_RockSmoothness;Rock Smoothness;8;0;Create;True;0;0;False;0;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-452.0576,-829.2311;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-497.6446,-418.8557;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-426.3458,849.6338;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-421.6604,267.0212;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;35;-442.7329,1608.219;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;31;-434.7801,1184.259;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;18;-496.7049,-110.1974;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-419.0459,374.9345;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-424.5673,721.1372;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;17;-175.2019,315.4006;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;36;-115.0133,1427.273;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;23;-172.2459,634.3336;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;10;-127.1,-308.6001;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;146.9001,44.82402;Float;False;True;2;Float;;0;0;Standard;WILL/RockySnow;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;21;0;20;2
WireConnection;21;1;12;0
WireConnection;14;5;46;0
WireConnection;22;0;21;0
WireConnection;4;5;45;0
WireConnection;15;0;4;0
WireConnection;15;1;14;0
WireConnection;15;2;22;0
WireConnection;19;0;15;0
WireConnection;11;0;19;2
WireConnection;11;1;12;0
WireConnection;42;0;1;0
WireConnection;42;1;41;0
WireConnection;44;0;9;0
WireConnection;44;1;43;0
WireConnection;27;0;16;4
WireConnection;27;1;25;0
WireConnection;38;0;2;0
WireConnection;38;1;37;0
WireConnection;35;0;33;0
WireConnection;35;1;29;0
WireConnection;35;2;34;0
WireConnection;31;0;30;0
WireConnection;31;1;28;0
WireConnection;31;2;32;0
WireConnection;18;0;11;0
WireConnection;26;0;2;4
WireConnection;26;1;24;0
WireConnection;40;0;16;0
WireConnection;40;1;39;0
WireConnection;17;0;38;0
WireConnection;17;1;40;0
WireConnection;17;2;18;0
WireConnection;36;0;31;0
WireConnection;36;1;35;0
WireConnection;36;2;18;0
WireConnection;23;0;26;0
WireConnection;23;1;27;0
WireConnection;23;2;18;0
WireConnection;10;0;42;0
WireConnection;10;1;44;0
WireConnection;10;2;18;0
WireConnection;0;0;10;0
WireConnection;0;1;15;0
WireConnection;0;3;17;0
WireConnection;0;4;23;0
WireConnection;0;5;36;0
ASEEND*/
//CHKSM=727D880F54224D00F9403400E6456C0043A22123