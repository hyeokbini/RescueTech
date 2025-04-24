// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WILL/Mountain_3Layers"
{
	Properties
	{
		_MaskTexture("Mask Texture", 2D) = "white" {}
		_MainNormal("MainNormal", 2D) = "bump" {}
		_MainNormal_Intensity("MainNormal_Intensity", Float) = 1
		_R_AlbedoColor("R_Albedo Color", Color) = (1,1,1,0)
		_R_NormalMap("R_Normal Map", 2D) = "bump" {}
		_R_Albedo("R_Albedo", 2D) = "white" {}
		_R_NormalIntensity("R_Normal  Intensity", Float) = 1
		_R_Metalic("R_Metalic", 2D) = "white" {}
		_R_MetalicIntensity("R_Metalic Intensity", Range( 0 , 1)) = 0
		_R_SmoothnessIntensity("R_Smoothness Intensity", Range( 0 , 1)) = 0
		_G_AlbedoColor("G_Albedo Color", Color) = (1,1,1,0)
		_G_Albedo("G_Albedo", 2D) = "white" {}
		_G_NormalMap("G_Normal Map", 2D) = "bump" {}
		_G_NormalIntensity("G_Normal  Intensity", Float) = 1
		_G_Metalic("G_Metalic", 2D) = "white" {}
		_G_MetalicIntensity("G_Metalic Intensity", Range( 0 , 1)) = 0
		_G_SmoothnessIntensity("G_Smoothness Intensity", Range( 0 , 1)) = 0
		_B_AlbedoColor("B_Albedo Color", Color) = (1,1,1,0)
		_B_Albedo("B_Albedo", 2D) = "white" {}
		_B_NormalMap("B_Normal Map", 2D) = "bump" {}
		_B_NormalIntensity("B_Normal  Intensity", Float) = 1
		_B_Metalic("B_Metalic", 2D) = "white" {}
		_B_MetalicIntensity("B_Metalic Intensity", Range( 0 , 1)) = 0
		_B_SmoothnessIntensity("B_Smoothness Intensity", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _MainNormal_Intensity;
		uniform sampler2D _MainNormal;
		uniform float4 _MainNormal_ST;
		uniform sampler2D _MaskTexture;
		uniform float4 _MaskTexture_ST;
		uniform float _R_NormalIntensity;
		uniform sampler2D _R_NormalMap;
		uniform float4 _R_NormalMap_ST;
		uniform float _G_NormalIntensity;
		uniform sampler2D _G_NormalMap;
		uniform float4 _G_NormalMap_ST;
		uniform float _B_NormalIntensity;
		uniform sampler2D _B_NormalMap;
		uniform float4 _B_NormalMap_ST;
		uniform sampler2D _R_Albedo;
		uniform float4 _R_Albedo_ST;
		uniform float4 _R_AlbedoColor;
		uniform sampler2D _G_Albedo;
		uniform float4 _G_Albedo_ST;
		uniform float4 _G_AlbedoColor;
		uniform sampler2D _B_Albedo;
		uniform float4 _B_Albedo_ST;
		uniform float4 _B_AlbedoColor;
		uniform sampler2D _R_Metalic;
		uniform float4 _R_Metalic_ST;
		uniform float _R_MetalicIntensity;
		uniform sampler2D _G_Metalic;
		uniform float4 _G_Metalic_ST;
		uniform float _G_MetalicIntensity;
		uniform sampler2D _B_Metalic;
		uniform float4 _B_Metalic_ST;
		uniform float _B_MetalicIntensity;
		uniform float _R_SmoothnessIntensity;
		uniform float _G_SmoothnessIntensity;
		uniform float _B_SmoothnessIntensity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainNormal = i.uv_texcoord * _MainNormal_ST.xy + _MainNormal_ST.zw;
			float2 uv_MaskTexture = i.uv_texcoord * _MaskTexture_ST.xy + _MaskTexture_ST.zw;
			float4 tex2DNode17 = tex2D( _MaskTexture, uv_MaskTexture );
			float2 uv_R_NormalMap = i.uv_texcoord * _R_NormalMap_ST.xy + _R_NormalMap_ST.zw;
			float2 uv_G_NormalMap = i.uv_texcoord * _G_NormalMap_ST.xy + _G_NormalMap_ST.zw;
			float2 uv_B_NormalMap = i.uv_texcoord * _B_NormalMap_ST.xy + _B_NormalMap_ST.zw;
			o.Normal = BlendNormals( UnpackScaleNormal( tex2D( _MainNormal, uv_MainNormal ) ,_MainNormal_Intensity ) , ( ( tex2DNode17.r * UnpackScaleNormal( tex2D( _R_NormalMap, uv_R_NormalMap ) ,_R_NormalIntensity ) ) + ( tex2DNode17.g * UnpackScaleNormal( tex2D( _G_NormalMap, uv_G_NormalMap ) ,_G_NormalIntensity ) ) + ( tex2DNode17.b * UnpackScaleNormal( tex2D( _B_NormalMap, uv_B_NormalMap ) ,_B_NormalIntensity ) ) ) );
			float2 uv_R_Albedo = i.uv_texcoord * _R_Albedo_ST.xy + _R_Albedo_ST.zw;
			float2 uv_G_Albedo = i.uv_texcoord * _G_Albedo_ST.xy + _G_Albedo_ST.zw;
			float2 uv_B_Albedo = i.uv_texcoord * _B_Albedo_ST.xy + _B_Albedo_ST.zw;
			o.Albedo = ( ( tex2DNode17.r * ( tex2D( _R_Albedo, uv_R_Albedo ) * _R_AlbedoColor ) ) + ( tex2DNode17.g * ( tex2D( _G_Albedo, uv_G_Albedo ) * _G_AlbedoColor ) ) + ( tex2DNode17.b * ( tex2D( _B_Albedo, uv_B_Albedo ) * _B_AlbedoColor ) ) ).rgb;
			float2 uv_R_Metalic = i.uv_texcoord * _R_Metalic_ST.xy + _R_Metalic_ST.zw;
			float4 tex2DNode11 = tex2D( _R_Metalic, uv_R_Metalic );
			float2 uv_G_Metalic = i.uv_texcoord * _G_Metalic_ST.xy + _G_Metalic_ST.zw;
			float4 tex2DNode5 = tex2D( _G_Metalic, uv_G_Metalic );
			float2 uv_B_Metalic = i.uv_texcoord * _B_Metalic_ST.xy + _B_Metalic_ST.zw;
			float4 tex2DNode69 = tex2D( _B_Metalic, uv_B_Metalic );
			o.Metallic = ( ( tex2DNode17.r * ( tex2DNode11 * _R_MetalicIntensity ) ) + ( tex2DNode17.g * ( tex2DNode5 * _G_MetalicIntensity ) ) + ( tex2DNode17.b * ( tex2DNode69 * _B_MetalicIntensity ) ) ).r;
			o.Smoothness = ( ( tex2DNode17.r * ( tex2DNode11.a * _R_SmoothnessIntensity ) ) + ( tex2DNode17.g * ( tex2DNode5.a * _G_SmoothnessIntensity ) ) + ( tex2DNode17.b * ( tex2DNode69.a * _B_SmoothnessIntensity ) ) );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=15401
7;29;2546;1364;3771.168;1602.854;2.245869;True;True
Node;AmplifyShaderEditor.RangedFloatNode;70;-1687.307,2081.501;Float;False;Property;_B_NormalIntensity;B_Normal  Intensity;20;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1722.843,507.6188;Float;False;Property;_G_NormalIntensity;G_Normal  Intensity;13;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1707.869,-980.9489;Float;False;Property;_R_NormalIntensity;R_Normal  Intensity;6;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-1361.74,-1372.595;Float;True;Property;_R_Albedo;R_Albedo;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;41;-1306.859,-1185.483;Float;False;Property;_R_AlbedoColor;R_Albedo Color;3;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-1342.166,-459.3149;Float;False;Property;_R_SmoothnessIntensity;R_Smoothness Intensity;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;69;-1337.79,2300.989;Float;True;Property;_B_Metalic;B_Metalic;21;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;2;-1359.889,948.2106;Float;False;Property;_G_MetalicIntensity;G_Metalic Intensity;15;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;16;-1385.581,100.8863;Float;True;Property;_G_Albedo;G_Albedo;11;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;23;-1390.69,488.3783;Float;True;Property;_G_NormalMap;G_Normal Map;12;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1;-1359.26,1029.253;Float;False;Property;_G_SmoothnessIntensity;G_Smoothness Intensity;16;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-1375.601,728.2426;Float;True;Property;_G_Metalic;G_Metalic;14;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;71;-1347.77,1699.197;Float;True;Property;_B_Albedo;B_Albedo;18;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;67;-1348.367,2038.566;Float;True;Property;_B_NormalMap;B_Normal Map;19;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;43;-1337.59,311.984;Float;False;Property;_G_AlbedoColor;G_Albedo Color;10;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;68;-1321.449,2602.001;Float;False;Property;_B_SmoothnessIntensity;B_Smoothness Intensity;23;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-1360.626,-760.3248;Float;True;Property;_R_Metalic;R_Metalic;7;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-1344.914,-540.3566;Float;False;Property;_R_MetalicIntensity;R_Metalic Intensity;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;66;-1299.779,1884.729;Float;False;Property;_B_AlbedoColor;B_Albedo Color;17;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;17;-963.1097,48.41852;Float;True;Property;_MaskTexture;Mask Texture;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;65;-1322.078,2520.958;Float;False;Property;_B_MetalicIntensity;B_Metalic Intensity;22;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;20;-1371.204,-1022.747;Float;True;Property;_R_NormalMap;R_Normal Map;4;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-878.603,-702.1953;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-895.6469,886.2667;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-508.2749,-773.8362;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-855.7664,2359.118;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-523.2489,714.7306;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-915.4914,289.8651;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-893.577,786.3716;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-880.6729,-602.3003;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;-877.6808,1862.61;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-485.4383,2287.477;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-886.3488,-1310.829;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-857.8363,2459.014;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-1673.715,-2062.939;Float;False;Property;_MainNormal_Intensity;MainNormal_Intensity;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-517.5869,605.2967;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-527.3199,832.7607;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-531.3899,969.1047;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-512.3459,-655.8069;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-489.5093,2405.508;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-507.4638,-918.6865;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;45;-1345.153,-2102.313;Float;True;Property;_MainNormal;MainNormal;1;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-516.4169,-520.4625;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-493.5793,2541.852;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;-479.7763,2178.042;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-258.5127,2.431142;Float;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-261.0657,-136.4814;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendNormalsNode;48;-155.7221,-2035.69;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;-263.6191,116.2866;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;39;-266.1721,243.9471;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;257.7,-17.69563;Float;False;True;2;Float;;0;0;Standard;WILL/Mountain_3Layers;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;23;5;4;0
WireConnection;67;5;70;0
WireConnection;20;5;9;0
WireConnection;21;0;11;0
WireConnection;21;1;10;0
WireConnection;19;0;5;4
WireConnection;19;1;1;0
WireConnection;27;0;17;1
WireConnection;27;1;20;0
WireConnection;73;0;69;0
WireConnection;73;1;65;0
WireConnection;29;0;17;2
WireConnection;29;1;23;0
WireConnection;44;0;16;0
WireConnection;44;1;43;0
WireConnection;24;0;5;0
WireConnection;24;1;2;0
WireConnection;15;0;11;4
WireConnection;15;1;8;0
WireConnection;72;0;71;0
WireConnection;72;1;66;0
WireConnection;77;0;17;3
WireConnection;77;1;67;0
WireConnection;42;0;18;0
WireConnection;42;1;41;0
WireConnection;74;0;69;4
WireConnection;74;1;68;0
WireConnection;33;0;17;2
WireConnection;33;1;44;0
WireConnection;30;0;17;2
WireConnection;30;1;24;0
WireConnection;26;0;17;2
WireConnection;26;1;19;0
WireConnection;28;0;17;1
WireConnection;28;1;21;0
WireConnection;78;0;17;3
WireConnection;78;1;73;0
WireConnection;35;0;17;1
WireConnection;35;1;42;0
WireConnection;45;5;46;0
WireConnection;31;0;17;1
WireConnection;31;1;15;0
WireConnection;79;0;17;3
WireConnection;79;1;74;0
WireConnection;76;0;17;3
WireConnection;76;1;72;0
WireConnection;37;0;27;0
WireConnection;37;1;29;0
WireConnection;37;2;77;0
WireConnection;36;0;35;0
WireConnection;36;1;33;0
WireConnection;36;2;76;0
WireConnection;48;0;45;0
WireConnection;48;1;37;0
WireConnection;38;0;28;0
WireConnection;38;1;30;0
WireConnection;38;2;78;0
WireConnection;39;0;31;0
WireConnection;39;1;26;0
WireConnection;39;2;79;0
WireConnection;0;0;36;0
WireConnection;0;1;48;0
WireConnection;0;3;38;0
WireConnection;0;4;39;0
ASEEND*/
//CHKSM=C1B736D6197B80A7BCA0F88BADB99FCC021CE0CA