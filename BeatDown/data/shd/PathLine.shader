#version 120 
#extension GL_EXT_geometry_shader4 : enable

void main(void)
{
	// variable to use in for loops
	int i;

	// Emit the original vertices without changing, making
	// this part exactly the same as if no geometry shader
	// was used.
	for(i=0; i< gl_VerticesIn; i++)
	{
		gl_Position = gl_PositionIn[i];
		EmitVertex();
	}
	// End the one primitive with the original vertices
	EndPrimitive();

	// Now we generate some more! This translates 0.2 over
	// the positive x axis.
	for(i=0; i< gl_VerticesIn; i++)
	{
		gl_Position = gl_PositionIn[i];
		gl_Position.x += 0.2;
		EmitVertex();
	}
	EndPrimitive();
}#version 120 
				#extension GL_EXT_geometry_shader4 : enable

				void main(void)
				{
					// variable to use in for loops
					int i;

					// Emit the original vertices without changing, making
					// this part exactly the same as if no geometry shader
					// was used.
					for(i=0; i< gl_VerticesIn; i++)
					{
						gl_Position = gl_PositionIn[i];
						EmitVertex();
					}
					// End the one primitive with the original vertices
					EndPrimitive();

					// Now we generate some more! This translates 0.2 over
					// the positive x axis.
					for(i=0; i< gl_VerticesIn; i++)
					{
						gl_Position = gl_PositionIn[i];
						gl_Position.x += 0.2;
						EmitVertex();
					}
					EndPrimitive();
				}