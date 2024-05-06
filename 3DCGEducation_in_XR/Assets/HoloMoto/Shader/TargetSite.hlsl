#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

float _Size;

sampler2D _MainTex;
float4 _MainTex_ST;
struct appdata
{
   float4  vertex :POSITION;
   float2 uv:TEXCOORD0;
   float3  normal :NORMAL;
};

struct FragmentInput
{
   float4 position : SV_POSITION;
   float2 uv :TEXCOORD0;
};

FragmentInput VertexOutput(float3 wp,float3 wn, float2 uv )
{
   FragmentInput i;
   i.position = TransformObjectToHClip(wp);
   i.uv = uv;
   return i;
}




appdata   vert (appdata v)
{
   v.vertex = mul(unity_ObjectToWorld, v.vertex);
   return  v;
}


//This shader get 4 vertex in Plane Models;
[maxvertexcount(120)]
void geom(triangle  appdata input[3],uint pid :SV_PrimitiveID,inout TriangleStream<FragmentInput> triStream)
{
   float3 wp0 = input[0].vertex.xyz;
   float3 wp1 = input[1].vertex.xyz;
   float3 wp2 = input[2].vertex.xyz;

   float2 uv0 = input[0].uv;
   float2 uv1 = input[1].uv;
   float2 uv2 = input[2].uv;
   float2 uv3 = input[0].uv;
   float2 uv4 = input[1].uv;
   float2 uv5 = input[2].uv;
   

   float3 wn0 = input[0].normal;
   float3 wn1 = input[1].normal;
   float3 wn2 = input[2].normal;
   
   float3 wp3 = input[0].vertex.xyz;
   float3 wp4 = input[1].vertex.xyz;
   float3 wp5 = input[2].vertex.xyz;
   
   float3 wn3 = input[0].normal;
   float3 wn4 = input[1].normal;
   float3 wn5 = input[2].normal;

   
   int i = 0;
   int n =_Size;
   float l= _Size-1;
   float ext= 0;
 
   while ( i<n)
   {
      float x=0;
      float result;
      

      float direction;
      direction= i ;

      float3 wp6 = wp0+float3(0,0,direction);
      float3 wp7 =wp1+float3(0,0,direction);
      float3 wp8 = wp2+float3(0,0,direction);
      float3 wp9 = wp3+float3(0,0,direction);
      float3 wp10 = wp4+float3(0,0,direction);
      float3 wp11 = wp5+float3(0,0,direction);
      
      triStream.Append(VertexOutput(wp6, wn0 ,uv0));
      triStream.Append(VertexOutput(wp7, wn1,uv1));
      triStream.Append(VertexOutput(wp8, wn2,uv2));

      triStream.Append(VertexOutput(wp9, wn3,uv3));
      triStream.Append(VertexOutput(wp10, wn4,uv4));
      triStream.Append(VertexOutput(wp11, wn5,uv5));
      triStream.RestartStrip();
      i=i+1;
   }

}


float4 frag (FragmentInput i):SV_Target
{
   
   float4 col=float4(1,1,1,1);
   col *= tex2D(_MainTex,i.uv);
    
   return col;
}