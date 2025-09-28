#version 430 core

layout(location = 0) in vec3 position;
layout(location = 3) in vec4 color;
layout(location = 4) in vec4 misc;

uniform mat4 PVM;

out vec4 vColor;

void main()
{
    gl_Position = PVM * vec4(position, 1.0);
    gl_PointSize = misc.x;
    vColor = color;
}
