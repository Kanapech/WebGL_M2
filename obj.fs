
precision mediump float;

varying vec4 pos3D;
varying vec3 N;

uniform int uModel;


// ==============================================
void main(void)
{
	vec3 col = vec3(0.8,0.4,0.4);
	vec3 lightDir = normalize(-pos3D.xyz);
	
	vec3 diff = col * dot(N, lightDir); // Lambert rendering, eye light source

	// Blinn-Phong:
	vec3 uSpecular = vec3(0.5);
	vec3 spec = uSpecular * pow( max( dot( N, normalize( lightDir + lightDir ) ), 0.0 ), 32.0 );
	vec3 blinn = spec + diff;

	if(uModel == 0)
		col = diff;
	if(uModel == 1)
		col = blinn;

	gl_FragColor = vec4(col, 1.0);
}




