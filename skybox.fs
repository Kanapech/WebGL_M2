precision mediump float;

varying vec3 texCoords;
uniform samplerCube uSkybox;

void main(void){
    vec4 col = vec4( 1.0, 0.0, 0.0, 1.0);
    col = textureCube(uSkybox, texCoords);
    gl_FragColor = col;
}
