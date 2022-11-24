precision mediump float;

uniform samplerCube uSkybox;
varying vec3 texCoords;

void main(void){
    //vec4 col = vec4( 1.0, 0.0, 0.0, 1.0);
    gl_FragColor = textureCube(uSkybox, texCoords);;
}
