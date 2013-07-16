using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using BeatDown.Game;
using Game.Planning;
using System.Collections.Generic;

namespace BeatDown.Renderer.GameObjects
{
	public class UnitRenderer:BaseRender
	{
		public static void RenderViewable (Unit u)
		{

			//draw the path to the hovered location.
			if (SharedResources.MouseIsDown && 
			    u.glId == Game.Selection.SelectedId 
			    && Game.Selection.Maploc !=0) {
				/*foreach(ITask task in u.Plan){
					if(task is Path){
						Path p = (Path)task;

						GL.PushMatrix();
						GL.Color3(1.0f,0.0f,0.0f);
						for(int i = 1; i < p.Coords.Count;i++){
							GL.Begin(BeginMode.Lines);

							GL.Vertex3(p.Coords[i].X,p.Coords[i].Y, p.Coords[i].Z);
							GL.Vertex3(p.Coords[i-1].X,p.Coords[i-1].Y, p.Coords[i-1].Z);



							GL.End ();
						}
						GL.PopMatrix();
					}
				}*/
				int x = (Game.Selection.Maploc-1)%Game.World.WORLD_SIZE;
				int z = (int)Math.Floor ((double)(Game.Selection.Maploc-1)/Game.World.WORLD_SIZE);
				List<Renderable.coords> Coords = Renderer.Render.Instance.w.GetPath(u.X, u.Z, x,z);
				Console.WriteLine("drawpath to "+x+","+z+" is "+Coords.Count);
				GL.PushMatrix();
					GL.Translate(0,1,0);
					GL.Color3(1.0f,0.0f,0.0f);
					
						for(int i = 1; i < Coords.Count;i++){
							
				//	Console.WriteLine(String.Format("Lines: {0},{1},{2}",Coords[i].X,Coords[i].Y, Coords[i].Z));
					GL.Begin(BeginMode.Lines);
							GL.Vertex3(Coords[i].X,Coords[i].Y, Coords[i].Z);
							GL.Vertex3(Coords[i-1].X,Coords[i-1].Y, Coords[i-1].Z);
					GL.End ();



						}

				GL.PopMatrix();	Console.WriteLine();

			}




			BaseRender.RenderViewable(u);
		}
		public static void RenderPickable(Unit u){
			BaseRender.RenderPickable(u);
		}
	}
}

