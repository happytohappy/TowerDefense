Shader "PBRMaskTintToon"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_SAM("SAM", 2D) = "white" {}
		_Emission("Emission", 2D) = "white" {}
		_Mask01("Mask01", 2D) = "white" {}
		_Mask02("Mask02", 2D) = "white" {}
		_Mask03("Mask03", 2D) = "white" {}
		_Color01("Color01", Color) = (0.7205882,0.08477508,0.08477508,0)
		_Color02("Color02", Color) = (0.02649222,0.3602941,0.09785674,0)
		_Color03("Color03", Color) = (0.07628676,0.2567445,0.6102941,0)
		_Color04("Color04", Color) = (1,0.6729082,0,0)
		_Color05("Color05", Color) = (0.3161438,0.08018869,1,0)
		_Color06("Color06", Color) = (0.829558,0.2311321,1,0)
		_Color07("Color07", Color) = (0.5660378,0.23073,0.03470988,0)
		_Color08("Color08", Color) = (0.3584906,0.3584906,0.3584906,0)
		_Color09_SKIN("Color09_SKIN", Color) = (0.9622642,0.6942402,0.521983,0)
		[HDR]_EmissionPower("EmissionPower", Color) = (0,0,0,0)
		_Color01Power("Color01Power", Range(0 , 6)) = 1
		_Color02Power("Color02Power", Range(0 , 6)) = 1
		_Color03Power("Color03Power", Range(0 , 6)) = 1
		_Color04Power("Color04Power", Range(0 , 6)) = 1
		_Color05Power("Color05Power", Range(0 , 6)) = 1
		_Color06Power("Color06Power", Range(0 , 6)) = 1
		_Color07Power("Color07Power", Range(0 , 6)) = 1
		_Color08Power("Color08Power", Range(0 , 6)) = 1
		_Color09Power("Color09Power", Range(0 , 6)) = 1
		[HideInInspector] _texcoord("", 2D) = "white" {}
		[HideInInspector] __dirty("", Int) = 1

		/* Color used for the material. Leave white if none */
		_Color("Color", Color) = (1,1,1,1)
		///* Texture used for the material */
		//_MainTex("Main Texture", 2D) = "white" {}
		/* Ambient color to add to the light calculation */
		[HDR] _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		/* Color to apply on the specular lighting stage */
		[HDR] _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
		/* The size of the specular reflection */
		_Glossiness("Glossiness", Float) = 32
		/* The color used in the rim lighting stage */
		[HDR] _RimColor("Rim Color", Color) = (1,1,1,1)
		/* How much should the material be affected by rim lighting */
		_RimBlend("Rim Blend", Range(0, 1)) = 0.5
		/* Controls how smoothly the rim blends with other unlit parts */
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
		/* Controls the color transition between shadowed surfaces and non shadowed surfaces */
		_Smoothness("Smoothness", Range(0, 0.5)) = 0.025
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 viewDir;
			float3 worldPos;
		};

		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Mask01;
		uniform float4 _Mask01_ST;
		uniform float4 _Color01;
		uniform float _Color01Power;
		uniform float4 _Color02;
		uniform float _Color02Power;
		uniform float4 _Color03;
		uniform float _Color03Power;
		uniform sampler2D _Mask02;
		uniform float4 _Mask02_ST;
		uniform float4 _Color04;
		uniform float _Color04Power;
		uniform float4 _Color05;
		uniform float _Color05Power;
		uniform float4 _Color06;
		uniform float _Color06Power;
		uniform sampler2D _Mask03;
		uniform float4 _Mask03_ST;
		uniform float4 _Color07;
		uniform float _Color07Power;
		uniform float4 _Color08;
		uniform float _Color08Power;
		uniform float4 _Color09_SKIN;
		uniform float _Color09Power;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform float4 _EmissionPower;
		uniform sampler2D _SAM;
		uniform float4 _SAM_ST;

		uniform float4 _Color;
		uniform float4 _AmbientColor;
		uniform float4 _SpecularColor;
		uniform float _Glossiness;
		uniform float _Smoothness;
		uniform float4 _RimColor;
		uniform float _RimBlend;
		uniform float _RimThreshold;

		void surf(Input i , inout SurfaceOutputStandard o)
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode16 = tex2D(_Albedo, uv_Albedo);
			float2 uv_Mask01 = i.uv_texcoord * _Mask01_ST.xy + _Mask01_ST.zw;
			float4 tex2DNode13 = tex2D(_Mask01, uv_Mask01);
			float4 temp_cast_0 = (tex2DNode13.r).xxxx;
			float4 temp_cast_1 = (tex2DNode13.g).xxxx;
			float4 temp_cast_2 = (tex2DNode13.b).xxxx;
			float2 uv_Mask02 = i.uv_texcoord * _Mask02_ST.xy + _Mask02_ST.zw;
			float4 tex2DNode41 = tex2D(_Mask02, uv_Mask02);
			float4 temp_cast_3 = (tex2DNode41.r).xxxx;
			float4 temp_cast_4 = (tex2DNode41.g).xxxx;
			float4 temp_cast_5 = (tex2DNode41.b).xxxx;
			float2 uv_Mask03 = i.uv_texcoord * _Mask03_ST.xy + _Mask03_ST.zw;
			float4 tex2DNode58 = tex2D(_Mask03, uv_Mask03);
			float4 temp_cast_6 = (tex2DNode58.r).xxxx;
			float4 temp_cast_7 = (tex2DNode58.g).xxxx;
			float4 temp_cast_8 = (tex2DNode58.b).xxxx;
			float4 blendOpSrc22 = tex2DNode16;
			float4 blendOpDest22 = ((min(temp_cast_0 , _Color01) * _Color01Power) + (min(temp_cast_1 , _Color02) * _Color02Power) + (min(temp_cast_2 , _Color03) * _Color03Power) + (min(temp_cast_3 , _Color04) * _Color04Power) + (min(temp_cast_4 , _Color05) * _Color05Power) + (min(temp_cast_5 , _Color06) * _Color06Power) + (min(temp_cast_6 , _Color07) * _Color07Power) + (min(temp_cast_7 , _Color08) * _Color08Power) + (min(temp_cast_8 , _Color09_SKIN) * _Color09Power));
			float4 lerpResult4 = lerp(tex2DNode16 , (saturate((blendOpSrc22 * blendOpDest22))) , (tex2DNode13.r + tex2DNode13.g + tex2DNode13.b + tex2DNode41.r + tex2DNode41.g + tex2DNode41.b + tex2DNode58.r + tex2DNode58.g + tex2DNode58.b));
			o.Albedo = lerpResult4.rgb;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			o.Emission = (tex2D(_Emission, uv_Emission) * _EmissionPower).rgb;
			float2 uv_SAM = i.uv_texcoord * _SAM_ST.xy + _SAM_ST.zw;
			float4 tex2DNode67 = tex2D(_SAM, uv_SAM);
			o.Metallic = tex2DNode67.b;
			o.Smoothness = tex2DNode67.r;
			o.Alpha = 1;


			float3 normal = normalize(o.Normal);
			float3 viewDir = normalize(i.viewDir);

			float3 lightDir = _WorldSpaceLightPos0;
#if defined(POINT) || defined(SPOT)
			lightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
#endif   

			float attenuation = 0.8f;
			//UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos);

			/* Calculate illumination from directional light. */
			float NdotL = dot(_WorldSpaceLightPos0, normal);

			/* Sample the shadow map and get a value in range [0, 1] where 0 is in the shadow, and 1 is not. */
			float shadowValue = attenuation;//SHADOW_ATTENUATION(i);
			/* Clamp the intensity into unlit and lit and interpolate them using the _Smoothness value */
			float lightIntensity = smoothstep(0, _Smoothness, NdotL * shadowValue) * 0.75;
			/* Multiply by the main directional light's intensity and color. */
			float4 light = lightIntensity * _LightColor0;

			/* Calculate specular lighting */
			float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
			float NdotH = dot(normal, halfVector);
			float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
			float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
			float4 specular = specularIntensitySmooth * _SpecularColor;

			/* Calculate rim lighting */
			float rimDot = 1 - dot(viewDir, normal);
			/* Rim should only appear on the lit parts of the surface so we multiply it by NdotL, raised to a power to smoothly blend it. */
			float rawRimIntensity = rimDot * pow(NdotL, _RimThreshold);
			float rimIntensity = smoothstep(_RimBlend - 0.01, _RimBlend + 0.01, rawRimIntensity);
			float4 rim = rimIntensity * _RimColor;

			/* Sample the main texture */
			//float4 sample = tex2D(_MainTex, i.uv);
			/* Combine everything */
			//return (light + rim + specular + _AmbientColor) * attenuation * _Color * sample;



			o.Emission = (light + rim + specular + _AmbientColor) * attenuation * _Color * lerpResult4;

		}

		ENDCG
	}
	Fallback "Diffuse"
}