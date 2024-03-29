﻿using System.Drawing.Drawing2D;
using QF = Quixo.Framework;

namespace Quixo;

public partial class Piece
	: UserControl
{
	public EventHandler? Selected { get; set; }

	private SelectedState currentSelectedState = SelectedState.None;
	private readonly QF.Piece? piece;
	private QF.Player currentPlayer = QF.Player.None;

	private Piece() => this.InitializeComponent();

	internal Piece(QF.Piece piece)
		: this()
	{
		this.InitializeComponent();
		this.SetStyle(ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
		this.piece = piece;
	}

	private void DrawPiece(Graphics graphics)
	{
		var rectangle = this.ClientRectangle;
		graphics.FillRectangle(Brushes.Black, rectangle);

		switch (this.currentPlayer)
		{
			case QF.Player.X:
				this.DrawXPiece(graphics);
				break;
			case QF.Player.O:
				this.DrawOPiece(graphics);
				break;
			default:
				break;
		}

		switch (this.currentSelectedState)
		{
			case SelectedState.CanBeMoved:
				this.DrawBorder(graphics, Color.Yellow);
				break;
			case SelectedState.Selected:
				this.DrawBorder(graphics, Color.Red);
				break;
			default:
				break;
		}
	}

	private void DrawBorder(Graphics graphics, Color borderColor)
	{
		var rectangle = this.ClientRectangle;
		rectangle.Inflate(-1, -1);

		if (rectangle.Width > 0 && rectangle.Height > 0)
		{
			using (Brush valueBrush = new SolidBrush(borderColor))
			{
				using (var valuePen = new Pen(valueBrush, 2F))
				{
					graphics.DrawRectangle(valuePen, rectangle);
				}
			}
		}
	}

	private void DrawXPiece(Graphics graphics)
	{
		var rectangle = this.ClientRectangle;
		rectangle.Inflate(-4, -4);

		if (rectangle.Width > 0 && rectangle.Height > 0)
		{
			using (Brush valueBrush = new LinearGradientBrush(this.ClientRectangle, Color.Orange, Color.Red, 45F))
			{
				var penWidth = ((rectangle.Width + rectangle.Height) / 2F) / 14F;

				using (var valuePen = new Pen(valueBrush, penWidth))
				{
					graphics.DrawLine(valuePen, rectangle.X, rectangle.Y,
						 rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
					graphics.DrawLine(valuePen, rectangle.X, rectangle.Y + rectangle.Height,
						 rectangle.X + rectangle.Width, rectangle.Y);
				}
			}
		}
	}

	private void DrawOPiece(Graphics graphics)
	{
		var rectangle = this.ClientRectangle;
		rectangle.Inflate(-4, -4);

		if (rectangle.Width > 0 && rectangle.Height > 0)
		{
			using (var valueBrush = new LinearGradientBrush(this.ClientRectangle, Color.Orange, Color.Red, 45F))
			{
				var penWidth = ((rectangle.Width + rectangle.Height) / 2F) / 14F;
				using (var valuePen = new Pen(valueBrush, penWidth))
				{
					graphics.DrawEllipse(valuePen, rectangle);
				}
			}
		}
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		base.Invalidate();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		ArgumentNullException.ThrowIfNull(e);
		base.OnPaint(e);
		this.DrawPiece(e.Graphics);
	}

	protected override void OnClick(EventArgs e)
	{
		base.OnClick(e);

		if (this.currentSelectedState != SelectedState.None)
		{
			this.Selected?.Invoke(this, EventArgs.Empty);
		}
	}

	internal QF.Piece? RepresentedPiece => this.piece;

	internal SelectedState CurrentSelectedState
	{
		get => this.currentSelectedState;
		set
		{
			this.currentSelectedState = value;
			this.Invalidate();
		}
	}

	internal QF.Player CurrentPlayer
	{
		get => this.currentPlayer;
		set
		{
			this.currentPlayer = value;
			this.Invalidate();
		}
	}

	public enum SelectedState
	{
		None, CanBeMoved, Selected
	}
}