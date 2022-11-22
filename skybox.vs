attribute vec3 aVertexPosition;

uniform mat4 uMVMatrix;
uniform mat4 uPMatrix;

varying vec3 texCoords;

void main(void){
    texCoords = aVertexPosition;
    vec4 pos =  uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0);
    gl_Position = pos;
}