
precision mediump float;

uniform int uModel;
uniform float uRoughness;
uniform samplerCube uSkybox;
uniform float uN1;
uniform float uN2;

varying vec4 pos3D;
varying vec3 N;
varying mat4 RMatrix;


const float PI = 3.14159;

float SchlickApprox(float NdotL, float Kr){ //https://wikipedia.org/fr/Approximation_de_Schlick
	return Kr + (1.0 - Kr) * pow((1.0 - NdotL), 5.0);
}

float TrowbridgeReitz(float NH2, float roughness2)
{
    float denom = NH2 * roughness2 + (1.0 - NH2);
    return roughness2 / (PI * pow(denom, 2.0));
}

float GGXSmith(float roughness2, float NdotV) // La lumière est à la même position que l'observateur donc on retourne juste le carré de la valeur calculée
{
    float g = (NdotV * 2.0) / (NdotV + sqrt(roughness2 + (1.0 - roughness2) * pow(NdotV, 2.0)));
    return pow(g, 2.0);
}


// ==============================================
void main(void)
{
	vec3 col = vec3(0.8,0.4,0.4);
	vec3 lightDir = normalize(-pos3D.xyz); //same as eye position here
	vec3 normals = normalize(N);
	vec3 diff = col * dot(normals, lightDir); // Lambert rendering, eye light source

	if(uModel == 0)
		col = diff;

	if(uModel == 1){// Blinn-Phong		
		vec3 uSpecular = vec3(0.1);
		vec3 spec = uSpecular * pow( max( dot( normals, normalize( lightDir + lightDir ) ), 0.0 ), 32.0 );
		vec3 blinn = spec + diff;
		col = blinn;	
	}

	if(uModel == 2){ //Mirror
		vec3 r = reflect(-lightDir, normals);
		col = vec3(textureCube(uSkybox, normalize(r * mat3(RMatrix)))); //On ne veut pas que le reflet bouge avec la caméra
	}

	if(uModel == 3){ //https://garykeen27.wixsite.com/portfolio/cook-torrance-shading

		vec3 CookTorrance = vec3(0.0);

		float NdotV = max(dot(normals,lightDir), 0.0);
		vec3 halfAngle = normalize(lightDir+lightDir);
		float NdotH = max(dot(normals,halfAngle), 0.0);
		float NH2 = pow(NdotH, 2.0);
		float roughness2 = pow(clamp(uRoughness, 0.01, 0.99), 2.0);
		float Kr = pow((uN1 - uN2) / (uN1 + uN2), 2.0);

		float F = SchlickApprox(NdotV, Kr);
		float D = TrowbridgeReitz(NH2, roughness2);
		float G = GGXSmith(roughness2, NdotV);

		float specular = (F * G * D) / (PI * NdotV); //refelectance spéculaire
		CookTorrance += specular;
		vec3 Kd = max((1.0 - CookTorrance), 0.0); //refelectance diffuse (la somme des 2 doit être égale à 1 pour garder la même quantité de lumière reçue qu'envoyée)
		
		col = (Kd*diff) + CookTorrance;
	}

	if(uModel == 4){ //Transparent
		//Modele basique
		float ratio = uN1/uN2;
		vec3 r = refract(-lightDir, normals, ratio);
		//col = vec3(textureCube(uSkybox, normalize(r * mat3(RMatrix))));

		//Meilleur modele avec reflet dans l'objet
		float NdotV = max(dot(normals,lightDir), 0.0);
		float Kr = pow((uN1 - uN2) / (uN1 + uN2), 2.0);
		vec3 r2 = reflect(-lightDir, normals);
		float F = SchlickApprox(NdotV, Kr);

		vec4 reflectCol = textureCube(uSkybox, normalize(r2 * mat3(RMatrix)));
		vec4 refractCol = textureCube(uSkybox, normalize(r * mat3(RMatrix)));
		col = vec3(mix(refractCol, reflectCol, F));
	}

	gl_FragColor = vec4(col, 1.0);
}