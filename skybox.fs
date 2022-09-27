precision mediump float;

varying vec2 texCoords;
varying vec3 vPosOrig;

uniform float boxSize;

uniform sampler2D uSampler0;
uniform sampler2D uSampler1;
uniform sampler2D uSampler2;
uniform sampler2D uSampler3;
uniform sampler2D uSampler4;
uniform sampler2D uSampler5;

void main(void){
    vec4 col = vec4( 0.0, 0.0, 0.0, 1.0);

    const float eps = 0.001;
    if(abs(vPosOrig.z - boxSize)<eps)
        col = texture2D( uSampler0, vec2(texCoords.s, texCoords.t));
    else if(abs(vPosOrig.z - -boxSize) < eps)
        col = texture2D( uSampler1, vec2(texCoords.s, texCoords.t));
    else if(abs(vPosOrig.y - boxSize) < eps)
        col = texture2D( uSampler2, vec2(texCoords.s, texCoords.t));
    else if(abs(vPosOrig.y - -boxSize) < eps)
        col = texture2D( uSampler3, vec2(texCoords.s, texCoords.t));
    else if(abs(vPosOrig.x - -boxSize) < eps)
        col = texture2D( uSampler4, vec2(texCoords.s, texCoords.t));
    else
        col = texture2D( uSampler5, vec2(texCoords.s, texCoords.t));

    gl_FragColor = col;
}
