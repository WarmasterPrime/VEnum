namespace VEnum
{
	/// <summary>
	/// 
	/// </summary>
	public class InvalidArgumentType:Exception
	{
		/// <summary>
		/// The value involved, related to, or resulting in this exception being thrown.
		/// </summary>
		public object? Value { get; private set; } = null;


		/// <inheritdoc cref="InvalidArgumentType(string, object, Exception)"/>
		public InvalidArgumentType() : base("The argument data-type is invalid.") { }
		/// <inheritdoc cref="InvalidArgumentType(string, object, Exception)"/>
		public InvalidArgumentType(string message) : base(message) { }
		/// <inheritdoc cref="InvalidArgumentType(string, object, Exception)"/>
		public InvalidArgumentType(string message, Exception innerException) : base(message, innerException) { }
		/// <inheritdoc cref="InvalidArgumentType(string, object, Exception)"/>
		/// <param name="argumentType">The <see cref="Type"/> object of the argument.</param>
		/// <param name="argumentName">The name of the argument.</param>
		public InvalidArgumentType(Type argumentType, string argumentName) : base($"The given parameter (\"{argumentName}\") is not a valid or acceptable data-type. Received the type \"{argumentType.Name}\".") { }
		/// <inheritdoc cref="InvalidArgumentType(string, object, Exception)"/>
		public InvalidArgumentType(string message, object value) : this(GenerateMessage(message, value))
		{
			Value=value;
		}
		/// <summary>
		/// The argument data-type is invalid.
		/// </summary>
		/// <param name="message">The exception message as a <see cref="string"/>.</param>
		/// <param name="value">An <see cref="object"/> that is used for context related to the exception.</param>
		/// <param name="innerException">The related <see cref="Exception"/> object.</param>
		public InvalidArgumentType(string message, object value, Exception innerException) : base(message, innerException)
		{
			Value=value;
		}
		/// <summary>
		/// Combines the context values with the exception message.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="args">The context values to append to the exception message.</param>
		/// <returns>a <see cref="string"/> representation of the newly generated exception message.</returns>
		private static string GenerateMessage(string message, params object[] args)
		{
			string res=message+"\nValues:";
			foreach(var sel in args)
				res+="\n- "+Convert.ToString(sel);
			return res;
		}

	}
}
