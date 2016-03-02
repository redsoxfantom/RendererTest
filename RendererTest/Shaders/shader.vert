#version 330 core

// The vertices's position
layout(location = 0) in vec3 vert_modelspace_pos;

// The model-view-perspective matrix
uniform mat4 MVP;

void main()
{
	// Sinple passthrough shader, just calculate the output clip-space postion of the vertex
	gl_Position = MVP * vec4(vert_modelspace_pos,1.0);
}