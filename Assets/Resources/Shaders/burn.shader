// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Eddies/Burn"
{
	Properties
	{
		[HideInInspector] _VTInfoBlock( "VT( auto )", Vector ) = ( 0, 0, 0, 0 )
		_Mask("Mask", 2D) = "white" {}
		_Texture0("Texture 0", 2D) = "white" {}
		_DistortionMap("Distortion Map", 2D) = "white" {}
		_DistortionAmount("Distortion Amount", Range( 0 , 1)) = 1
		_ScrollSpeed("Scroll Speed", Range( 0 , 1)) = 0
		_Warm("Warm", Color) = (0,0,0,0)
		_Hot("Hot", Color) = (0,0,0,0)
		_Power("Power", Float) = 6
		_BurnArea("Burn Area", 2D) = "white" {}
		_BurnAmount("BurnAmount", Range( 0 , 1)) = 0.5281364
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "Amplify" = "True"  "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		AlphaToMask On
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _BurnArea;
		uniform float4 _BurnArea_ST;
		uniform float4 _Warm;
		uniform float4 _Hot;
		uniform sampler2D _Mask;
		uniform sampler2D _DistortionMap;
		uniform float4 _DistortionMap_ST;
		uniform float _DistortionAmount;
		uniform float _ScrollSpeed;
		uniform float _Power;
		uniform sampler2D _Texture0;
		uniform float _BurnAmount;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BurnArea = i.uv_texcoord * _BurnArea_ST.xy + _BurnArea_ST.zw;
			float4 tex2DNode27 = tex2D( _BurnArea, uv_BurnArea );
			o.Albedo = tex2DNode27.rgb;
			float2 uv_DistortionMap = i.uv_texcoord * _DistortionMap_ST.xy + _DistortionMap_ST.zw;
			float3 DistortionMap47 = UnpackNormal( tex2D( _DistortionMap, uv_DistortionMap ) );
			float ScrollSpeed64 = ( _Time.y * _ScrollSpeed );
			float2 panner11 = ( ScrollSpeed64 * float2( 0,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord9 = i.uv_texcoord + panner11;
			float4 lerpResult19 = lerp( _Warm , _Hot , tex2D( _Mask, ( ( (DistortionMap47).xy * _DistortionAmount ) + uv_TexCoord9 ) ).r);
			float4 temp_cast_1 = (_Power).xxxx;
			float4 Burning39 = ( pow( lerpResult19 , temp_cast_1 ) * _Power );
			float2 panner53 = ( ScrollSpeed64 * float2( 0,-1 ) + i.uv_texcoord);
			float2 temp_output_60_0 = ( ( (UnpackNormal( tex2D( _DistortionMap, panner53 ) )).xy * 0.02 ) + i.uv_texcoord );
			float4 tex2DNode32 = tex2D( _Texture0, temp_output_60_0 );
			float temp_output_33_0 = step( tex2DNode32.r , _BurnAmount );
			o.Emission = ( Burning39 * ( temp_output_33_0 + ( temp_output_33_0 - step( tex2DNode32.r , ( _BurnAmount / 1.05 ) ) ) ) ).rgb;
			o.Alpha = tex2DNode27.a;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			AlphaToMask Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
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
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
1197;604;1225;617;2259.92;422.0225;3.062669;True;True
Node;AmplifyShaderEditor.RangedFloatNode;62;-2958.654,-527.5174;Inherit;False;Property;_ScrollSpeed;Scroll Speed;5;0;Create;True;0;0;False;0;0;0.74;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;63;-2826.264,-669.8902;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.VirtualTextureObject;4;-1817.22,-98.64896;Inherit;True;Property;_DistortionMap;Distortion Map;3;0;Create;True;0;0;False;0;-1;None;b0df4a518044fb04484a9e29fc9ead0e;False;white;Auto;Unity5;0;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-2608.26,-535.3731;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;29;-1426.922,-92.18319;Inherit;True;Property;_TextureSample1;Texture Sample 1;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;64;-2399.067,-517.8349;Inherit;False;ScrollSpeed;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;47;-1055.337,-60.53506;Inherit;False;DistortionMap;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;28;-1998.122,-968.6503;Inherit;False;2292.165;754.739;Comment;17;39;22;21;23;19;17;18;3;10;2;7;9;11;8;6;66;48;BURNING;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;48;-1812.172,-759.58;Inherit;False;47;DistortionMap;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;66;-1943.411,-422.046;Inherit;False;64;ScrollSpeed;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;67;-2064.777,494.8849;Inherit;False;64;ScrollSpeed;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;54;-2130.421,156.206;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-1666.414,-512.2922;Inherit;False;Property;_DistortionAmount;Distortion Amount;4;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;11;-1563.528,-373.118;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;53;-1795.068,215.2577;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;6;-1502.523,-769.6429;Inherit;True;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;55;-1404.651,185.8844;Inherit;True;Property;_TextureSample4;Texture Sample 4;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1265.556,-415.281;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1208.966,-744.3095;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;56;-1001.484,191.3707;Inherit;True;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-1154.331,496.3955;Inherit;False;Constant;_Heatwave;Heatwave;10;0;Create;True;0;0;False;0;0.02;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;2;-984.666,-435.7949;Inherit;True;Property;_Mask;Mask;1;0;Create;True;0;0;False;0;653ed76661a82e54eaeb5207168c77a1;653ed76661a82e54eaeb5207168c77a1;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-921.0915,-598.1156;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;18;-678.0003,-704.6678;Inherit;False;Property;_Hot;Hot;7;0;Create;True;0;0;False;0;0,0,0,0;0.9803922,0.7501533,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;59;-754.8417,547.1086;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;17;-608.1265,-906.431;Inherit;False;Property;_Warm;Warm;6;0;Create;True;0;0;False;0;0,0,0,0;1,0.5318798,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-700.3538,-499.8853;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-715.3459,394.6708;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;60;-493.2289,378.4202;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-214.6491,407.8609;Inherit;True;Constant;_DivideAmount;Divide Amount;9;0;Create;True;0;0;False;0;1.05;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-275.5026,267.9523;Inherit;False;Property;_BurnAmount;BurnAmount;10;0;Create;True;0;0;False;0;0.5281364;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;68;-670.7156,-65.56;Inherit;True;Property;_Texture0;Texture 0;2;0;Create;True;0;0;False;0;653ed76661a82e54eaeb5207168c77a1;653ed76661a82e54eaeb5207168c77a1;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.LerpOp;19;-355.442,-755.4617;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-275.9718,-522.3393;Inherit;False;Property;_Power;Power;8;0;Create;True;0;0;False;0;6;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;21;-93.77699,-743.7787;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;36;134.597,318.7472;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;32;-197.6412,-38.10981;Inherit;True;Property;_TextureSample3;Texture Sample 3;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;38;333.4114,185.6296;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;33;250.0038,-0.8242998;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;11.03395,-610.8064;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;44;515.8148,50.33308;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;68.1298,-386.1513;Inherit;False;Burning;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;43;804.1729,-299.1877;Inherit;False;39;Burning;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;797.6458,-98.96274;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;26;547.655,-1262.415;Inherit;True;Property;_BurnArea;Burn Area;9;0;Create;True;0;0;False;0;None;c18c1957bfcda2247a57837b06dd3426;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;27;863.2299,-1014.577;Inherit;True;Property;_TextureSample2;Texture Sample 2;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;79;-990.9941,80.42798;Inherit;False;Property;_MovementSpeed;Movement Speed;11;0;Create;True;0;0;False;0;0;0.1673729;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;1014.885,-218.7819;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;78;-556.444,156.1324;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1645.899,-708.3947;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Eddies/Burn;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;True;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;61;0;63;0
WireConnection;61;1;62;0
WireConnection;29;0;4;0
WireConnection;64;0;61;0
WireConnection;47;0;29;0
WireConnection;11;1;66;0
WireConnection;53;0;54;0
WireConnection;53;1;67;0
WireConnection;6;0;48;0
WireConnection;55;0;4;0
WireConnection;55;1;53;0
WireConnection;9;1;11;0
WireConnection;7;0;6;0
WireConnection;7;1;8;0
WireConnection;56;0;55;0
WireConnection;10;0;7;0
WireConnection;10;1;9;0
WireConnection;3;0;2;0
WireConnection;3;1;10;0
WireConnection;58;0;56;0
WireConnection;58;1;57;0
WireConnection;60;0;58;0
WireConnection;60;1;59;0
WireConnection;19;0;17;0
WireConnection;19;1;18;0
WireConnection;19;2;3;1
WireConnection;21;0;19;0
WireConnection;21;1;23;0
WireConnection;36;0;34;0
WireConnection;36;1;37;0
WireConnection;32;0;68;0
WireConnection;32;1;60;0
WireConnection;38;0;32;1
WireConnection;38;1;36;0
WireConnection;33;0;32;1
WireConnection;33;1;34;0
WireConnection;22;0;21;0
WireConnection;22;1;23;0
WireConnection;44;0;33;0
WireConnection;44;1;38;0
WireConnection;39;0;22;0
WireConnection;45;0;33;0
WireConnection;45;1;44;0
WireConnection;27;0;26;0
WireConnection;35;0;43;0
WireConnection;35;1;45;0
WireConnection;78;0;60;0
WireConnection;78;2;79;0
WireConnection;0;0;27;0
WireConnection;0;2;35;0
WireConnection;0;9;27;4
ASEEND*/
//CHKSM=4866A06D70F11356DE937D4383A2C9E3A02935D2