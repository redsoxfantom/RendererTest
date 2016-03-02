#version 330 core

// The vertices's position
layout(location = 0) in vec3 vert_modelspace_pos;
layout(location = 1) in vec3 norm_modelspace_vector;

// The model-view-perspective matrix
uniform mat4 MVP;

void main()
{
	// Sinple passthrough shader, just calculate the output clip-space postion of the vertex
	gl_Position = MVP * vec4(vert_modelspace_pos,1.0);
}