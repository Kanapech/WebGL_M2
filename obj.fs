
precision mediump float;

varying vec4 pos3D;
varying vec3 N;
uniform bool blinn;



// ==============================================
void main(void)
{
	vec3 lightDir = normalize(-pos3D.xyz);
	
	vec3 diff = vec3(0.8,0.4,0.4) * dot(N, lightDir); // Lambert rendering, eye light source

	// Blinn-Phong:
	vec3 uSpecular = vec3(0.5);
	vec3 spec = uSpecular * pow( max( dot( N, normalize( lightDir + lightDir ) ), 0.0 ), 32.0 );
	vec3 res = spec + diff;

	vec3 col = blinn?res:diff;

	gl_FragColor = vec4(col, 1.0);
}




