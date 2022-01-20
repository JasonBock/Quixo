namespace Quixo.Framework;

/// <summary>
/// The exception that is thrown if a user tries to make an invalid move.
/// </summary>
public sealed class InvalidMoveException
	: Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidMoveException"/> class
	/// </summary>
	public InvalidMoveException() : base() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidMoveException"/> class
	/// with its message string set to <i>message</i>.
	/// </summary>
	/// <param name="message">
	/// A <see cref="String"/> that describes the error. The content of message is intended to be 
	/// understood by humans. The caller of this constructor is required to ensure that this string 
	/// has been localized for the current system culture. 
	/// </param>
	public InvalidMoveException(string message)
		: base(message) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="InvalidMoveException"/> class
	/// with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// A <see cref="String"/> that describes the error. The content of message is intended to be 
	/// understood by humans. The caller of this constructor is required to ensure that this string 
	/// has been localized for the current system culture. 
	/// </param>
	/// <param name="inner">
	/// The exception that is the cause of the current exception. If the <i>innerException</i> parameter is not a 
	/// null reference, the current exception is raised in a <b>catch</b> block that handles 
	/// the inner exception. 
	/// </param>
	public InvalidMoveException(string message, Exception inner)
		: base(message, inner) { }
}