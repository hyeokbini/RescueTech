// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WILL/TreeSnow"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_BaseColor("Base Color", Color) = (1,1,1,0)
		_BaseAlbedo("Base Albedo", 2D) = "white" {}
		_BaseNormal("Base Normal", 2D) = "bump" {}
		_BaseNormalPower("Base Normal Power", Float) = 1
		_BaseAO("Base AO", 2D) = "white" {}
		_BaseAOPower("Base AO Power", Range( 0 , 1)) = 0
		_BaseMetallicSmooth("Base MetallicSmooth", 2D) = "white" {}
		_BaseMetallic("Base Metallic", Range( 0 , 1)) = 0
		_BaseSmoothness("Base Smoothness", Range( 0 , 1)) = 0
		_SnowColor("Snow Color", Color) = (1,1,1,0)
		_SnowAlbedo("Snow Albedo", 2D) = "white" {}
		_SnowNormal("Snow Normal", 2D) = "bump" {}
		_SnowNormalPower("Snow Normal Power", Float) = 1
		_SnowMetallicSmooth("Snow MetallicSmooth", 2D) = "white" {}
		_SnowMetallic("Snow Metallic", Range( 0 , 1)) = 0
		_SnowSmoothness("Snow Smoothness", Range( 0 , 1)) = 0
		_SnowAmount("SnowAmount", Float) = 0.13
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TreeTransparentCutout"  "Queue" = "Geometry+0" }
		Cull Off
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
			float2 uv2_texcoord2;
		};

		uniform float _BaseNormalPower;
		uniform sampler2D _BaseNormal;
		uniform float4 _BaseNormal_ST;
		uniform float _SnowNormalPower;
		uniform sampler2D _SnowNormal;
		uniform float4 _SnowNormal_ST;
		uniform float _SnowAmount;
		uniform sampler2D _BaseAlbedo;
		uniform float4 _BaseAlbedo_ST;
		uniform float4 _BaseColor;
		uniform sampler2D _SnowAlbedo;
		uniform float4 _SnowAlbedo_ST;
		uniform float4 _SnowColor;
		uniform sampler2D _BaseMetallicSmooth;
		uniform float4 _BaseMetallicSmooth_ST;
		uniform float _BaseMetallic;
		uniform sampler2D _SnowMetallicSmooth;
		uniform float4 _SnowMetallicSmooth_ST;
		uniform float _SnowMetallic;
		uniform float _BaseSmoothness;
		uniform float _SnowSmoothness;
		uniform sampler2D _BaseAO;
		uniform float4 _BaseAO_ST;
		uniform float _BaseAOPower;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BaseNormal = i.uv_texcoord * _BaseNormal_ST.xy + _BaseNormal_ST.zw;
			float2 uv_SnowNormal = i.uv_texcoord * _SnowNormal_ST.xy + _SnowNormal_ST.zw;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 lerpResult15 = lerp( UnpackScaleNormal( tex2D( _BaseNormal, uv_BaseNormal ) ,_BaseNormalPower ) , UnpackScaleNormal( tex2D( _SnowNormal, uv_SnowNormal ) ,_SnowNormalPower ) , saturate( ( ase_worldNormal.y * _SnowAmount ) ));
			o.Normal = lerpResult15;
			float2 uv_BaseAlbedo = i.uv_texcoord * _BaseAlbedo_ST.xy + _BaseAlbedo_ST.zw;
			float4 tex2DNode1 = tex2D( _BaseAlbedo, uv_BaseAlbedo );
			float2 uv_SnowAlbedo = i.uv_texcoord * _SnowAlbedo_ST.xy + _SnowAlbedo_ST.zw;
			float temp_output_18_0 = saturate( ( WorldNormalVector( i , lerpResult15 ).y * _SnowAmount ) );
			float4 lerpResult10 = lerp( ( tex2DNode1 * _BaseColor ) , ( tex2D( _SnowAlbedo, uv_SnowAlbedo ) * _SnowColor ) , temp_output_18_0);
			o.Albedo = lerpResult10.rgb;
			float2 uv_BaseMetallicSmooth = i.uv_texcoord * _BaseMetallicSmooth_ST.xy + _BaseMetallicSmooth_ST.zw;
			float4 tex2DNode2 = tex2D( _BaseMetallicSmooth, uv_BaseMetallicSmooth );
			float2 uv_SnowMetallicSmooth = i.uv_texcoord * _SnowMetallicSmooth_ST.xy + _SnowMetallicSmooth_ST.zw;
			float4 tex2DNode16 = tex2D( _SnowMetallicSmooth, uv_SnowMetallicSmooth );
			float4 lerpResult17 = lerp( ( tex2DNode2 * _BaseMetallic ) , ( tex2DNode16 * _SnowMetallic ) , temp_output_18_0);
			o.Metallic = lerpResult17.r;
			float lerpResult23 = lerp( ( tex2DNode2.a * _BaseSmoothness ) , ( tex2DNode16.a * _SnowSmoothness ) , temp_output_18_0);
			o.Smoothness = lerpResult23;
			float4 temp_cast_2 = (1.0).xxxx;
			float2 uv2_BaseAO = i.uv2_texcoord2 * _BaseAO_ST.xy + _BaseAO_ST.zw;
			float4 lerpResult31 = lerp( temp_cast_2 , tex2D( _BaseAO, uv2_BaseAO ) , _BaseAOPower);
			o.Occlusion = lerpResult31.r;
			o.Alpha = 1;
			clip( tex2DNode1.a - _Cutoff );
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
				float4 customPack1 : TEXCOORD1;
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
				o.customPack1.zw = customInputData.uv2_texcoord2;
				o.customPack1.zw = v.texcoord1;
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
				surfIN.uv2_texcoord2 = IN.customPack1.zw;
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
7;29;2546;1364;1789.435;1024.274;1;True;True
Node;AmplifyShaderEditor.WorldNormalVector;20;-1685.906,-319.7951;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;12;-1773.599,-115.1001;Float;False;Property;_SnowAmount;SnowAmount;17;0;Create;True;0;0;False;0;0.13;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-1822.449,68.51828;Float;False;Property;_BaseNormalPower;Base Normal Power;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-1808.449,293.5183;Float;False;Property;_SnowNormalPower;Snow Normal Power;13;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1444.505,-272.8951;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-1548.101,267.1998;Float;True;Property;_SnowNormal;Snow Normal;12;0;Create;True;0;0;False;0;None;4f892efbede37b24fb7a62705fe6fe86;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-1548.101,72.7999;Float;True;Property;_BaseNormal;Base Normal;3;0;Create;True;0;0;False;0;None;b43c3e2a5eb101c498cbc8be3ab200bf;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;22;-1283.806,-264.4951;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;15;-1067.3,73.59992;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;19;-865.5047,-194.2967;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;37;-747.6633,392.1023;Float;False;Property;_BaseMetallic;Base Metallic;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-733.0459,474.9345;Float;False;Property;_BaseSmoothness;Base Smoothness;9;0;Create;True;0;0;False;0;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-773.0005,186.9001;Float;True;Property;_BaseMetallicSmooth;Base MetallicSmooth;7;0;Create;True;0;0;False;0;None;15c2414b791d3994bb42cace924a0e96;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;41;-804.0576,-861.5811;Float;False;Property;_BaseColor;Base Color;1;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-850.4003,-629.1513;Float;True;Property;_SnowAlbedo;Snow Albedo;11;0;Create;True;0;0;False;0;None;89dfff602af024144abbb1ff2e8e1909;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-856.4,-1061.652;Float;True;Property;_BaseAlbedo;Base Albedo;2;0;Create;True;0;0;False;0;None;15c2414b791d3994bb42cace924a0e96;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-651.7996,-116.3;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;43;-794.7476,-431.7825;Float;False;Property;_SnowColor;Snow Color;10;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;16;-781.8019,661.7009;Float;True;Property;_SnowMetallicSmooth;Snow MetallicSmooth;14;0;Create;True;0;0;False;0;None;89dfff602af024144abbb1ff2e8e1909;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;25;-768.2457,949.9339;Float;False;Property;_SnowSmoothness;Snow Smoothness;16;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-754.9703,859.8183;Float;False;Property;_SnowMetallic;Snow Metallic;15;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-707.6617,1381.64;Float;False;Constant;_Float0;Float 0;11;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-866.4747,1468.935;Float;False;Property;_BaseAOPower;Base AO Power;6;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;28;-878.0158,1183.507;Float;True;Property;_BaseAO;Base AO;5;0;Create;True;0;0;False;0;None;7ab069d3fef625c439037fa7fe0c7f7a;True;1;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-421.6604,267.0212;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;18;-496.7049,-110.1974;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-426.3458,849.6338;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-452.0576,-829.2311;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-497.6446,-418.8557;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-424.5673,721.1372;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-419.0459,374.9345;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;31;-506.7801,1333.259;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;10;-127.1,-308.6001;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;17;-175.2019,315.4006;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;23;-172.2459,634.3336;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;173.9001,-85.17598;Float;False;True;2;Float;;0;0;Standard;WILL/TreeSnow;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TreeTransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;21;0;20;2
WireConnection;21;1;12;0
WireConnection;14;5;46;0
WireConnection;4;5;45;0
WireConnection;22;0;21;0
WireConnection;15;0;4;0
WireConnection;15;1;14;0
WireConnection;15;2;22;0
WireConnection;19;0;15;0
WireConnection;11;0;19;2
WireConnection;11;1;12;0
WireConnection;38;0;2;0
WireConnection;38;1;37;0
WireConnection;18;0;11;0
WireConnection;27;0;16;4
WireConnection;27;1;25;0
WireConnection;42;0;1;0
WireConnection;42;1;41;0
WireConnection;44;0;9;0
WireConnection;44;1;43;0
WireConnection;40;0;16;0
WireConnection;40;1;39;0
WireConnection;26;0;2;4
WireConnection;26;1;24;0
WireConnection;31;0;30;0
WireConnection;31;1;28;0
WireConnection;31;2;32;0
WireConnection;10;0;42;0
WireConnection;10;1;44;0
WireConnection;10;2;18;0
WireConnection;17;0;38;0
WireConnection;17;1;40;0
WireConnection;17;2;18;0
WireConnection;23;0;26;0
WireConnection;23;1;27;0
WireConnection;23;2;18;0
WireConnection;0;0;10;0
WireConnection;0;1;15;0
WireConnection;0;3;17;0
WireConnection;0;4;23;0
WireConnection;0;5;31;0
WireConnection;0;10;1;4
ASEEND*/
//CHKSM=FB560EE4ABFC1E69E86F4C3B66EFABE62AE3F060