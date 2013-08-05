using System;

namespace BeatDown.Renderer.InterfaceElements
{
	public class SelectedUnitInfo:InterfaceElement
	{

		protected Base Name;
		protected Base Hp;
		protected Base Actions;

		public SelectedUnitInfo (InterfaceElement parent)
		{
			this.parent = parent;
			this.Width=140;
			this.Height =80;
			this.X = 500;
			this.Y = 400;


			Name = new Base(this);
			Name.X = 500;
			Name.Y = 400;
			Name.Width=140;
			Name.Height =32;
			Name.Text ="NAME";

			Hp = new Base(this);
			Hp.X = 500;
			Hp.Y = 433;
			Hp.Width=140;
			Hp.Height =32;
			Hp.Text = "Health:";

			Actions = new Base(this);
			Actions.X = 500;
			Actions.Y = 464;
			Actions.Width=140;
			Actions.Height =32;
			Actions.Text = "Actions:";

			this.children.Add(Name);
			this.children.Add(Hp);
			this.children.Add(Actions);

		}
		public override void Draw ()
		{
			if (Game.Selection.SelectedId != Game.Selection.NONE) {
				Game.Unit tmp = Game.Game.Instance.Manager.Units [Game.Selection.SelectedId];
				Name.Text = tmp.Name;
				Hp.Text = String.Format ("Health: {0}/{1}", tmp.Health, tmp.MaxHealth);
				Actions.Text = String.Format ("Actions: {0}/{1}", tmp.ActionPoints, tmp.MaxActionPoints);

			} else {
				Name.Text = "";
				Hp.Text = "";
				Actions.Text = "";
			}
			base.Draw ();
		}

		public override void Layout (int width, int height)
		{
			this.X = width - this.Width;
			this.Y = height - this.Height;
		}
	}
}

