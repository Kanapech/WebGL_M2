attribute vec3 aVertexPosition;
attribute vec3 aVertexNormal;

uniform mat4 uRMatrix;
uniform mat4 uMVMatrix;
uniform mat4 uPMatrix;
uniform mat4 uRotMVMatrix;

varying vec4 pos3D;
varying vec3 N;
varying mat4 RMatrix;

void main(void) {
	pos3D = uMVMatrix * vec4(aVertexPosition,1.0);
	N = vec3(uRMatrix * vec4(aVertexNormal,1.0));
	RMatrix = uRotMVMatrix;
	gl_Position = uPMatrix * pos3D;
}
