
precision mediump float;

varying vec4 pos3D;
varying vec3 N;

uniform int uModel;
uniform samplerCube uSkybox;
varying mat4 RMatrix;


// ==============================================
void main(void)
{
	vec3 col = vec3(0.8,0.4,0.4);
	vec3 lightDir = normalize(-pos3D.xyz); //same as eye position here
	
	vec3 diff = col * dot(N, lightDir); // Lambert rendering, eye light source

	

	if(uModel == 0)
		col = diff;

	if(uModel == 1){
		// Blinn-Phong:
		vec3 uSpecular = vec3(0.5);
		vec3 spec = uSpecular * pow( max( dot( N, normalize( lightDir + lightDir ) ), 0.0 ), 16.0 );
		vec3 blinn = spec + diff;
		col = blinn;	
	}

	if(uModel == 2){
		vec3 r = reflect(-lightDir, N);
		col = vec3(textureCube(uSkybox, normalize(r * mat3(RMatrix)))); //On ne veut pas que le reflet bouge avec la caméra
	}

	gl_FragColor = vec4(col, 1.0);
}