namespace VEnum
{
	/// <summary>
	/// Instance management extension methods.
	/// </summary>
	public static class EnumInstanceExt
	{
		/// <summary>
		/// Creates a new (empty) instance of an <see cref="Enum"/> object.
		/// </summary>
		/// <param name="type">The <see cref="Type"/> object to use to reference an <see cref="Enum"/> object's <see cref="Type"/> object.</param>
		/// <returns>the <see cref="Enum"/> object representation of the given <paramref name="type"/>.</returns>
		/// <exception cref="ArgumentException"></exception>
		public static Enum? CreateInstanceFromType(this Type type) => type.IsAssignableFrom(typeof(Enum)) ? ((Enum[])Enum.GetValues(type)).FirstOrDefault() : throw new ArgumentException("The Type object must reference an object that inherits from the Enum class.", nameof(type));

	}
}
