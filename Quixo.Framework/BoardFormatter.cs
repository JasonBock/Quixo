using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace Quixo.Framework
{
	/// <summary>
	/// This class will serialize and deserialize a <see cref="Board"/> class.
	/// </summary>
	/// <remarks>
	/// The format of the serialization is: "0,0:0,4|4,0:4,4|0,0:0,4", 
	/// where each move is separated by a pipe character ('|'), 
	/// the source and the destination of each move is separated by a colon (':'),
	/// and the X and Y coordinates are separated by a comma (',').
	/// By using a simple format like this, it's easy to create a game
	/// using a text editor. It also removes versioning issues that
	/// can be run into using other formatters.
	/// </remarks>
	public sealed class BoardFormatter
		: IFormatter
	{
		private ISurrogateSelector selector;
		private SerializationBinder binder;
		private StreamingContext context;

		/// <summary>
		/// Returns a <see cref="Board"/> object based on the 
		/// serialized data.
		/// </summary>
		/// <param name="serializationStream">A serialized version of a <see cref="Board"/> in the simplified format</param>
		/// <returns>A new <see cref="Board"/> object.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="serializationStream"/> is <code>null</code>.</exception>
		/// <exception cref="SerializationException">Thrown if an error occurred during deserialization.</exception>
		public object Deserialize(Stream serializationStream)
		{
			if (serializationStream == null) { throw new ArgumentNullException(nameof(serializationStream));  }

			var board = new Board();

			string moves = null;

			using (var reader = new StreamReader(serializationStream))
			{
				moves = reader.ReadToEnd();
			}

			try
			{
				foreach (var move in moves.Split('|'))
				{
					var moveParts = move.Split(':');
					var sourceMove = moveParts[0].Split(',')[0];
					var destinationMove = moveParts[1].Split(',')[1];

					board.MovePiece(
						new Point(int.Parse(moveParts[0].Split(',')[0]),
							int.Parse(moveParts[0].Split(',')[1])),
						new Point(int.Parse(moveParts[1].Split(',')[0]),
							int.Parse(moveParts[1].Split(',')[1])));
				}
			}
			catch (FormatException formatEx)
			{
				throw new SerializationException(string.Empty, formatEx);
			}
			catch (IndexOutOfRangeException indexEx)
			{
				throw new SerializationException(string.Empty, indexEx);
			}

			return board;
		}

		/// <summary>
		/// Serializes the given <see cref="Board"/> into the stream.
		/// </summary>
		/// <param name="serializationStream">The stream to serialize the given <see cref="Board"/>.</param>
		/// <param name="graph">A <see cref="Board"/> object to serialize.</param>
		/// <exception cref="ArgumentNullException">Thrown if either <paramref name="serializationStream"/> or <paramref name="graph"/> are <code>null</code>.</exception>
		/// <exception cref="ArgumentException">Thrown if <paramref name="graph"/> is not a <see cref="Board"/>.</exception>
		public void Serialize(Stream serializationStream, object graph)
		{
			if (graph == null) { throw new ArgumentNullException(nameof(graph)); }

			if (serializationStream == null) { throw new ArgumentNullException(nameof(serializationStream)); }

			if (graph.GetType() != typeof(Board)) { throw new ArgumentException("The given object must be a Board.", nameof(graph)); }

			var board = graph as Board;
			var moves = new List<string>();

			foreach (var move in board.Moves)
			{
				moves.Add($"{move.Source.X},{move.Source.Y}:{move.Destination.X},{move.Destination.Y}");
			}

			using (var writer = new StreamWriter(serializationStream))
			{
				writer.Write(string.Join("|", moves.ToArray()));
			}
		}

		public SerializationBinder Binder
		{
			get => this.binder;
			set => this.binder = value;
		}

		public StreamingContext Context
		{
			get => this.context;
			set => this.context = value;
		}

		public ISurrogateSelector SurrogateSelector
		{
			get => this.selector;
			set => this.selector = value;
		}
	}
}